using APIGuia.Context;
using APIGuia.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace APIGuia.Controllers;


[ApiController]
[Route("[controller]")]
public class ReservasController : ControllerBase
{
    private readonly APIGuiaDBContext _context;
    private readonly IMemoryCache _cache;

    // Injeta o contexto do banco de dados e o cache em memória
    public ReservasController(APIGuiaDBContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    // Endpoint para buscar as reservas filtradas por data
    [HttpGet]
    [Authorize] // Requer autenticação
    public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetReservasFiltradas(

    // Parâmetros da requisição
    [FromQuery] DateTime? dataInicio,
    [FromQuery] DateTime? dataFim,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    
    {
        if (dataInicio == null || dataFim == null)
        {
            return BadRequest("As datas de início e fim são obrigatórias.");
        }


        // Define a chave do cache com base nos parâmetros da requisição
        string cacheKey = $"reservas_{dataInicio}_{dataFim}_{page}_{pageSize}";

        // Verifica se os dados estão no cache
        // Caso não estejam, busca no banco de dados
        if (!_cache.TryGetValue(cacheKey, out PaginationDTO<ReservaDTO>? resposta))
        {
            var query = _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Suite)
                .Where(r => r.DataEntrada >= dataInicio && r.DataSaida <= dataFim); // Filtra as reservas no intervalo de datas


            // Calcula o total de registros e o número de páginas
            int totalRegistros = await query.CountAsync();
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / pageSize);


            // Aplica a paginação e seleciona os dados necessários
            var reservas = await query
                .Skip((page - 1) * pageSize)   // Pula os registros das páginas anteriores
                .Take(pageSize)               // Limita a quantidade de registros
                .Select(r => new ReservaDTO
                {
                    ReservaId = r.ReservaId,
                    DataEntrada = r.DataEntrada,
                    DataSaida = r.DataSaida,
                    ClienteNome = r.Cliente.Nome, // Nome do cliente
                    SuiteTipo = r.Suite.Tipo,    // Tipo da suíte
                    Valor = r.Valor
                })
                .ToListAsync();


            // Cria o objeto de resposta com os dados paginados
            resposta = new PaginationDTO<ReservaDTO>
            {
                Dados = reservas,
                PaginaAtual = page,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros
            };


            // Configura as opções de cache
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5), // Expira em 5 minutos
                SlidingExpiration = TimeSpan.FromMinutes(2) // Renova se for acessado em 2 min
            };

            // Armazena os dados no cache
            _cache.Set(cacheKey, resposta, cacheOptions);
        }

        // Retorna os dados paginados
        return Ok(resposta);
    }
}

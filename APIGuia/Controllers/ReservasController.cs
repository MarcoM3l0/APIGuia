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

    public ReservasController(APIGuiaDBContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }


    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetReservasFiltradas(
    [FromQuery] DateTime? dataInicio,
    [FromQuery] DateTime? dataFim,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        if (dataInicio == null || dataFim == null)
        {
            return BadRequest("As datas de início e fim são obrigatórias.");
        }

        string cacheKey = $"reservas_{dataInicio}_{dataFim}_{page}_{pageSize}";

        if (!_cache.TryGetValue(cacheKey, out PaginationDTO<ReservaDTO>? resposta))
        {
            var query = _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Suite)
                .Where(r => r.DataEntrada >= dataInicio && r.DataSaida <= dataFim);

            int totalRegistros = await query.CountAsync();
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / pageSize);

            var reservas = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new ReservaDTO
                {
                    ReservaId = r.ReservaId,
                    DataEntrada = r.DataEntrada,
                    DataSaida = r.DataSaida,
                    ClienteNome = r.Cliente.Nome,
                    SuiteTipo = r.Suite.Tipo,
                    Valor = r.Valor
                })
                .ToListAsync();

            resposta = new PaginationDTO<ReservaDTO>
            {
                Dados = reservas,
                PaginaAtual = page,
                TotalPaginas = totalPaginas,
                TotalRegistros = totalRegistros
            };

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5), // Expira em 5 minutos
                SlidingExpiration = TimeSpan.FromMinutes(2) // Renova se for acessado em 2 min
            };

            _cache.Set(cacheKey, resposta, cacheOptions);
        }

        return Ok(resposta);
    }
}

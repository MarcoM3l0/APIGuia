using APIGuia.Context;
using APIGuia.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace APIGuia.Controllers;
[ApiController]
[Route("[controller]")]
public class FaturamentoController : ControllerBase
{
    private readonly APIGuiaDBContext _context;
    private readonly IMemoryCache _cache;

    // Injeção de dependência do contexto do banco de dados e do cache
    public FaturamentoController(APIGuiaDBContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }


    // Endpoint para obter o faturamento mensal
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<FaturamentoDTO>> GetFaturamentoMensal(

    // Parâmetros da consulta
    [FromQuery] int ano,
    [FromQuery] int mes)

    {
        if (ano < 2000 || mes < 1 || mes > 12)
        {
            return BadRequest("Ano ou mês inválido.");
        }

        string cacheKey = $"faturamento_{ano}_{mes}"; // Chave única para o cache baseada no ano e mês

        // Tenta obter os dados do cache
        // Se não estiver no cache, consulta o banco de dados
        if (!_cache.TryGetValue(cacheKey, out FaturamentoDTO? faturamento))
        {
            faturamento = await _context.Reservas // Filtra reservas pelo ano e mês
                .Where(r => r.DataEntrada.Year == ano && r.DataEntrada.Month == mes) // Agrupa por ano e mês
                .GroupBy(r => new { r.DataEntrada.Year, r.DataEntrada.Month })
                .Select(g => new FaturamentoDTO
                {
                    Ano = g.Key.Year,
                    Mes = g.Key.Month,
                    TotalReservas = g.Count(), // Conta o total de reservas
                    TotalFaturado = g.Sum(r => r.Valor) // Soma o valor faturado
                })
                .FirstOrDefaultAsync() ?? new FaturamentoDTO { Ano = ano, Mes = mes, TotalReservas = 0, TotalFaturado = 0 };


            // Armazena os dados no cache por 10 minutos
            _cache.Set(cacheKey, faturamento, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });
        }

        // Retorna os dados do faturamento
        return Ok(faturamento);
    }

}

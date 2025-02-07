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

    public FaturamentoController(APIGuiaDBContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<FaturamentoDTO>> GetFaturamentoMensal(
    [FromQuery] int ano,
    [FromQuery] int mes)
    {
        if (ano < 2000 || mes < 1 || mes > 12)
        {
            return BadRequest("Ano ou mês inválido.");
        }

        string cacheKey = $"faturamento_{ano}_{mes}";

        if (!_cache.TryGetValue(cacheKey, out FaturamentoDTO? faturamento))
        {
            faturamento = await _context.Reservas
                .Where(r => r.DataEntrada.Year == ano && r.DataEntrada.Month == mes)
                .GroupBy(r => new { r.DataEntrada.Year, r.DataEntrada.Month })
                .Select(g => new FaturamentoDTO
                {
                    Ano = g.Key.Year,
                    Mes = g.Key.Month,
                    TotalReservas = g.Count(),
                    TotalFaturado = g.Sum(r => r.Valor)
                })
                .FirstOrDefaultAsync() ?? new FaturamentoDTO { Ano = ano, Mes = mes, TotalReservas = 0, TotalFaturado = 0 };

            _cache.Set(cacheKey, faturamento, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });
        }

        return Ok(faturamento);
    }

}

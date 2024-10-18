using BancoChu.App.Caching;
using BancoChu.Domain.Interfaces.Handlers;
using BancoChu.Domain.Interfaces.Repositories;
using BancoChu.Dto.Commands;
using BancoChu.Dto.Responses;
using BancoChu.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BancoChu.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly IMemoryCache _cache;
    private readonly BancoChuContext _context;
    private readonly ITransacoesHandler _handler;
    private readonly ITransacoesRepository _repository;

    public TransacoesController(
        IMemoryCache cache,
        BancoChuContext context,
        ITransacoesHandler handler,
        ITransacoesRepository repository)
    {
        _cache = cache;
        _context = context;
        _handler = handler;
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> RealizarTransacao([FromBody] TransacaoCommand command)
    {
        var result = await _handler.Handle(command);
        return Ok(result);
    }

    [HttpGet("extrato")]
    public async Task<IActionResult> ObterTransacoesPorPeriodo(string dataInicio, string dataFim)
    {
        if (!_cache.TryGetValue(CacheKeys.Extrato, out List<TransacaoResponse> extratos))
        {
            // Valida se o formato da data inserida está correta e declara variáveis
            if (!DateOnly.TryParseExact(dataInicio, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None,
                    out var startDate) ||
                !DateOnly.TryParseExact(dataFim, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None,
                    out var endDate))
            {
                return BadRequest("Datas devem estar no formato dd-MM-aaaa");
            }

            if (startDate > endDate)
                return BadRequest("DataTransacao inicial não pode ser maior que a final");

            var startDateTime = startDate.ToDateTime(TimeOnly.MinValue);
            var endDateTime = endDate.ToDateTime(TimeOnly.MaxValue);

            var transacoes = await _repository.BuscarTransacoesPorPeriodo(startDateTime, endDateTime);

            if (!transacoes.Any())
                return NotFound("Nenhuma transação realizada nesse período");

            extratos = new List<TransacaoResponse>();

            transacoes.ForEach(c =>
                extratos.Add(new TransacaoResponse(c.ContaOrigemId, c.ContaDestinoId, c.Valor, c.DataFormatada)));

            _cache.Set(CacheKeys.Extrato, extratos,
                new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(60)));
        }

        return Ok(extratos);
    }
}
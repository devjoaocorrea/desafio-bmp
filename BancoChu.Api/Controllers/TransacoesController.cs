using BancoChu.Domain.Entidades;
using BancoChu.Domain.Interfaces.Handlers;
using BancoChu.Dto.Commands;
using BancoChu.Dto.Responses;
using BancoChu.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoChu.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly BancoChuContext _context;
    private readonly ITransacoesHandler _handler;

    public TransacoesController(BancoChuContext context,
        ITransacoesHandler handler)
    {
        _context = context;
        _handler = handler;
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
        // Valida se o formato da data inserida está correta e declara variáveis
        if (!DateOnly.TryParseExact(dataInicio, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out var startDate) ||
            !DateOnly.TryParseExact(dataFim, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out var endDate))
        {
            return BadRequest("Datas devem estar no formato dd-MM-aaaa");
        }

        if (startDate > endDate)
            return BadRequest("DataTransacao inicial não pode ser maior que a final");

        var startDateTime = startDate.ToDateTime(TimeOnly.MinValue);
        var endDateTime = endDate.ToDateTime(TimeOnly.MaxValue);

        var transacoes = await _context.Transacoes
            .Include(t => t.ContaOrigem)
            .Include(t => t.ContaDestino)
            .Where(t => t.Data >= startDateTime && t.Data <= endDateTime)
            .ToListAsync();

        if (!transacoes.Any())
            return NotFound("Nenhuma transação realizada nesse período");

        var response = new List<TransacaoResponse>();

        transacoes.ForEach(c => response.Add(new TransacaoResponse(c.ContaOrigemId, c.ContaDestinoId, c.Valor, c.DataFormatada)));

        return Ok(response);
    }

    [HttpGet("todos-extratos")]
    public async Task<IActionResult> ObterTransacoes()
    {
        var transacoes = await _context.Transacoes.ToListAsync();

        return Ok(transacoes);
    }
}

using BancoChu.Domain.Entidades;
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

    public TransacoesController(BancoChuContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> RealizarTransacao([FromBody] TransacaoCommand command)
    {
        // Pega a conta sem fazer tracking.
        var contaOrigem = await _context.Contas
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == command.ContaOrigemId);

        var contaDestino = await _context.Contas
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == command.ContaDestinoId);

        if (contaOrigem is null || contaDestino is null)
            return NotFound("ContaOrigem não encontrada");

        if (contaOrigem.TemSaldoSuficiente(command.Valor))
            return BadRequest("Saldo insuficiente");

        // Atualiza o saldo das contas
        contaOrigem.Saldo += -command.Valor;
        contaDestino.Saldo += command.Valor;

        // Anexa as contas ao contexto para serem modificados
        _context.Entry(contaOrigem).State = EntityState.Modified;
        _context.Entry(contaDestino).State = EntityState.Modified;

        Transacao transacao = new(
            command.ContaOrigemId,
            contaOrigem,
            command.ContaDestinoId,
            contaDestino,
            command.Valor,
            command.DataTransacao);

        // Adiciona e salva a transacao
        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();

        return Ok(new TransacaoResponse(transacao.ContaOrigemId, transacao.ContaDestinoId, transacao.Valor, transacao.DataFormatada));
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

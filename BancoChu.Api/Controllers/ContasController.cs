using BancoChu.Domain.Entidades;
using BancoChu.Dto.Commands;
using BancoChu.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoChu.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContasController : ControllerBase
{
    private readonly BancoChuContext _context;

    public ContasController(BancoChuContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarConta([FromBody] ContaCommand command)
    {
        Conta conta = new(
            command.NumeroConta,
            command.Agencia,
            command.Titular,
            command.Saldo);

        _context.Contas.Add(conta);
        await _context.SaveChangesAsync();

        return Ok(conta.Id);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterContaPorId(Guid id)
    {
        var conta = await _context.Contas.FindAsync(id);
        return conta == null ? NotFound() : Ok(conta);
    }

    [HttpGet]
    public async Task<IActionResult> VisualizarContas() => Ok(await _context.Contas.ToListAsync());
}

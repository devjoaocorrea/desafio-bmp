using BancoChu.Domain.Entidades;
using BancoChu.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BancoChu.Infra.Repositories;

public class ContasRepository : IContasRepository
{
    private readonly BancoChuContext _context;

    public ContasRepository(BancoChuContext context)
    {
        _context = context;
    }

    public async Task<Conta> BuscarPorNumeroEAgencia(string numero, string agencia)
    {
        return await _context.Contas
            .AsNoTracking()
            .FirstOrDefaultAsync(c =>
                c.NumeroConta == numero
				&& c.Agencia == agencia);
    }

    public void AtualizarSaldos(IEnumerable<Conta> contas)
    {
        foreach (var conta in contas)
        {
            _context.Entry(conta).State = EntityState.Modified;
        }
    }
}
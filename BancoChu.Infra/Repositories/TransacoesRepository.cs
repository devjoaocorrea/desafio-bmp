using BancoChu.Domain.Entidades;
using BancoChu.Domain.Interfaces.Repositories;

namespace BancoChu.Infra.Repositories;

public class TransacoesRepository : ITransacoesRepository
{
	private readonly BancoChuContext _context;

	public TransacoesRepository(BancoChuContext context)
	{
		_context = context;
	}

	public async Task SalvarTransacao(Transacao transacao)
	{
		_context.Transacoes.Add(transacao);
		await _context.SaveChangesAsync();
	}
}

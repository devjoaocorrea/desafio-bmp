using BancoChu.Domain.Entidades;
using BancoChu.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BancoChu.Infra.Repositories;

public class TransacoesRepository : ITransacoesRepository
{
	private readonly BancoChuContext _context;

	public TransacoesRepository(BancoChuContext context)
	{
		_context = context;
	}

	public async Task<List<Transacao>> BuscarTransacoesPorPeriodo(DateTime dataInicio, DateTime dataFim)
	{
		return await _context.Transacoes
				.Include(t => t.ContaOrigem)
				.Include(t => t.ContaDestino)
				.Where(t => t.Data >= dataInicio && t.Data <= dataFim)
				.ToListAsync();
	}

	public async Task SalvarTransacao(Transacao transacao)
	{
		_context.Transacoes.Add(transacao);
		await _context.SaveChangesAsync();
	}
}

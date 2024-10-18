using BancoChu.Domain.Entidades;

namespace BancoChu.Domain.Interfaces.Repositories;

public interface ITransacoesRepository
{
	Task SalvarTransacao(Transacao transacao);
	Task<List<Transacao>> BuscarTransacoesPorPeriodo(DateTime dataInicio, DateTime dataFim);
}

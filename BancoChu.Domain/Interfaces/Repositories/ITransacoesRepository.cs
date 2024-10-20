using BancoChu.Domain.Entidades;
using BancoChu.Dto.Responses;

namespace BancoChu.Domain.Interfaces.Repositories;

public interface ITransacoesRepository
{
	Task SalvarTransacao(Transacao transacao);
	Task<List<TransacaoResponse>> BuscarTransacoesPorPeriodo(DateTime dataInicio, DateTime dataFim);
}

using BancoChu.Domain.Entidades;
using BancoChu.Domain.Interfaces.Handlers;
using BancoChu.Domain.Interfaces.Repositories;
using BancoChu.Dto.Commands;
using BancoChu.Dto.Responses;

namespace BancoChu.Domain.Handlers;

public class TransacoesHandler : ITransacoesHandler
{
	private readonly ITransacoesRepository _repository;
	private readonly IContasRepository _contasRepository;

	public TransacoesHandler(ITransacoesRepository repository,
		IContasRepository contasRepository)
	{
		_repository = repository;
		_contasRepository = contasRepository;
	}
	
	public async Task<TransacaoResponse> Handle(TransacaoCommand command)
	{
		// Pega a conta sem fazer tracking.
		var contaOrigem = await _contasRepository.BuscarPorIdAsync(command.ContaOrigemId);
		
		if (contaOrigem is null)
			return new TransacaoResponse(false, "Conta origem não encontrada");
		
		var contaDestino = await _contasRepository.BuscarPorIdAsync(command.ContaDestinoId);

		if (contaDestino is null)
			return new TransacaoResponse(false, "Conta destino não encontrada");
		
		if (!contaOrigem.TemSaldoSuficiente(command.Valor))
			return new TransacaoResponse(false, "Saldo insuficiente");

		// Atualiza o saldo das contas
		contaOrigem.Saldo += -command.Valor;
		contaDestino.Saldo += command.Valor;

		// Anexa as contas ao contexto para serem modificados
		var contas = new List<Conta>
		{
			contaOrigem,
			contaDestino
		};
		await _contasRepository.AtualizarSaldos(contas);
		
		Transacao transacao = new(
			command.ContaOrigemId,
			contaOrigem,
			command.ContaDestinoId,
			contaDestino,
			command.Valor,
			command.DataTransacao);

		// Adiciona e salva a transacao
		await _repository.SalvarTransacao(transacao);
		
		return new TransacaoResponse(transacao.ContaOrigemId, transacao.ContaDestinoId, transacao.Valor, transacao.DataFormatada);
	}
}

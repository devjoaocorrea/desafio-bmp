using BancoChu.Domain.Entidades;
using BancoChu.Domain.Interfaces.Handlers;
using BancoChu.Domain.Interfaces.Repositories;
using BancoChu.Dto.Commands;
using BancoChu.Dto.Responses;
using System.Net.Http.Json;

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
		// Busca a conta sem fazer tracking
		var contaOrigem = await _contasRepository.BuscarPorIdAsync(command.ContaOrigemId);

		if (contaOrigem is null)
		{
			return InvalidResponse("Conta origem não encontrada");
		}

		var contaDestino = await _contasRepository.BuscarPorIdAsync(command.ContaDestinoId);

		if (contaDestino is null)
		{
			return InvalidResponse("Conta destino não encontrada");
		}

		if (!contaOrigem.TemSaldoSuficiente(command.Valor))
		{
			InvalidResponse("Saldo insuficiente");
		}

		// Verifica se a data esta valida para fazer a transacao
		if (!await IsDataTransacaoValida(command.DataTransacao))
		{
			return InvalidResponse("A transação deve ser feita em dias úteis");
		}

		// Atualiza o saldo das contas
		contaOrigem.AlterarSaldo(contaOrigem.Saldo + (-command.Valor));
		contaDestino.AlterarSaldo(contaDestino.Saldo + command.Valor);

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

		return new TransacaoResponse
		{
			IsOk = true,
			Mensagem = "Transferência realizada com sucesso!",
			ContaOrigemId = transacao.ContaOrigemId,
			ContaDestinoId = transacao.ContaDestinoId,
			DataTransacao = transacao.DataFormatada,
			Valor = transacao.Valor
		};
	}

	private static TransacaoResponse InvalidResponse(string reason)
		=> new() { IsOk = false, Mensagem = reason };

	private static async Task<bool> IsDataTransacaoValida(DateTime dataTransacao)
	{
		if (dataTransacao.DayOfWeek == DayOfWeek.Sunday || dataTransacao.DayOfWeek == DayOfWeek.Saturday)
		{
			return false;
		}

		var url = $"https://brasilapi.com.br/api/feriados/v1/{dataTransacao.Year}";

		using var client = new HttpClient();
		var response = await client.GetAsync(url);

		if (!response.IsSuccessStatusCode)
		{
			throw new Exception("Não foi possível obter informações de feriados.");
		}

		var feriados = await response.Content.ReadFromJsonAsync<List<Feriado>>()
			?? throw new Exception("Falha ao obter lista de feriados.");

		// Verifica se a data da transacao esta em um feriado
		return !feriados.Any(feriado => DateTime.Parse(feriado.date) == dataTransacao.Date);
	}

	private record Feriado(string date, string name);
}

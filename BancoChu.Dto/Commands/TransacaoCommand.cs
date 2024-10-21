namespace BancoChu.Dto.Commands;

public class TransacaoCommand
{
	public string NumeroContaOrigem { get; init; }

	public string AgenciaOrigem { get; init; }

	public string NumeroContaDestino { get; init; }

	public string AgenciaDestino { get; init; }

	public string ChavePix { get; init; }

	public int TipoTransacao { get; init; }

	public decimal Valor { get; init; }

	public DateTime DataTransacao { get; init; }
}
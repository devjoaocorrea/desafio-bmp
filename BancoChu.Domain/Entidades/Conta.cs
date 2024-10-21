namespace BancoChu.Domain.Entidades;

public class Conta : EntidadeBase
{
	public Conta(string numeroConta, string agencia, string titular, string documento, decimal saldo, string chavePix = null)
	{
		NumeroConta = numeroConta;
		Agencia = agencia;
		Titular = titular;
		Saldo = saldo;
		Documento = documento;
		ChavePix = chavePix;
	}

	public Conta() { }

	public string NumeroConta { get; init; }

	public string Agencia { get; init; }

	public string Titular { get; init; }

	public string Documento { get; init; }

	public string ChavePix { get; init; }

	public decimal Saldo { get; private set; } = 0;

	public bool TemSaldoSuficiente(decimal valorTransferencia) => Saldo >= valorTransferencia;

	public void AlterarSaldo(decimal novoSaldo) => Saldo = novoSaldo;
}

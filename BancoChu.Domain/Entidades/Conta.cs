using System.ComponentModel.DataAnnotations;

namespace BancoChu.Domain.Entidades;

public class Conta : EntidadeBase
{
    public Conta(string numeroConta, string agencia, string titular, decimal saldo)
    {
        NumeroConta = numeroConta;
        Agencia = agencia;
        Titular = titular;
        Saldo = saldo;
    }

    public Conta() { }

    [Required]
    public string NumeroConta { get; init; }

    [Required]
    public string Agencia { get; init; }

    [Required]
    public string Titular { get; init; }

    public decimal Saldo { get; set; } = 0;

    public bool TemSaldoSuficiente(decimal valorTransferencia) => Saldo >= valorTransferencia;
}

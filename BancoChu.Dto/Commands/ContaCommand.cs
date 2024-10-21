namespace BancoChu.Dto.Commands;

public class ContaCommand
{
    public string Documento { get; init; }

    public string ChavePix { get; init; }

    public string NumeroConta { get; init; }

    public string Agencia { get; init; }

    public string Titular { get; init; }

    public decimal Saldo { get; init; }
}
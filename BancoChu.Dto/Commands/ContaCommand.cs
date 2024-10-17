namespace BancoChu.Dto.Commands;

public class ContaCommand
{
    public string NumeroConta { get; init; }
    public string Agencia { get; init; }
    public string Titular { get; init; }
    public decimal Saldo { get; init; }
}
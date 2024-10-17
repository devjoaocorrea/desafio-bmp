namespace BancoChu.Dto.Commands;

public class TransacaoCommand
{
    public Guid ContaOrigemId { get; init; }
    public Guid ContaDestinoId { get; init; }
    public decimal Valor { get; init; }
    public DateTime DataTransacao { get; init; }
}
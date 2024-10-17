namespace BancoChu.Dto.Responses;

public class TransacaoResponse
{
    public TransacaoResponse(Guid contaOrigemId, Guid contaDestinoId, decimal valor, string dataTransacao)
    {
        ContaOrigemId = contaOrigemId;
        ContaDestinoId = contaDestinoId;
        Valor = valor;
        DataTransacao = dataTransacao;
    }

    public Guid ContaOrigemId { get; set; }
    public Guid ContaDestinoId { get; set; }
    public decimal Valor { get; set; }
    public string DataTransacao { get; set; }

}

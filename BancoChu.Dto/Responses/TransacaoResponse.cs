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

    public TransacaoResponse(bool isOk, string mensagem)
    {
        IsOk = isOk;
        Mensagem = mensagem;
    }

    public bool IsOk { get; set; }
    public string Mensagem { get; set; }

    public Guid ContaOrigemId { get; set; }
    public Guid ContaDestinoId { get; set; }
    public decimal Valor { get; set; }
    public string DataTransacao { get; set; }

}

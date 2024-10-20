namespace BancoChu.Dto.Responses;

public class TransacaoResponse
{
    public bool IsOk { get; set; }
    public string Mensagem { get; set; }

    public Guid ContaOrigemId { get; set; }
    public Guid ContaDestinoId { get; set; }
    public decimal Valor { get; set; }
    public string DataTransacao { get; set; }

}

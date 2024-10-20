namespace BancoChu.Domain.Entidades;

public class Transacao : EntidadeBase
{
    public Transacao(Guid contaOrigemId, Conta contaOrigem, Guid contaDestinoId, Conta contaDestino, decimal valor, DateTime data)
    {
        ContaOrigemId = contaOrigemId;
        ContaOrigem = contaOrigem;
        ContaDestinoId = contaDestinoId;
        ContaDestino = contaDestino;
        Valor = valor;
        Data = data;
    }

    public Transacao() { }

    public Guid ContaOrigemId { get; init; }
    public Conta ContaOrigem { get; init; }
    public Guid ContaDestinoId { get; init; }
    public Conta ContaDestino { get; init; }
    public decimal Valor { get; init; }
    public DateTime Data { get; init; }
    public string Mensagem { get; set; }
    public bool Sucesso { get; set; }
    
    public string DataFormatada => Data.ToString("dd/MM/yyyy HH:mm:ss");
}

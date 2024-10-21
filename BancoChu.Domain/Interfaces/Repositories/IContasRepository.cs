using BancoChu.Domain.Entidades;

namespace BancoChu.Domain.Interfaces.Repositories;

public interface IContasRepository
{
    Task<Conta> BuscarPorNumeroEAgencia(string numero, string agencia);
    void AtualizarSaldos(IEnumerable<Conta> contas);
}
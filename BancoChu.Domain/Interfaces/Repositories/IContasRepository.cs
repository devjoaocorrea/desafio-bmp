using BancoChu.Domain.Entidades;

namespace BancoChu.Domain.Interfaces.Repositories;

public interface IContasRepository
{
    Task<Conta> BuscarPorIdAsync(Guid id);
    Task AtualizarSaldos(IEnumerable<Conta> contas);
}
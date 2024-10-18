using BancoChu.Dto.Commands;
using BancoChu.Dto.Responses;

namespace BancoChu.Domain.Interfaces.Handlers;

public interface ITransacoesHandler
{
    Task<TransacaoResponse> Handle(TransacaoCommand command);
}

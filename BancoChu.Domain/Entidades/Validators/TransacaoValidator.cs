using BancoChu.Dto.Commands;
using FluentValidation;

namespace BancoChu.Domain.Entidades.Validators;

public class TransacaoValidator : AbstractValidator<TransacaoCommand>
{
	public TransacaoValidator()
	{
		RuleFor(x => x.Valor)
			.GreaterThan(0)
			.WithMessage("Não pode realizar uma transação com o valor zerado");

		RuleFor(x => x.ContaOrigemId)
			.NotEmpty()
			.WithMessage("Conta de origem não informada");
		
		RuleFor(x => x.ContaDestinoId)
			.NotEmpty()
			.WithMessage("Conta de destino não informada");
	}
}

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

		RuleFor(x => x.NumeroContaOrigem)
			.NotEmpty()
			.WithMessage("Número da conta origem não informado");
		
		RuleFor(x => x.AgenciaOrigem)
			.NotEmpty()
			.WithMessage("Agência origem não informada");

		RuleFor(x => x.NumeroContaDestino)
			.NotEmpty()
			.WithMessage("Número da conta destino não informado");

		RuleFor(x => x.AgenciaDestino)
			.NotEmpty()
			.WithMessage("Agência destino não informada");
	}
}

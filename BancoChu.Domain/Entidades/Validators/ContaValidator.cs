using FluentValidation;

namespace BancoChu.Domain.Entidades.Validators;

public class ContaValidator : AbstractValidator<Conta>
{
	public ContaValidator()
	{
		RuleFor(x => x.Titular)
			.NotEmpty().WithMessage("O titular deve ser preenchido")
			.Length(3, 20)
			.WithMessage("O titular deve ter entre 3 até 20 caracteres");

		RuleFor(x => x.Agencia)
			.NotEmpty()
			.WithMessage("A agência deve ser informado");

		RuleFor(x => x.NumeroConta)
			.NotEmpty()
			.WithMessage("O número da conta deve ser informado");

		RuleFor(x => x.Saldo)
			.GreaterThan(0)
			.WithMessage("O valor do saldo não pode ser negativo ao criar a conta");

		RuleFor(x => x.Documento)
			.NotEmpty()
			.WithMessage("A precisa ter um documento");
	}
}

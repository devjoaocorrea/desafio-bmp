using BancoChu.Domain.Entidades;
using BancoChu.Domain.Entidades.Validators;
using FluentValidation.TestHelper;

namespace BancoChu.Tests.Contas;

public class ContasTests
{
	private readonly ContaValidator _validator;

	public ContasTests()
	{
		_validator = new ContaValidator();
	}

	[Fact]
	public void Deve_RetornarErro_Quando_TitularNaoForInformado()
	{
		// Arrange
		var command = new Conta { Titular = "" };

		// Act e Assert
		var result = _validator.TestValidate(command);
		result.ShouldHaveValidationErrorFor(x => x.Titular)
			  .WithErrorMessage("O titular deve ser preenchido");
	}

	[Fact]
	public void Deve_RetornarErro_Quando_TitularNaoTiverEntre3e20Caracteres()
	{
		// Arrange
		var command = new Conta { Titular = "Jo" };

		// Act e Assert
		var result = _validator.TestValidate(command);
		result.ShouldHaveValidationErrorFor(x => x.Titular)
			  .WithErrorMessage("O titular deve ter entre 3 até 20 caracteres");
	}

	[Fact]
	public void Nao_DeveRetornarErro_Quando_TitularForValido()
	{
		// Arrange
		var command = new Conta { Titular = "Joao Silva" };

		// Act e Assert
		var result = _validator.TestValidate(command);
		result.ShouldNotHaveValidationErrorFor(x => x.Titular);
	}

	[Fact]
	public void Deve_RetornarErro_Quando_AgenciaNaoForInformada()
	{
		// Arrange
		var command = new Conta { Agencia = "" };

		// Act e Assert
		var result = _validator.TestValidate(command);
		result.ShouldHaveValidationErrorFor(x => x.Agencia)
			  .WithErrorMessage("A agência deve ser informado");
	}

	[Fact]
	public void Nao_DeveRetornarErro_Quando_AgenciaForInformada()
	{
		// Arrange
		var command = new Conta { Agencia = "1234" };

		// Act e Assert
		var result = _validator.TestValidate(command);
		result.ShouldNotHaveValidationErrorFor(x => x.Agencia);
	}

	[Fact]
	public void Deve_RetornarErro_Quando_NumeroContaNaoForInformado()
	{
		// Arrange
		var command = new Conta { NumeroConta = "" };

		// Act e Assert
		var result = _validator.TestValidate(command);
		result.ShouldHaveValidationErrorFor(x => x.NumeroConta)
			  .WithErrorMessage("O número da conta deve ser informado");
	}

	[Fact]
	public void Nao_DeveRetornarErro_Quando_NumeroContaForInformado()
	{
		// Arrange
		var command = new Conta { NumeroConta = "123456" };

		// Act e Assert
		var result = _validator.TestValidate(command);
		result.ShouldNotHaveValidationErrorFor(x => x.NumeroConta);
	}

	[Fact]
	public void Deve_RetornarErro_Quando_SaldoForNegativo()
	{
		// Arrange
		var command = new Conta();
		command.AlterarSaldo(-1);

		// Act e Assert
		var result = _validator.TestValidate(command);
		result.ShouldHaveValidationErrorFor(x => x.Saldo)
			  .WithErrorMessage("O valor do saldo não pode ser negativo ao criar a conta");
	}

	[Fact]
	public void Nao_DeveRetornarErro_Quando_SaldoForPositivo()
	{
		// Arrange
		var command = new Conta();
		command.AlterarSaldo(100);

		// Act e Assert
		var result = _validator.TestValidate(command);
		result.ShouldNotHaveValidationErrorFor(x => x.Saldo);
	}
}

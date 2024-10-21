using BancoChu.Domain.Entidades.Validators;
using BancoChu.Dto.Commands;
using FluentValidation.TestHelper;

namespace BancoChu.Tests.Transacoes;

public class TransacoesTests
{
	private readonly TransacaoValidator _validator;

	public TransacoesTests()
	{
		_validator = new();

	}

	[Fact]
	public void Deve_RetornarErro_Quando_ValorForZero()
	{
		// Arrange
		var command = new TransacaoCommand { Valor = 0 };

		// Act e Assert
		var result = _validator.TestValidate(command);
		result.ShouldHaveValidationErrorFor(x => x.Valor)
			  .WithErrorMessage("Não pode realizar uma transação com o valor zerado");
	}

	[Fact]
	public void Deve_RetornarErro_Quando_ValorForNegativo()
	{
		// Arrange
		var command = new TransacaoCommand { Valor = -20 };

		// Act e Assert
		var result = _validator.TestValidate(command);
		result.ShouldHaveValidationErrorFor(x => x.Valor)
			  .WithErrorMessage("Não pode realizar uma transação com o valor zerado");
	}

	[Fact]
	public void Nao_Deve_RetornarErro_Qundo_ValorForPositivo()
	{
		// Arrange
		var command = new TransacaoCommand { Valor = 100 };

		// Act e Assert
		var result = _validator.TestValidate(command);
		result.ShouldNotHaveValidationErrorFor(x => x.Valor);
	}

	[Fact]
	public void Deve_RetornarErro_Quando_NumeroContaNaoForInformado()
	{
		// Arrange
		var command = new TransacaoCommand { NumeroContaOrigem = null };

		// Act e Assert
		var result = _validator.TestValidate(command);
		result.ShouldHaveValidationErrorFor(x => x.NumeroContaOrigem)
			  .WithErrorMessage("Número da conta não informado");
	}

	[Fact]
	public void Nao_DeveRetornarErro_Quando_NumeroContaForInformada()
	{
		// Arrange
		var command = new TransacaoCommand { NumeroContaOrigem = "123456" };

		// Act e Assert
		var result = _validator.TestValidate(command);
		result.ShouldNotHaveValidationErrorFor(x => x.NumeroContaOrigem);
	}

	[Fact]
	public void Deve_RetornarErro_Quando_AgenciaNaoForInformada()
	{
		// Arrange
		var command = new TransacaoCommand { AgenciaOrigem = null };

		// Act e Assert
		var result = _validator.TestValidate(command);
		result.ShouldHaveValidationErrorFor(x => x.AgenciaOrigem)
			  .WithErrorMessage("Agência não informada");
	}

	[Fact]
	public void Nao_DeveRetornarErro_Quando_AgenciaForInformada()
	{
		// Arrange
		var command = new TransacaoCommand { AgenciaOrigem = "0001" };

		// Act e Assert
		var result = _validator.TestValidate(command);
		result.ShouldNotHaveValidationErrorFor(x => x.AgenciaOrigem);
	}
}

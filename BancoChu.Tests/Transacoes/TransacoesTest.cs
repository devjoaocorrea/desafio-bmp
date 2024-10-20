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
    public void Deve_RetornarErro_Quando_ContaOrigemIdNaoForInformada()
    {
        // Arrange
        var command = new TransacaoCommand { ContaOrigemId = Guid.Empty };

        // Act e Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ContaOrigemId)
              .WithErrorMessage("Conta de origem não informada");
    }

    [Fact]
    public void Nao_DeveRetornarErro_Quando_ContaOrigemIdForInformada()
    {
        // Arrange
        var command = new TransacaoCommand { ContaOrigemId = Guid.NewGuid() };

        // Act e Assert
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.ContaOrigemId);
    }

    [Fact]
    public void Deve_RetornarErro_Quando_ContaDestinoIdNaoForInformada()
    {
        // Arrange
        var command = new TransacaoCommand { ContaDestinoId = Guid.Empty };

        // Act e Assert
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.ContaDestinoId)
              .WithErrorMessage("Conta de destino não informada");
    }

    [Fact]
    public void Nao_DeveRetornarErro_Quando_ContaDestinoIdForInformada()
    {
        // Arrange
        var command = new TransacaoCommand { ContaDestinoId = Guid.NewGuid() };

        // Act e Assert
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.ContaDestinoId);
    }
}

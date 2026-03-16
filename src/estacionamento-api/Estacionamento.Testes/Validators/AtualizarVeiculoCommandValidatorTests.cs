using Estacionamento.Application.Features.Veiculos.Commands.AtualizarVeiculo;
using Estacionamento.Domain.Enums;

namespace Estacionamento.Testes.Validators;

public class AtualizarVeiculoCommandValidatorTests
{
    [Fact]
    public void AtualizarVeiculoCommandValidator_NaoDeveAceitarDadosInvalidos()
    {
        AtualizarVeiculoCommandValidator validator;
        AtualizarVeiculoCommand command;
        var resultado = default(FluentValidation.Results.ValidationResult);

        validator = new AtualizarVeiculoCommandValidator();
        command = new AtualizarVeiculoCommand
        {
            Id = Guid.Empty,
            Descricao = " ",
            Marca = (EnumMarca)999,
            Modelo = "",
            Valor = 0m
        };

        resultado = validator.Validate(command);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, error => error.PropertyName == "Id");
        Assert.Contains(resultado.Errors, error => error.PropertyName == "Descricao");
        Assert.Contains(resultado.Errors, error => error.PropertyName == "Marca");
        Assert.Contains(resultado.Errors, error => error.PropertyName == "Modelo");
        Assert.Contains(resultado.Errors, error => error.PropertyName == "Valor");
    }
}

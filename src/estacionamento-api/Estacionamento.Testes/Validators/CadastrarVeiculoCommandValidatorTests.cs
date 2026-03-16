using Estacionamento.Application.Features.Veiculos.Commands.CadastrarVeiculo;
using Estacionamento.Domain.Enums;

namespace Estacionamento.Testes.Validators;

public class CadastrarVeiculoCommandValidatorTests
{
    [Fact]
    public void CadastrarVeiculoCommandValidator_DeveAceitarDadosValidos()
    {
        CadastrarVeiculoCommandValidator validator;
        CadastrarVeiculoCommand command;
        var resultado = default(FluentValidation.Results.ValidationResult);

        validator = new CadastrarVeiculoCommandValidator();
        command = new CadastrarVeiculoCommand
        {
            Descricao = "Fork Ka bolinha",
            Marca = EnumMarca.Ford,
            Modelo = "FA",
            Opcionais = "canela seca",
            Valor = 139900m
        };

        resultado = validator.Validate(command);

        Assert.True(resultado.IsValid);
    }

    [Fact]
    public void CadastrarVeiculoCommandValidator_NaoDeveAceitarDescricaoInvalida()
    {
        CadastrarVeiculoCommandValidator validator;
        CadastrarVeiculoCommand command;
        var resultado = default(FluentValidation.Results.ValidationResult);

        validator = new CadastrarVeiculoCommandValidator();
        command = new CadastrarVeiculoCommand
        {
            Descricao = "",
            Marca = EnumMarca.Ford,
            Modelo = "KA",
            Valor = 12900m
        };

        resultado = validator.Validate(command);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, error => error.PropertyName == "Descricao");
    }

    [Fact]
    public void CadastrarVeiculoCommandValidator_NaoDeveAceitarMarcaInvalida()
    {
        CadastrarVeiculoCommandValidator validator;
        CadastrarVeiculoCommand command;
        var resultado = default(FluentValidation.Results.ValidationResult);

        validator = new CadastrarVeiculoCommandValidator();
        command = new CadastrarVeiculoCommand
        {
            Descricao = "Ford KA",
            Marca = (EnumMarca)999,
            Modelo = "KA",
            Valor = 11900m
        };

        resultado = validator.Validate(command);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, error => error.PropertyName == "Marca");
    }

    [Fact]
    public void CadastrarVeiculoCommandValidator_NaoDeveAceitarModeloInvalido()
    {
        CadastrarVeiculoCommandValidator validator;
        CadastrarVeiculoCommand command;
        var resultado = default(FluentValidation.Results.ValidationResult);

        validator = new CadastrarVeiculoCommandValidator();
        command = new CadastrarVeiculoCommand
        {
            Descricao = "Ford KA",
            Marca = EnumMarca.Ford,
            Modelo = "",
            Valor = 10000m
        };

        resultado = validator.Validate(command);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, error => error.PropertyName == "Modelo");
    }

    [Fact]
    public void CadastrarVeiculoCommandValidator_NaoDeveAceitarValorInvalido()
    {
        CadastrarVeiculoCommandValidator validator;
        CadastrarVeiculoCommand command;
        var resultado = default(FluentValidation.Results.ValidationResult);

        validator = new CadastrarVeiculoCommandValidator();
        command = new CadastrarVeiculoCommand
        {
            Descricao = "Ford KA",
            Marca = EnumMarca.Ford,
            Modelo = "KA",
            Valor = -10m
        };

        resultado = validator.Validate(command);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, error => error.PropertyName == "Valor");
    }
}

using Estacionamento.Application.Features.Usuarios.Commands.CadastrarUsuario;

namespace Estacionamento.Testes.Validators;

public class CadastrarUsuarioCommandValidatorTests
{
    [Fact]
    public void CadastrarUsuarioCommandValidator_DeveAceitarDadosValidos()
    {
        CadastrarUsuarioCommandValidator validator;
        CadastrarUsuarioCommand command;
        var resultado = default(FluentValidation.Results.ValidationResult);

        validator = new CadastrarUsuarioCommandValidator();
        command = new CadastrarUsuarioCommand
        {
            Nome = "Anderson",
            Login = "anderson",
            Senha = "123456"
        };

        resultado = validator.Validate(command);

        Assert.True(resultado.IsValid);
    }

    [Fact]
    public void CadastrarUsuarioCommandValidator_NaoDeveAceitarNomeInvalido()
    {
        CadastrarUsuarioCommandValidator validator;
        CadastrarUsuarioCommand command;
        var resultado = default(FluentValidation.Results.ValidationResult);

        validator = new CadastrarUsuarioCommandValidator();
        command = new CadastrarUsuarioCommand
        {
            Nome = "  ",
            Login = "anderson",
            Senha = "123456"
        };

        resultado = validator.Validate(command);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, error => error.PropertyName == "Nome");
    }

    [Fact]
    public void CadastrarUsuarioCommandValidator_NaoDeveAceitarLoginInvalido()
    {
        CadastrarUsuarioCommandValidator validator;
        CadastrarUsuarioCommand command;
        var resultado = default(FluentValidation.Results.ValidationResult);

        validator = new CadastrarUsuarioCommandValidator();
        command = new CadastrarUsuarioCommand
        {
            Nome = "Anderson",
            Login = "an",
            Senha = "123456"
        };

        resultado = validator.Validate(command);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, error => error.PropertyName == "Login");
    }

    [Fact]
    public void CadastrarUsuarioCommandValidator_NaoDeveAceitarSenhaInvalida()
    {
        CadastrarUsuarioCommandValidator validator;
        CadastrarUsuarioCommand command;
        var resultado = default(FluentValidation.Results.ValidationResult);

        validator = new CadastrarUsuarioCommandValidator();
        command = new CadastrarUsuarioCommand
        {
            Nome = "Anderson",
            Login = "anderson",
            Senha = "123"
        };

        resultado = validator.Validate(command);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, error => error.PropertyName == "Senha");
    }
}

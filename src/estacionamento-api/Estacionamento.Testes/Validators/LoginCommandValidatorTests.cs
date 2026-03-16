using Estacionamento.Application.Features.Auth.Commands.Login;

namespace Estacionamento.Testes.Validators;

public class LoginCommandValidatorTests
{
    [Fact]
    public void LoginCommandValidator_DeveAceitarDadosValidos()
    {
        LoginCommandValidator validator;
        LoginCommand command;
        var resultado = default(FluentValidation.Results.ValidationResult);

        validator = new LoginCommandValidator();
        command = new LoginCommand
        {
            Login = "anderson",
            Senha = "senha123"
        };

        resultado = validator.Validate(command);

        Assert.True(resultado.IsValid);
    }

    [Fact]
    public void LoginCommandValidator_NaoDeveAceitarDadosInvalidos()
    {
        LoginCommandValidator validator;
        LoginCommand command;
        var resultado = default(FluentValidation.Results.ValidationResult);

        validator = new LoginCommandValidator();
        command = new LoginCommand
        {
            Login = " an",
            Senha = "123"
        };

        resultado = validator.Validate(command);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, error => error.PropertyName == "Login");
        Assert.Contains(resultado.Errors, error => error.PropertyName == "Senha");
    }
}

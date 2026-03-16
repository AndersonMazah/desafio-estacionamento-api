using FluentValidation;

namespace Estacionamento.Application.Features.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(command => command.Login)
            .NotEmpty()
            .MinimumLength(3)
            .Must(value => value.Trim() == value)
            .WithMessage("Login não deve conter espaços no início ou fim.");

        RuleFor(command => command.Senha)
            .NotEmpty()
            .MinimumLength(6);
    }
}

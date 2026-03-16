using FluentValidation;

namespace Estacionamento.Application.Features.Usuarios.Commands.CadastrarUsuario;

public class CadastrarUsuarioCommandValidator : AbstractValidator<CadastrarUsuarioCommand>
{
    public CadastrarUsuarioCommandValidator()
    {
        RuleFor(command => command.Nome)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50)
            .Must(value => value.Trim() == value)
            .WithMessage("Nome não deve conter espaços no início ou fim.");

        RuleFor(command => command.Login)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(20)
            .Must(value => value.Trim() == value)
            .WithMessage("Login não deve conter espaços no início ou fim.");

        RuleFor(command => command.Senha)
            .NotEmpty()
            .MinimumLength(6);
    }
}

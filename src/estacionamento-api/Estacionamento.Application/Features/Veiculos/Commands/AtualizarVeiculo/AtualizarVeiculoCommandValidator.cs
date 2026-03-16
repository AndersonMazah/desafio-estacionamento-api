using FluentValidation;

namespace Estacionamento.Application.Features.Veiculos.Commands.AtualizarVeiculo;

public class AtualizarVeiculoCommandValidator : AbstractValidator<AtualizarVeiculoCommand>
{
    public AtualizarVeiculoCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();

        RuleFor(command => command.Descricao)
            .NotEmpty()
            .MaximumLength(100)
            .Must(value => value.Trim() == value)
            .WithMessage("Descrição não deve conter espaços no início ou fim.");

        RuleFor(command => command.Marca)
            .IsInEnum();

        RuleFor(command => command.Modelo)
            .NotEmpty()
            .MaximumLength(30)
            .Must(value => value.Trim() == value)
            .WithMessage("Modelo não deve conter espaços no início ou fim.");

        RuleFor(command => command.Valor)
            .GreaterThan(0)
            .When(command => command.Valor.HasValue);
    }
}

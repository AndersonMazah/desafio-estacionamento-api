using FluentValidation;

namespace Estacionamento.Application.Features.Veiculos.Commands.ExcluirVeiculo;

public class ExcluirVeiculoCommandValidator : AbstractValidator<ExcluirVeiculoCommand>
{
    public ExcluirVeiculoCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}

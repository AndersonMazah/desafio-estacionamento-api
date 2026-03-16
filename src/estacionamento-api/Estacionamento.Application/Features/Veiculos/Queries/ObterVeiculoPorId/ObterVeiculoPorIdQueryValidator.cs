using FluentValidation;

namespace Estacionamento.Application.Features.Veiculos.Queries.ObterVeiculoPorId;

public class ObterVeiculoPorIdQueryValidator : AbstractValidator<ObterVeiculoPorIdQuery>
{
    public ObterVeiculoPorIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty();
    }
}

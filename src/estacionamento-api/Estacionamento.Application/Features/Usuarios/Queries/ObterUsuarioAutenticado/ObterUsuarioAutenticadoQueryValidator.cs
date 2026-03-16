using FluentValidation;

namespace Estacionamento.Application.Features.Usuarios.Queries.ObterUsuarioAutenticado;

public class ObterUsuarioAutenticadoQueryValidator : AbstractValidator<ObterUsuarioAutenticadoQuery>
{
    public ObterUsuarioAutenticadoQueryValidator()
    {
        RuleFor(query => query.UsuarioId)
            .NotEmpty();
    }
}

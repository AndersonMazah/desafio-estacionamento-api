using Estacionamento.Application.Common.Models;
using MediatR;

namespace Estacionamento.Application.Features.Usuarios.Queries.ObterUsuarioAutenticado;

public class ObterUsuarioAutenticadoQuery : IRequest<UsuarioModel>
{
    public Guid UsuarioId { get; set; }
}

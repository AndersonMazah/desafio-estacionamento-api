using Estacionamento.Application.Common.Models;
using Estacionamento.Application.Services.Usuarios;
using MediatR;

namespace Estacionamento.Application.Features.Usuarios.Queries.ObterUsuarioAutenticado;

public class ObterUsuarioAutenticadoQueryHandler : IRequestHandler<ObterUsuarioAutenticadoQuery, UsuarioModel>
{
    private readonly IUsuarioService _usuarioService;

    public ObterUsuarioAutenticadoQueryHandler(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public async Task<UsuarioModel> Handle(ObterUsuarioAutenticadoQuery request, CancellationToken cancellationToken)
    {
        return await _usuarioService.ObterPorIdAsync(request.UsuarioId, cancellationToken);
    }
}

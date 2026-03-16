using Estacionamento.Application.Common.Models;
using Estacionamento.Application.Services.Usuarios;
using MediatR;

namespace Estacionamento.Application.Features.Usuarios.Commands.CadastrarUsuario;

public class CadastrarUsuarioCommandHandler : IRequestHandler<CadastrarUsuarioCommand, UsuarioModel>
{
    private readonly IUsuarioService _usuarioService;

    public CadastrarUsuarioCommandHandler(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public async Task<UsuarioModel> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
    {
        return await _usuarioService.CadastrarAsync(request.Nome, request.Login, request.Senha, cancellationToken);
    }
}

using Estacionamento.Application.Common.Models;
using MediatR;

namespace Estacionamento.Application.Features.Usuarios.Commands.CadastrarUsuario;

public class CadastrarUsuarioCommand : IRequest<UsuarioModel>
{
    public string Nome { get; set; } = string.Empty;

    public string Login { get; set; } = string.Empty;

    public string Senha { get; set; } = string.Empty;
}

using Estacionamento.Application.Common.Models;
using MediatR;

namespace Estacionamento.Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<LoginModel>
{
    public string Login { get; set; } = string.Empty;

    public string Senha { get; set; } = string.Empty;
}

using Estacionamento.Application.Common.Models;
using Estacionamento.Application.Services.Auth;
using MediatR;

namespace Estacionamento.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginModel>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _authService.AutenticarAsync(request.Login, request.Senha, cancellationToken);
    }
}

using Estacionamento.Application.Common.Models;

namespace Estacionamento.Application.Services.Auth;

public interface IAuthService
{
    Task<LoginModel> AutenticarAsync(string login, string senha, CancellationToken cancellationToken);
}

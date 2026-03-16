namespace Estacionamento.Application.Abstractions.Authentication;

public interface IJwtTokenService
{
    JwtTokenResult GenerateToken(Guid userId);
}

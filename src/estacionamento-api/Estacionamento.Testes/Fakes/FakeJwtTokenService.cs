using Estacionamento.Application.Abstractions.Authentication;

namespace Estacionamento.Testes.Fakes;

public class FakeJwtTokenService : IJwtTokenService
{
    public Guid? UltimoUsuarioIdGerado { get; private set; }

    public JwtTokenResult GenerateToken(Guid userId)
    {
        UltimoUsuarioIdGerado = userId;

        return new JwtTokenResult
        {
            Token = $"token-{userId}",
            ExpiresAtUtc = DateTime.UtcNow.AddMinutes(30)
        };
    }
}

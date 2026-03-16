using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Estacionamento.WebApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUsuarioId(this ClaimsPrincipal principal)
    {
        string? usuarioId;
        usuarioId = principal.FindFirstValue(JwtRegisteredClaimNames.Sub);
        if (!Guid.TryParse(usuarioId, out Guid resultado))
        {
            throw new UnauthorizedAccessException("Token JWT inválido.");
        }
        return resultado;
    }

}

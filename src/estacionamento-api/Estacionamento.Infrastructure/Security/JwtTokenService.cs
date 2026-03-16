using Estacionamento.Application.Abstractions.Authentication;
using Estacionamento.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Estacionamento.Infrastructure.Security;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public JwtTokenResult GenerateToken(Guid userId)
    {
        JwtSecurityTokenHandler tokenHandler;
        byte[] key;
        DateTime expiresAtUtc;
        SecurityTokenDescriptor tokenDescriptor;
        SecurityToken token;

        tokenHandler = new JwtSecurityTokenHandler();
        key = Encoding.UTF8.GetBytes(_jwtOptions.SecretKey);
        expiresAtUtc = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);

        tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString())
            }),
            Expires = expiresAtUtc,
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        token = tokenHandler.CreateToken(tokenDescriptor);

        return new JwtTokenResult
        {
            Token = tokenHandler.WriteToken(token),
            ExpiresAtUtc = expiresAtUtc
        };
    }
}

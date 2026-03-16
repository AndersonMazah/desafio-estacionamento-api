using Estacionamento.Infrastructure.Options;
using Estacionamento.Infrastructure.Security;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace Estacionamento.Testes.Services;

public class JwtTokenServiceTests
{
    [Fact]
    public void JwtTokenService_DeveGerarTokenNaoVazio()
    {
        JwtTokenService jwtTokenService;
        Guid usuarioId;
        var resultado = default(Estacionamento.Application.Abstractions.Authentication.JwtTokenResult);

        jwtTokenService = CriarServico();
        usuarioId = Guid.NewGuid();

        resultado = jwtTokenService.GenerateToken(usuarioId);

        Assert.False(string.IsNullOrWhiteSpace(resultado.Token));
    }

    [Fact]
    public void JwtTokenService_DeveGerarTokenComIdentificadorDoUsuario()
    {
        JwtTokenService jwtTokenService;
        JwtSecurityTokenHandler tokenHandler;
        Guid usuarioId;
        var resultado = default(Estacionamento.Application.Abstractions.Authentication.JwtTokenResult);
        JwtSecurityToken token;
        string? subject;

        jwtTokenService = CriarServico();
        tokenHandler = new JwtSecurityTokenHandler();
        usuarioId = Guid.NewGuid();

        resultado = jwtTokenService.GenerateToken(usuarioId);
        token = tokenHandler.ReadJwtToken(resultado.Token);
        subject = token.Subject;

        Assert.Equal(usuarioId.ToString(), subject);
    }

    private static JwtTokenService CriarServico()
    {
        JwtOptions jwtOptions;

        jwtOptions = new JwtOptions
        {
            Issuer = "EstacionamentoApi",
            Audience = "EstacionamentoApi",
            SecretKey = "UmaChaveSuperSeguraComNoMinimo32Caracteres",
            ExpirationMinutes = 30
        };

        return new JwtTokenService(Options.Create(jwtOptions));
    }
}

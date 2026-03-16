using Estacionamento.Application.Abstractions.Authentication;
using Estacionamento.Application.Abstractions.Security;
using Estacionamento.Application.Common.Exceptions;
using Estacionamento.Application.Common.Models;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Interfaces;

namespace Estacionamento.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthService(
        IUsuarioRepository usuarioRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginModel> AutenticarAsync(string login, string senha, CancellationToken cancellationToken)
    {
        Usuario? usuario;
        JwtTokenResult tokenResult;

        usuario = await _usuarioRepository.ObterPorLoginAsync(login.Trim(), cancellationToken);

        if (usuario is null)
        {
            throw new UnauthorizedAppException("Credenciais inválidas.");
        }

        if (!_passwordHasher.VerifyPassword(senha, usuario.Senha))
        {
            throw new UnauthorizedAppException("Credenciais inválidas.");
        }

        tokenResult = _jwtTokenService.GenerateToken(usuario.Id);

        return new LoginModel
        {
            Token = tokenResult.Token
        };
    }
}

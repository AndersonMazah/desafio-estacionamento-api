using Estacionamento.Application.Common.Exceptions;
using Estacionamento.Application.Features.Auth.Commands.Login;
using Estacionamento.Application.Services.Auth;
using Estacionamento.Domain.Entities;
using Estacionamento.Infrastructure.Security;
using Estacionamento.Testes.Fakes;

namespace Estacionamento.Testes.Application.Auth;

public class LoginCommandHandlerTests
{
    [Fact]
    public async Task LoginCommandHandler_DeveAutenticarComCredenciaisValidas()
    {
        FakeUsuarioRepository usuarioRepository;
        BcryptPasswordHasher passwordHasher;
        FakeJwtTokenService jwtTokenService;
        AuthService authService;
        LoginCommandHandler handler;
        Usuario usuario;
        LoginCommand command;
        var resultado = default(Estacionamento.Application.Common.Models.LoginModel);

        usuarioRepository = new FakeUsuarioRepository();
        passwordHasher = new BcryptPasswordHasher();
        jwtTokenService = new FakeJwtTokenService();
        authService = new AuthService(usuarioRepository, passwordHasher, jwtTokenService);
        handler = new LoginCommandHandler(authService);
        usuario = new Usuario(Guid.NewGuid(), "Anderson", "anderson", passwordHasher.HashPassword("senha123"));
        command = new LoginCommand
        {
            Login = "anderson",
            Senha = "senha123"
        };

        await usuarioRepository.AdicionarAsync(usuario, CancellationToken.None);
        resultado = await handler.Handle(command, CancellationToken.None);

        Assert.Equal($"token-{usuario.Id}", resultado.Token);
        Assert.Equal(usuario.Id, jwtTokenService.UltimoUsuarioIdGerado);
    }

    [Fact]
    public async Task LoginCommandHandler_DeveFalharQuandoLoginNaoExistir()
    {
        FakeUsuarioRepository usuarioRepository;
        BcryptPasswordHasher passwordHasher;
        FakeJwtTokenService jwtTokenService;
        AuthService authService;
        LoginCommandHandler handler;
        LoginCommand command;

        usuarioRepository = new FakeUsuarioRepository();
        passwordHasher = new BcryptPasswordHasher();
        jwtTokenService = new FakeJwtTokenService();
        authService = new AuthService(usuarioRepository, passwordHasher, jwtTokenService);
        handler = new LoginCommandHandler(authService);
        command = new LoginCommand
        {
            Login = "naoexiste",
            Senha = "senha123"
        };

        await Assert.ThrowsAsync<UnauthorizedAppException>(async () =>
        {
            await handler.Handle(command, CancellationToken.None);
        });
    }

    [Fact]
    public async Task LoginCommandHandler_DeveFalharQuandoSenhaEstiverIncorreta()
    {
        FakeUsuarioRepository usuarioRepository;
        BcryptPasswordHasher passwordHasher;
        FakeJwtTokenService jwtTokenService;
        AuthService authService;
        LoginCommandHandler handler;
        Usuario usuario;
        LoginCommand command;

        usuarioRepository = new FakeUsuarioRepository();
        passwordHasher = new BcryptPasswordHasher();
        jwtTokenService = new FakeJwtTokenService();
        authService = new AuthService(usuarioRepository, passwordHasher, jwtTokenService);
        handler = new LoginCommandHandler(authService);
        usuario = new Usuario(Guid.NewGuid(), "Anderson", "anderson", passwordHasher.HashPassword("senha123"));
        command = new LoginCommand
        {
            Login = "anderson",
            Senha = "senhaErrada"
        };

        await usuarioRepository.AdicionarAsync(usuario, CancellationToken.None);

        await Assert.ThrowsAsync<UnauthorizedAppException>(async () =>
        {
            await handler.Handle(command, CancellationToken.None);
        });
    }

    [Fact]
    public async Task LoginCommandHandler_DeveRetornarTokenJwtEmCasoDeSucesso()
    {
        FakeUsuarioRepository usuarioRepository;
        BcryptPasswordHasher passwordHasher;
        FakeJwtTokenService jwtTokenService;
        AuthService authService;
        LoginCommandHandler handler;
        Usuario usuario;
        LoginCommand command;
        var resultado = default(Estacionamento.Application.Common.Models.LoginModel);

        usuarioRepository = new FakeUsuarioRepository();
        passwordHasher = new BcryptPasswordHasher();
        jwtTokenService = new FakeJwtTokenService();
        authService = new AuthService(usuarioRepository, passwordHasher, jwtTokenService);
        handler = new LoginCommandHandler(authService);
        usuario = new Usuario(Guid.NewGuid(), "Carlos Lima", "carlos", passwordHasher.HashPassword("senha123"));
        command = new LoginCommand
        {
            Login = "carlos",
            Senha = "senha123"
        };

        await usuarioRepository.AdicionarAsync(usuario, CancellationToken.None);
        resultado = await handler.Handle(command, CancellationToken.None);

        Assert.False(string.IsNullOrWhiteSpace(resultado.Token));
        Assert.StartsWith("token-", resultado.Token);
    }
}

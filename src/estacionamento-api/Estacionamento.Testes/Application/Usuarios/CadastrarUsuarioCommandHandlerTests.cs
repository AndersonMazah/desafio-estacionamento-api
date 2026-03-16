using Estacionamento.Application.Common.Exceptions;
using Estacionamento.Application.Features.Usuarios.Commands.CadastrarUsuario;
using Estacionamento.Application.Services.Usuarios;
using Estacionamento.Domain.Entities;
using Estacionamento.Infrastructure.Security;
using Estacionamento.Testes.Fakes;

namespace Estacionamento.Testes.Application.Usuarios;

public class CadastrarUsuarioCommandHandlerTests
{
    [Fact]
    public async Task CadastrarUsuarioHandler_DeveCadastrarUsuarioValidoComSucesso()
    {
        FakeUsuarioRepository usuarioRepository;
        BcryptPasswordHasher passwordHasher;
        UsuarioService usuarioService;
        CadastrarUsuarioCommandHandler handler;
        CadastrarUsuarioCommand command;
        var resultado = default(Estacionamento.Application.Common.Models.UsuarioModel);

        usuarioRepository = new FakeUsuarioRepository();
        passwordHasher = new BcryptPasswordHasher();
        usuarioService = new UsuarioService(usuarioRepository, passwordHasher);
        handler = new CadastrarUsuarioCommandHandler(usuarioService);
        command = new CadastrarUsuarioCommand
        {
            Nome = "Anderson",
            Login = "anderson",
            Senha = "senha123"
        };

        resultado = await handler.Handle(command, CancellationToken.None);

        Assert.NotEqual(Guid.Empty, resultado.Id);
        Assert.Equal("Anderson", resultado.Nome);
        Assert.Equal("anderson", resultado.Login);
        Assert.Single(usuarioRepository.Usuarios);
    }

    [Fact]
    public async Task CadastrarUsuarioHandler_NaoDeveCadastrarUsuarioComLoginDuplicado()
    {
        FakeUsuarioRepository usuarioRepository;
        BcryptPasswordHasher passwordHasher;
        UsuarioService usuarioService;
        CadastrarUsuarioCommandHandler handler;
        CadastrarUsuarioCommand command;
        Usuario usuarioExistente;

        usuarioRepository = new FakeUsuarioRepository();
        passwordHasher = new BcryptPasswordHasher();
        usuarioService = new UsuarioService(usuarioRepository, passwordHasher);
        handler = new CadastrarUsuarioCommandHandler(usuarioService);
        usuarioExistente = new Usuario(Guid.NewGuid(), "Anderson", "anderson", passwordHasher.HashPassword("senha123"));
        command = new CadastrarUsuarioCommand
        {
            Nome = "Outro Usuario",
            Login = "anderson",
            Senha = "senha456"
        };

        await usuarioRepository.AdicionarAsync(usuarioExistente, CancellationToken.None);

        await Assert.ThrowsAsync<ConflictAppException>(async () =>
        {
            await handler.Handle(command, CancellationToken.None);
        });
    }

    [Fact]
    public async Task CadastrarUsuarioHandler_DeveGerarHashDaSenhaSemSalvarTextoPuro()
    {
        FakeUsuarioRepository usuarioRepository;
        BcryptPasswordHasher passwordHasher;
        UsuarioService usuarioService;
        CadastrarUsuarioCommandHandler handler;
        CadastrarUsuarioCommand command;
        Usuario usuarioSalvo;

        usuarioRepository = new FakeUsuarioRepository();
        passwordHasher = new BcryptPasswordHasher();
        usuarioService = new UsuarioService(usuarioRepository, passwordHasher);
        handler = new CadastrarUsuarioCommandHandler(usuarioService);
        command = new CadastrarUsuarioCommand
        {
            Nome = "Joao Souza",
            Login = "joao",
            Senha = "senha123"
        };

        await handler.Handle(command, CancellationToken.None);

        usuarioSalvo = usuarioRepository.Usuarios.Single();

        Assert.NotEqual("senha123", usuarioSalvo.Senha);
        Assert.True(passwordHasher.VerifyPassword("senha123", usuarioSalvo.Senha));
    }
}

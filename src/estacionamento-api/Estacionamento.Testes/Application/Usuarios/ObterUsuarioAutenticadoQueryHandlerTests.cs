using Estacionamento.Application.Common.Exceptions;
using Estacionamento.Application.Features.Usuarios.Queries.ObterUsuarioAutenticado;
using Estacionamento.Application.Services.Usuarios;
using Estacionamento.Domain.Entities;
using Estacionamento.Infrastructure.Security;
using Estacionamento.Testes.Fakes;

namespace Estacionamento.Testes.Application.Usuarios;

public class ObterUsuarioAutenticadoQueryHandlerTests
{
    [Fact]
    public async Task ObterUsuarioAutenticadoQueryHandler_DeveRetornarUsuarioQuandoEleExistir()
    {
        FakeUsuarioRepository usuarioRepository;
        UsuarioService usuarioService;
        ObterUsuarioAutenticadoQueryHandler handler;
        Usuario usuario;
        ObterUsuarioAutenticadoQuery query;
        var resultado = default(Estacionamento.Application.Common.Models.UsuarioModel);

        usuarioRepository = new FakeUsuarioRepository();
        usuarioService = new UsuarioService(usuarioRepository, new BcryptPasswordHasher());
        handler = new ObterUsuarioAutenticadoQueryHandler(usuarioService);
        usuario = new Usuario(Guid.NewGuid(), "Anderson", "anderson", "hash");
        query = new ObterUsuarioAutenticadoQuery
        {
            UsuarioId = usuario.Id
        };

        await usuarioRepository.AdicionarAsync(usuario, CancellationToken.None);
        resultado = await handler.Handle(query, CancellationToken.None);

        Assert.Equal(usuario.Id, resultado.Id);
        Assert.Equal(usuario.Nome, resultado.Nome);
        Assert.Equal(usuario.Login, resultado.Login);
    }

    [Fact]
    public async Task ObterUsuarioAutenticadoQueryHandler_DeveFalharQuandoUsuarioNaoExistir()
    {
        FakeUsuarioRepository usuarioRepository;
        UsuarioService usuarioService;
        ObterUsuarioAutenticadoQueryHandler handler;
        ObterUsuarioAutenticadoQuery query;

        usuarioRepository = new FakeUsuarioRepository();
        usuarioService = new UsuarioService(usuarioRepository, new BcryptPasswordHasher());
        handler = new ObterUsuarioAutenticadoQueryHandler(usuarioService);
        query = new ObterUsuarioAutenticadoQuery
        {
            UsuarioId = Guid.NewGuid()
        };

        await Assert.ThrowsAsync<NotFoundAppException>(async () =>
        {
            await handler.Handle(query, CancellationToken.None);
        });
    }
}

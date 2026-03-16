using Estacionamento.Application.Abstractions.Security;
using Estacionamento.Application.Common.Exceptions;
using Estacionamento.Application.Common.Models;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Interfaces;

namespace Estacionamento.Application.Services.Usuarios;

public class UsuarioService : IUsuarioService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository, IPasswordHasher passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UsuarioModel> CadastrarAsync(string nome, string login, string senha, CancellationToken cancellationToken)
    {
        Usuario usuario;
        string loginNormalizado;

        loginNormalizado = login.Trim();

        if (await _usuarioRepository.ExistePorLoginAsync(loginNormalizado, cancellationToken))
        {
            throw new ConflictAppException("Já existe um usuário com o login informado.");
        }

        usuario = new Usuario(
            Guid.NewGuid(),
            nome.Trim(),
            loginNormalizado,
            _passwordHasher.HashPassword(senha));

        await _usuarioRepository.AdicionarAsync(usuario, cancellationToken);

        return new UsuarioModel
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Login = usuario.Login
        };
    }

    public async Task<UsuarioModel> ObterPorIdAsync(Guid usuarioId, CancellationToken cancellationToken)
    {
        Usuario? usuario;

        usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId, cancellationToken);

        if (usuario is null)
        {
            throw new NotFoundAppException("Usuário autenticado não encontrado.");
        }

        return new UsuarioModel
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Login = usuario.Login
        };
    }
}

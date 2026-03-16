using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Interfaces;

namespace Estacionamento.Testes.Fakes;

public class FakeUsuarioRepository : IUsuarioRepository
{
    private readonly List<Usuario> _usuarios;

    public FakeUsuarioRepository()
    {
        _usuarios = new List<Usuario>();
    }

    public IReadOnlyCollection<Usuario> Usuarios
    {
        get
        {
            return _usuarios.AsReadOnly();
        }
    }

    public Task<Usuario?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Usuario? usuario;

        usuario = _usuarios.SingleOrDefault(item => item.Id == id);

        return Task.FromResult(usuario);
    }

    public Task<Usuario?> ObterPorLoginAsync(string login, CancellationToken cancellationToken)
    {
        string loginNormalizado;
        Usuario? usuario;

        loginNormalizado = login.ToLowerInvariant();
        usuario = _usuarios.SingleOrDefault(item => item.Login.ToLowerInvariant() == loginNormalizado);

        return Task.FromResult(usuario);
    }

    public Task<bool> ExistePorLoginAsync(string login, CancellationToken cancellationToken)
    {
        string loginNormalizado;
        bool existe;

        loginNormalizado = login.ToLowerInvariant();
        existe = _usuarios.Any(item => item.Login.ToLowerInvariant() == loginNormalizado);

        return Task.FromResult(existe);
    }

    public Task AdicionarAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        _usuarios.Add(usuario);

        return Task.CompletedTask;
    }

    public Task AtualizarAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task RemoverAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        _usuarios.Remove(usuario);

        return Task.CompletedTask;
    }
}

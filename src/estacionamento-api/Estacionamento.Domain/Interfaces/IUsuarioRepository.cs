using Estacionamento.Domain.Entities;

namespace Estacionamento.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Usuario?> ObterPorLoginAsync(string login, CancellationToken cancellationToken);

    Task<bool> ExistePorLoginAsync(string login, CancellationToken cancellationToken);

    Task AdicionarAsync(Usuario usuario, CancellationToken cancellationToken);

    Task AtualizarAsync(Usuario usuario, CancellationToken cancellationToken);

    Task RemoverAsync(Usuario usuario, CancellationToken cancellationToken);
}

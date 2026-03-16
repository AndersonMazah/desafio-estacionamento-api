using Estacionamento.Domain.Entities;

namespace Estacionamento.Domain.Interfaces;

public interface IVeiculoRepository
{
    Task<Veiculo?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Veiculo>> ListarAsync(CancellationToken cancellationToken);

    Task AdicionarAsync(Veiculo veiculo, CancellationToken cancellationToken);

    Task AtualizarAsync(Veiculo veiculo, CancellationToken cancellationToken);

    Task RemoverAsync(Veiculo veiculo, CancellationToken cancellationToken);
}

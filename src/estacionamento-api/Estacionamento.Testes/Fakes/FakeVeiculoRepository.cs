using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Interfaces;

namespace Estacionamento.Testes.Fakes;

public class FakeVeiculoRepository : IVeiculoRepository
{
    private readonly List<Veiculo> _veiculos;

    public FakeVeiculoRepository()
    {
        _veiculos = new List<Veiculo>();
    }

    public IReadOnlyCollection<Veiculo> Veiculos
    {
        get
        {
            return _veiculos.AsReadOnly();
        }
    }

    public Task<Veiculo?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Veiculo? veiculo;

        veiculo = _veiculos.SingleOrDefault(item => item.Id == id);

        return Task.FromResult(veiculo);
    }

    public Task<IReadOnlyCollection<Veiculo>> ListarAsync(CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Veiculo> veiculos;

        veiculos = _veiculos
            .OrderBy(item => item.Descricao)
            .ToList();

        return Task.FromResult(veiculos);
    }

    public Task AdicionarAsync(Veiculo veiculo, CancellationToken cancellationToken)
    {
        _veiculos.Add(veiculo);

        return Task.CompletedTask;
    }

    public Task AtualizarAsync(Veiculo veiculo, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task RemoverAsync(Veiculo veiculo, CancellationToken cancellationToken)
    {
        _veiculos.Remove(veiculo);

        return Task.CompletedTask;
    }
}

using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Interfaces;
using Estacionamento.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Infrastructure.Repositories;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly EstacionamentoDbContext _context;

    public VeiculoRepository(EstacionamentoDbContext context)
    {
        _context = context;
    }

    public async Task<Veiculo?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Veiculos.SingleOrDefaultAsync(veiculo => veiculo.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Veiculo>> ListarAsync(CancellationToken cancellationToken)
    {
        return await _context.Veiculos
            .OrderBy(veiculo => veiculo.Descricao)
            .ToListAsync(cancellationToken);
    }

    public async Task AdicionarAsync(Veiculo veiculo, CancellationToken cancellationToken)
    {
        _context.Veiculos.Add(veiculo);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(Veiculo veiculo, CancellationToken cancellationToken)
    {
        _context.Veiculos.Update(veiculo);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverAsync(Veiculo veiculo, CancellationToken cancellationToken)
    {
        _context.Veiculos.Remove(veiculo);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

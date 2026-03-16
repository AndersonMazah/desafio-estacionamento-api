using Estacionamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Infrastructure.Persistence;

public class EstacionamentoDbContext : DbContext
{
    public EstacionamentoDbContext(DbContextOptions<EstacionamentoDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios
    {
        get
        {
            return Set<Usuario>();
        }
    }

    public DbSet<Veiculo> Veiculos
    {
        get
        {
            return Set<Veiculo>();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EstacionamentoDbContext).Assembly);
    }
}

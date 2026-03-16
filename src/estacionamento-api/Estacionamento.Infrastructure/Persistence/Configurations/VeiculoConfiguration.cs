using Estacionamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estacionamento.Infrastructure.Persistence.Configurations;

public class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.HasKey(veiculo => veiculo.Id);

        builder.Property(veiculo => veiculo.Descricao)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(veiculo => veiculo.Marca)
            .IsRequired();

        builder.Property(veiculo => veiculo.Modelo)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(veiculo => veiculo.Opcionais);

        builder.Property(veiculo => veiculo.Valor);
    }
}

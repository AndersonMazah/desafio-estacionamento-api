using Estacionamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estacionamento.Infrastructure.Persistence.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(usuario => usuario.Id);

        builder.Property(usuario => usuario.Nome)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(usuario => usuario.Login)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(usuario => usuario.Senha)
            .IsRequired();

        builder.HasIndex(usuario => usuario.Login)
            .IsUnique();
    }
}

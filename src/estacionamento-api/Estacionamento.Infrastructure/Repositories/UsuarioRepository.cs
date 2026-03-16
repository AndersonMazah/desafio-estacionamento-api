using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Interfaces;
using Estacionamento.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EstacionamentoDbContext _context;

    public UsuarioRepository(EstacionamentoDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Usuarios.SingleOrDefaultAsync(usuario => usuario.Id == id, cancellationToken);
    }

    public async Task<Usuario?> ObterPorLoginAsync(string login, CancellationToken cancellationToken)
    {
        string loginNormalizado;
        loginNormalizado = login.ToLower();
        return await _context.Usuarios.SingleOrDefaultAsync(usuario => usuario.Login.ToLower() == loginNormalizado, cancellationToken);
    }

    public async Task<bool> ExistePorLoginAsync(string login, CancellationToken cancellationToken)
    {
        string loginNormalizado;
        loginNormalizado = login.ToLower();
        return await _context.Usuarios.AnyAsync(usuario => usuario.Login.ToLower() == loginNormalizado, cancellationToken);
    }

    public async Task AdicionarAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

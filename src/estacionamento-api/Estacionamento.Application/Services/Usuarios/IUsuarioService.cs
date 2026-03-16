using Estacionamento.Application.Common.Models;

namespace Estacionamento.Application.Services.Usuarios;

public interface IUsuarioService
{
    Task<UsuarioModel> CadastrarAsync(string nome, string login, string senha, CancellationToken cancellationToken);

    Task<UsuarioModel> ObterPorIdAsync(Guid usuarioId, CancellationToken cancellationToken);
}

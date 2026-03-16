using Estacionamento.Application.Common.Models;
using Estacionamento.Domain.Enums;

namespace Estacionamento.Application.Services.Veiculos;

public interface IVeiculoService
{
    Task<VeiculoModel> CadastrarAsync(string descricao, EnumMarca marca, string modelo, string? opcionais, decimal? valor, CancellationToken cancellationToken);

    Task<VeiculoModel> AtualizarAsync(Guid id, string descricao, EnumMarca marca, string modelo, string? opcionais, decimal? valor, CancellationToken cancellationToken);

    Task<VeiculoModel> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<VeiculoModel>> ListarAsync(CancellationToken cancellationToken);

    Task ExcluirAsync(Guid id, CancellationToken cancellationToken);
}

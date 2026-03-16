using Estacionamento.Application.Common.Exceptions;
using Estacionamento.Application.Common.Models;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Enums;
using Estacionamento.Domain.Interfaces;

namespace Estacionamento.Application.Services.Veiculos;

public class VeiculoService : IVeiculoService
{
    private readonly IVeiculoRepository _veiculoRepository;

    public VeiculoService(IVeiculoRepository veiculoRepository)
    {
        _veiculoRepository = veiculoRepository;
    }

    public async Task<VeiculoModel> CadastrarAsync(string descricao, EnumMarca marca, string modelo, string? opcionais, decimal? valor, CancellationToken cancellationToken)
    {
        Veiculo veiculo;

        veiculo = new Veiculo(
            Guid.NewGuid(),
            descricao.Trim(),
            marca,
            modelo.Trim(),
            opcionais,
            valor);

        await _veiculoRepository.AdicionarAsync(veiculo, cancellationToken);

        return MapVeiculo(veiculo);
    }

    public async Task<VeiculoModel> AtualizarAsync(Guid id, string descricao, EnumMarca marca, string modelo, string? opcionais, decimal? valor, CancellationToken cancellationToken)
    {
        Veiculo veiculo;

        veiculo = await ObterEntidadePorIdAsync(id, cancellationToken);
        veiculo.Atualizar(descricao.Trim(), marca, modelo.Trim(), opcionais, valor);

        await _veiculoRepository.AtualizarAsync(veiculo, cancellationToken);

        return MapVeiculo(veiculo);
    }

    public async Task<VeiculoModel> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Veiculo veiculo;

        veiculo = await ObterEntidadePorIdAsync(id, cancellationToken);

        return MapVeiculo(veiculo);
    }

    public async Task<IReadOnlyCollection<VeiculoModel>> ListarAsync(CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Veiculo> veiculos;

        veiculos = await _veiculoRepository.ListarAsync(cancellationToken);

        return veiculos.Select(MapVeiculo).ToList();
    }

    public async Task ExcluirAsync(Guid id, CancellationToken cancellationToken)
    {
        Veiculo veiculo;

        veiculo = await ObterEntidadePorIdAsync(id, cancellationToken);
        await _veiculoRepository.RemoverAsync(veiculo, cancellationToken);
    }

    private async Task<Veiculo> ObterEntidadePorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Veiculo? veiculo;

        veiculo = await _veiculoRepository.ObterPorIdAsync(id, cancellationToken);

        if (veiculo is null)
        {
            throw new NotFoundAppException("Veículo não encontrado.");
        }

        return veiculo;
    }

    private static VeiculoModel MapVeiculo(Veiculo veiculo)
    {
        return new VeiculoModel
        {
            Id = veiculo.Id,
            Descricao = veiculo.Descricao,
            Marca = veiculo.Marca,
            Modelo = veiculo.Modelo,
            Opcionais = veiculo.Opcionais,
            Valor = veiculo.Valor
        };
    }
}

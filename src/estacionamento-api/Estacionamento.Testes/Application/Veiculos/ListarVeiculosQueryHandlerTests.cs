using Estacionamento.Application.Features.Veiculos.Queries.ListarVeiculos;
using Estacionamento.Application.Services.Veiculos;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Enums;
using Estacionamento.Testes.Fakes;

namespace Estacionamento.Testes.Application.Veiculos;

public class ListarVeiculosQueryHandlerTests
{
    [Fact]
    public async Task ListarVeiculosQueryHandler_DeveRetornarListaDeVeiculos()
    {
        FakeVeiculoRepository veiculoRepository;
        VeiculoService veiculoService;
        ListarVeiculosQueryHandler handler;
        IReadOnlyCollection<Estacionamento.Application.Common.Models.VeiculoModel> resultado;

        veiculoRepository = new FakeVeiculoRepository();
        veiculoService = new VeiculoService(veiculoRepository);
        handler = new ListarVeiculosQueryHandler(veiculoService);

        await veiculoRepository.AdicionarAsync(
            new Veiculo(Guid.NewGuid(), "Sedan", EnumMarca.Renault, "Sandero", null, 90000m),
            CancellationToken.None);
        await veiculoRepository.AdicionarAsync(
            new Veiculo(Guid.NewGuid(), "Hatch", EnumMarca.Volkswagen, "Gol", null, 30000m),
            CancellationToken.None);

        resultado = await handler.Handle(new ListarVeiculosQuery(), CancellationToken.None);

        Assert.Equal(2, resultado.Count);
    }

    [Fact]
    public async Task ListarVeiculosQueryHandler_DeveRetornarListaVaziaQuandoNaoHouverVeiculos()
    {
        FakeVeiculoRepository veiculoRepository;
        VeiculoService veiculoService;
        ListarVeiculosQueryHandler handler;
        IReadOnlyCollection<Estacionamento.Application.Common.Models.VeiculoModel> resultado;

        veiculoRepository = new FakeVeiculoRepository();
        veiculoService = new VeiculoService(veiculoRepository);
        handler = new ListarVeiculosQueryHandler(veiculoService);

        resultado = await handler.Handle(new ListarVeiculosQuery(), CancellationToken.None);

        Assert.Empty(resultado);
    }
}

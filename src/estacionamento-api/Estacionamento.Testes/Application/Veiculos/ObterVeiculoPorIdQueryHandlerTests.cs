using Estacionamento.Application.Common.Exceptions;
using Estacionamento.Application.Features.Veiculos.Queries.ObterVeiculoPorId;
using Estacionamento.Application.Services.Veiculos;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Enums;
using Estacionamento.Testes.Fakes;

namespace Estacionamento.Testes.Application.Veiculos;

public class ObterVeiculoPorIdQueryHandlerTests
{
    [Fact]
    public async Task ObterVeiculoPorIdQueryHandler_DeveRetornarVeiculoQuandoExistir()
    {
        FakeVeiculoRepository veiculoRepository;
        VeiculoService veiculoService;
        ObterVeiculoPorIdQueryHandler handler;
        Veiculo veiculo;
        ObterVeiculoPorIdQuery query;
        var resultado = default(Estacionamento.Application.Common.Models.VeiculoModel);

        veiculoRepository = new FakeVeiculoRepository();
        veiculoService = new VeiculoService(veiculoRepository);
        handler = new ObterVeiculoPorIdQueryHandler(veiculoService);
        veiculo = new Veiculo(Guid.NewGuid(), "Carro", EnumMarca.Ford, "Ka", null, 80000m);
        query = new ObterVeiculoPorIdQuery
        {
            Id = veiculo.Id
        };

        await veiculoRepository.AdicionarAsync(veiculo, CancellationToken.None);
        resultado = await handler.Handle(query, CancellationToken.None);

        Assert.Equal(veiculo.Id, resultado.Id);
        Assert.Equal("Carro", resultado.Descricao);
    }

    [Fact]
    public async Task ObterVeiculoPorIdQueryHandler_DeveFalharQuandoVeiculoNaoExistir()
    {
        FakeVeiculoRepository veiculoRepository;
        VeiculoService veiculoService;
        ObterVeiculoPorIdQueryHandler handler;
        ObterVeiculoPorIdQuery query;

        veiculoRepository = new FakeVeiculoRepository();
        veiculoService = new VeiculoService(veiculoRepository);
        handler = new ObterVeiculoPorIdQueryHandler(veiculoService);
        query = new ObterVeiculoPorIdQuery
        {
            Id = Guid.NewGuid()
        };

        await Assert.ThrowsAsync<NotFoundAppException>(async () =>
        {
            await handler.Handle(query, CancellationToken.None);
        });
    }
}

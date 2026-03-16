using Estacionamento.Application.Common.Exceptions;
using Estacionamento.Application.Features.Veiculos.Commands.ExcluirVeiculo;
using Estacionamento.Application.Services.Veiculos;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Enums;
using Estacionamento.Testes.Fakes;
using MediatR;

namespace Estacionamento.Testes.Application.Veiculos;

public class ExcluirVeiculoCommandHandlerTests
{
    [Fact]
    public async Task ExcluirVeiculoCommandHandler_DeveRemoverVeiculoExistenteComSucesso()
    {
        FakeVeiculoRepository veiculoRepository;
        VeiculoService veiculoService;
        ExcluirVeiculoCommandHandler handler;
        Veiculo veiculo;
        Unit resultado;

        veiculoRepository = new FakeVeiculoRepository();
        veiculoService = new VeiculoService(veiculoRepository);
        handler = new ExcluirVeiculoCommandHandler(veiculoService);
        veiculo = new Veiculo(Guid.NewGuid(), "Carro", EnumMarca.Ford, "Ka", null, 25000m);

        await veiculoRepository.AdicionarAsync(veiculo, CancellationToken.None);
        resultado = await handler.Handle(new ExcluirVeiculoCommand { Id = veiculo.Id }, CancellationToken.None);

        Assert.Equal(Unit.Value, resultado);
        Assert.Empty(veiculoRepository.Veiculos);
    }

    [Fact]
    public async Task ExcluirVeiculoCommandHandler_DeveFalharAoRemoverVeiculoInexistente()
    {
        FakeVeiculoRepository veiculoRepository;
        VeiculoService veiculoService;
        ExcluirVeiculoCommandHandler handler;

        veiculoRepository = new FakeVeiculoRepository();
        veiculoService = new VeiculoService(veiculoRepository);
        handler = new ExcluirVeiculoCommandHandler(veiculoService);

        await Assert.ThrowsAsync<NotFoundAppException>(async () =>
        {
            await handler.Handle(new ExcluirVeiculoCommand { Id = Guid.NewGuid() }, CancellationToken.None);
        });
    }
}

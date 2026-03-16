using Estacionamento.Application.Common.Exceptions;
using Estacionamento.Application.Features.Veiculos.Commands.AtualizarVeiculo;
using Estacionamento.Application.Services.Veiculos;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Enums;
using Estacionamento.Testes.Fakes;

namespace Estacionamento.Testes.Application.Veiculos;

public class AtualizarVeiculoCommandHandlerTests
{
    [Fact]
    public async Task AtualizarVeiculoCommandHandler_DeveAtualizarVeiculoValidoComSucesso()
    {
        FakeVeiculoRepository veiculoRepository;
        VeiculoService veiculoService;
        AtualizarVeiculoCommandHandler handler;
        Veiculo veiculo;
        AtualizarVeiculoCommand command;
        var resultado = default(Estacionamento.Application.Common.Models.VeiculoModel);

        veiculoRepository = new FakeVeiculoRepository();
        veiculoService = new VeiculoService(veiculoRepository);
        handler = new AtualizarVeiculoCommandHandler(veiculoService);
        veiculo = new Veiculo(Guid.NewGuid(), "Carro antigo", EnumMarca.Ford, "Ka", null, 15000m);
        command = new AtualizarVeiculoCommand
        {
            Id = veiculo.Id,
            Descricao = "Carro atualizado",
            Marca = EnumMarca.Volkswagen,
            Modelo = "Gol",
            Opcionais = "Direcao eletrica",
            Valor = 55000m
        };

        await veiculoRepository.AdicionarAsync(veiculo, CancellationToken.None);
        resultado = await handler.Handle(command, CancellationToken.None);

        Assert.Equal(veiculo.Id, resultado.Id);
        Assert.Equal("Carro atualizado", resultado.Descricao);
        Assert.Equal(EnumMarca.Volkswagen, resultado.Marca);
        Assert.Equal("Gol", resultado.Modelo);
    }

    [Fact]
    public async Task AtualizarVeiculoCommandHandler_DeveFalharAoAtualizarVeiculoInexistente()
    {
        FakeVeiculoRepository veiculoRepository;
        VeiculoService veiculoService;
        AtualizarVeiculoCommandHandler handler;
        AtualizarVeiculoCommand command;

        veiculoRepository = new FakeVeiculoRepository();
        veiculoService = new VeiculoService(veiculoRepository);
        handler = new AtualizarVeiculoCommandHandler(veiculoService);
        command = new AtualizarVeiculoCommand
        {
            Id = Guid.NewGuid(),
            Descricao = "Carro atualizado",
            Marca = EnumMarca.Volkswagen,
            Modelo = "Gol",
            Opcionais = "Direcao eletrica",
            Valor = 55000m
        };

        await Assert.ThrowsAsync<NotFoundAppException>(async () =>
        {
            await handler.Handle(command, CancellationToken.None);
        });
    }
}

using Estacionamento.Application.Features.Veiculos.Commands.CadastrarVeiculo;
using Estacionamento.Application.Services.Veiculos;
using Estacionamento.Domain.Enums;
using Estacionamento.Testes.Fakes;

namespace Estacionamento.Testes.Application.Veiculos;

public class CadastrarVeiculoCommandHandlerTests
{
    [Fact]
    public async Task CadastrarVeiculoCommandHandler_DeveCadastrarVeiculoValidoComSucesso()
    {
        FakeVeiculoRepository veiculoRepository;
        VeiculoService veiculoService;
        CadastrarVeiculoCommandHandler handler;
        CadastrarVeiculoCommand command;
        var resultado = default(Estacionamento.Application.Common.Models.VeiculoModel);

        veiculoRepository = new FakeVeiculoRepository();
        veiculoService = new VeiculoService(veiculoRepository);
        handler = new CadastrarVeiculoCommandHandler(veiculoService);
        command = new CadastrarVeiculoCommand
        {
            Descricao = "Carro de passeio",
            Marca = EnumMarca.Ford,
            Modelo = "Fiesta",
            Opcionais = "Ar-condicionado",
            Valor = 35000m
        };

        resultado = await handler.Handle(command, CancellationToken.None);

        Assert.NotEqual(Guid.Empty, resultado.Id);
        Assert.Equal("Carro de passeio", resultado.Descricao);
        Assert.Equal(EnumMarca.Ford, resultado.Marca);
        Assert.Single(veiculoRepository.Veiculos);
    }
}

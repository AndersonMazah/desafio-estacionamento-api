using Estacionamento.Application.Common.Models;
using Estacionamento.Application.Services.Veiculos;
using MediatR;

namespace Estacionamento.Application.Features.Veiculos.Commands.CadastrarVeiculo;

public class CadastrarVeiculoCommandHandler : IRequestHandler<CadastrarVeiculoCommand, VeiculoModel>
{
    private readonly IVeiculoService _veiculoService;

    public CadastrarVeiculoCommandHandler(IVeiculoService veiculoService)
    {
        _veiculoService = veiculoService;
    }

    public async Task<VeiculoModel> Handle(CadastrarVeiculoCommand request, CancellationToken cancellationToken)
    {
        return await _veiculoService.CadastrarAsync(
            request.Descricao,
            request.Marca,
            request.Modelo,
            request.Opcionais,
            request.Valor,
            cancellationToken);
    }
}

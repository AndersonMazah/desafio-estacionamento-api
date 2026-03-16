using Estacionamento.Application.Common.Models;
using Estacionamento.Application.Services.Veiculos;
using MediatR;

namespace Estacionamento.Application.Features.Veiculos.Commands.AtualizarVeiculo;

public class AtualizarVeiculoCommandHandler : IRequestHandler<AtualizarVeiculoCommand, VeiculoModel>
{
    private readonly IVeiculoService _veiculoService;

    public AtualizarVeiculoCommandHandler(IVeiculoService veiculoService)
    {
        _veiculoService = veiculoService;
    }

    public async Task<VeiculoModel> Handle(AtualizarVeiculoCommand request, CancellationToken cancellationToken)
    {
        return await _veiculoService.AtualizarAsync(
            request.Id,
            request.Descricao,
            request.Marca,
            request.Modelo,
            request.Opcionais,
            request.Valor,
            cancellationToken);
    }
}

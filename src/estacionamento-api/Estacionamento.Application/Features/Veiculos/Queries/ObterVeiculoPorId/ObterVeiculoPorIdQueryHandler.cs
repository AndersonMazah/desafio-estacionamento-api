using Estacionamento.Application.Common.Models;
using Estacionamento.Application.Services.Veiculos;
using MediatR;

namespace Estacionamento.Application.Features.Veiculos.Queries.ObterVeiculoPorId;

public class ObterVeiculoPorIdQueryHandler : IRequestHandler<ObterVeiculoPorIdQuery, VeiculoModel>
{
    private readonly IVeiculoService _veiculoService;

    public ObterVeiculoPorIdQueryHandler(IVeiculoService veiculoService)
    {
        _veiculoService = veiculoService;
    }

    public async Task<VeiculoModel> Handle(ObterVeiculoPorIdQuery request, CancellationToken cancellationToken)
    {
        return await _veiculoService.ObterPorIdAsync(request.Id, cancellationToken);
    }
}

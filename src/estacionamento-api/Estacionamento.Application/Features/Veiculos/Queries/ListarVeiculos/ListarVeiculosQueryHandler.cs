using Estacionamento.Application.Common.Models;
using Estacionamento.Application.Services.Veiculos;
using MediatR;

namespace Estacionamento.Application.Features.Veiculos.Queries.ListarVeiculos;

public class ListarVeiculosQueryHandler : IRequestHandler<ListarVeiculosQuery, IReadOnlyCollection<VeiculoModel>>
{
    private readonly IVeiculoService _veiculoService;

    public ListarVeiculosQueryHandler(IVeiculoService veiculoService)
    {
        _veiculoService = veiculoService;
    }

    public async Task<IReadOnlyCollection<VeiculoModel>> Handle(ListarVeiculosQuery request, CancellationToken cancellationToken)
    {
        return await _veiculoService.ListarAsync(cancellationToken);
    }
}

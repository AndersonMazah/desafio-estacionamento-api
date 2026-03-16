using Estacionamento.Application.Services.Veiculos;
using MediatR;

namespace Estacionamento.Application.Features.Veiculos.Commands.ExcluirVeiculo;

public class ExcluirVeiculoCommandHandler : IRequestHandler<ExcluirVeiculoCommand, Unit>
{
    private readonly IVeiculoService _veiculoService;

    public ExcluirVeiculoCommandHandler(IVeiculoService veiculoService)
    {
        _veiculoService = veiculoService;
    }

    public async Task<Unit> Handle(ExcluirVeiculoCommand request, CancellationToken cancellationToken)
    {
        await _veiculoService.ExcluirAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}

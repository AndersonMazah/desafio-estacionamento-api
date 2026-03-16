using MediatR;

namespace Estacionamento.Application.Features.Veiculos.Commands.ExcluirVeiculo;

public class ExcluirVeiculoCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

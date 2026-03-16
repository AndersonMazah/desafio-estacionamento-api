using Estacionamento.Application.Common.Models;
using MediatR;

namespace Estacionamento.Application.Features.Veiculos.Queries.ListarVeiculos;

public class ListarVeiculosQuery : IRequest<IReadOnlyCollection<VeiculoModel>>
{
}

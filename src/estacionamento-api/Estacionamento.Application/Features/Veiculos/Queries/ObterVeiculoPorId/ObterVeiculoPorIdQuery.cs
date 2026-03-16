using Estacionamento.Application.Common.Models;
using MediatR;

namespace Estacionamento.Application.Features.Veiculos.Queries.ObterVeiculoPorId;

public class ObterVeiculoPorIdQuery : IRequest<VeiculoModel>
{
    public Guid Id { get; set; }
}

using Estacionamento.Application.Common.Models;
using Estacionamento.Domain.Enums;
using MediatR;

namespace Estacionamento.Application.Features.Veiculos.Commands.CadastrarVeiculo;

public class CadastrarVeiculoCommand : IRequest<VeiculoModel>
{
    public string Descricao { get; set; } = string.Empty;

    public EnumMarca Marca { get; set; }

    public string Modelo { get; set; } = string.Empty;

    public string? Opcionais { get; set; }

    public decimal? Valor { get; set; }
}

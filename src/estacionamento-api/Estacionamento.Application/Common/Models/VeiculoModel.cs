using Estacionamento.Domain.Enums;

namespace Estacionamento.Application.Common.Models;

public class VeiculoModel
{
    public Guid Id { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public EnumMarca Marca { get; set; }

    public string Modelo { get; set; } = string.Empty;

    public string? Opcionais { get; set; }

    public decimal? Valor { get; set; }
}

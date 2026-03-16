using Estacionamento.Domain.Enums;

namespace Estacionamento.Domain.Entities;

public class Veiculo
{
    public Veiculo(Guid id, string descricao, EnumMarca marca, string modelo, string? opcionais, decimal? valor)
    {
        Id = id;
        Descricao = descricao;
        Marca = marca;
        Modelo = modelo;
        Opcionais = opcionais;
        Valor = valor;
    }

    public Guid Id { get; private set; }

    public string Descricao { get; private set; }

    public EnumMarca Marca { get; private set; }

    public string Modelo { get; private set; }

    public string? Opcionais { get; private set; }

    public decimal? Valor { get; private set; }

    public void Atualizar(string descricao, EnumMarca marca, string modelo, string? opcionais, decimal? valor)
    {
        Descricao = descricao;
        Marca = marca;
        Modelo = modelo;
        Opcionais = opcionais;
        Valor = valor;
    }

}

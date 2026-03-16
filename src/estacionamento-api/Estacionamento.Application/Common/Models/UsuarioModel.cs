namespace Estacionamento.Application.Common.Models;

public class UsuarioModel
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Login { get; set; } = string.Empty;
}

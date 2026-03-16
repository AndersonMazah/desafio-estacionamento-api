namespace Estacionamento.Domain.Entities;

public class Usuario
{
    public Usuario(Guid id, string nome, string login, string senha)
    {
        Id = id;
        Nome = nome;
        Login = login;
        Senha = senha;
    }

    public Guid Id { get; private set; }

    public string Nome { get; private set; }

    public string Login { get; private set; }

    public string Senha { get; private set; }

    public void Atualizar(string nome, string login, string senha)
    {
        Nome = nome;
        Login = login;
        Senha = senha;
    }

}

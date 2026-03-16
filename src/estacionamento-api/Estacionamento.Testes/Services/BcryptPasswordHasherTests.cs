using Estacionamento.Infrastructure.Security;

namespace Estacionamento.Testes.Services;

public class BcryptPasswordHasherTests
{
    [Fact]
    public void BcryptPasswordHasher_DeveGerarHashDiferenteDaSenhaOriginal()
    {
        BcryptPasswordHasher passwordHasher;
        string hash;

        passwordHasher = new BcryptPasswordHasher();
        hash = passwordHasher.HashPassword("senha123");

        Assert.NotEqual("senha123", hash);
    }

    [Fact]
    public void BcryptPasswordHasher_DeveValidarSenhaCorretaComHash()
    {
        BcryptPasswordHasher passwordHasher;
        string hash;
        bool resultado;

        passwordHasher = new BcryptPasswordHasher();
        hash = passwordHasher.HashPassword("senha123");
        resultado = passwordHasher.VerifyPassword("senha123", hash);

        Assert.True(resultado);
    }

    [Fact]
    public void BcryptPasswordHasher_NaoDeveValidarSenhaIncorretaComHash()
    {
        BcryptPasswordHasher passwordHasher;
        string hash;
        bool resultado;

        passwordHasher = new BcryptPasswordHasher();
        hash = passwordHasher.HashPassword("senha123");
        resultado = passwordHasher.VerifyPassword("senhaErrada", hash);

        Assert.False(resultado);
    }
}

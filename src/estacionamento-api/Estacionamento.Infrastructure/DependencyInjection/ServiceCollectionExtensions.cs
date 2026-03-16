using Estacionamento.Application.Abstractions.Authentication;
using Estacionamento.Application.Abstractions.Security;
using Estacionamento.Domain.Interfaces;
using Estacionamento.Infrastructure.Options;
using Estacionamento.Infrastructure.Persistence;
using Estacionamento.Infrastructure.Repositories;
using Estacionamento.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Estacionamento.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.AddDbContext<EstacionamentoDbContext>(options =>
        {
            options.UseInMemoryDatabase("EstacionamentoDb");
        });

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}

using Estacionamento.Application.Behaviors;
using Estacionamento.Application.Services.Auth;
using Estacionamento.Application.Services.Usuarios;
using Estacionamento.Application.Services.Veiculos;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Estacionamento.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IVeiculoService, VeiculoService>();
        return services;
    }
}

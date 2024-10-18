using BancoChu.Domain.Handlers;
using BancoChu.Domain.Interfaces.Handlers;
using BancoChu.Domain.Interfaces.Repositories;
using BancoChu.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BancoChu.App;

public static class Container
{
    public static void AddServices(this IServiceCollection services)
    {
        AddHandlers(services);
        AddRepositories(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<ITransacoesRepository, TransacoesRepository>();
        services.AddScoped<IContasRepository, ContasRepository>();
    }

    private static void AddHandlers(IServiceCollection services)
    {
        services.AddScoped<ITransacoesHandler, TransacoesHandler>();
    }
}

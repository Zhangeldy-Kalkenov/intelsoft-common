using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Nomad.Common.UseCases.Internal;

namespace Nomad.Common.UseCases;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services, Assembly assembly)
    {
        var useCaseType = typeof(IUseCase<,>);

        var handlers = assembly.GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == useCaseType)
                .Select(i => new { Handler = t, Interface = i }));

        foreach (var h in handlers)
            services.AddTransient(h.Interface, h.Handler);

        services.AddScoped<IUseCaseDispatcher, UseCaseDispatcher>();
        return services;
    }

    public static IServiceCollection AddUseCaseMiddleware<T>(this IServiceCollection services)
        where T : class, IUseCaseMiddleware
    {
        services.AddScoped<IUseCaseMiddleware, T>();
        return services;
    }
}

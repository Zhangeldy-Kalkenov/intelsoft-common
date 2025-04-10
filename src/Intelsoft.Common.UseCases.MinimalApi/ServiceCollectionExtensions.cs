using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Intelsoft.Common.UseCases.MinimalApi;

public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Registers all types implementing <see cref="IEndpointModule"/> from the specified assemblies.
    /// </summary>
    public static IServiceCollection AddEndpointModules(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var type in assemblies
                     .SelectMany(a => a.GetTypes())
                     .Where(t => !t.IsAbstract && typeof(IEndpointModule).IsAssignableFrom(t)))
        {
            services.AddScoped(typeof(IEndpointModule), type);
        }

        return services;
    }

    /// <summary>
    ///     Registers all <see cref="IEndpointModule"/> types from the calling assembly.
    /// </summary>
    public static IServiceCollection AddEndpointModulesFromCurrentAssembly(this IServiceCollection services)
    {
        return services.AddEndpointModules(Assembly.GetCallingAssembly());
    }

    /// <summary>
    ///     Registers all <see cref="IEndpointModule"/> types from the entry assembly.
    /// </summary>
    public static IServiceCollection AddEndpointModulesFromEntryAssembly(this IServiceCollection services)
    {
        var entry = Assembly.GetEntryAssembly();
        return entry is not null ? services.AddEndpointModules(entry) : services;
    }
}

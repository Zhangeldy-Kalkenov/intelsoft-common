using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Nomad.Common.SeedWork;

namespace Nomad.Common.EntityFrameworkCore;

/// <summary>
///     Scans assemblies and registers all concrete EfRepository implementations as IRepository.
/// </summary>
public static class EfRepositoryScanner
{
    /// <summary>
    ///     Registers all EfRepository-based repositories found in the specified assemblies.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="assemblies">Assemblies to scan.</param>
    public static IServiceCollection AddEfRepositoriesFromAssemblies(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        var repoBaseType = typeof(EfRepository<,>);
        var repoInterfaceType = typeof(IRepository<,>);

        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes()
                .Where(t => t is { IsAbstract: false, IsInterface: false, IsClass: true })
                .Where(t =>
                    t.BaseType is { IsGenericType: true } &&
                    t.BaseType.GetGenericTypeDefinition() == repoBaseType);

            foreach (var type in types)
            {
                var baseType = type.BaseType!;
                var genericArgs = baseType.GetGenericArguments();
                var aggregateType = genericArgs[0];
                var idType = genericArgs[1];

                var interfaceType = repoInterfaceType.MakeGenericType(aggregateType, idType);
                services.AddScoped(interfaceType, type);
            }
        }

        return services;
    }
}

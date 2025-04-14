using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Nomad.Common.SeedWork.Internal;

namespace Nomad.Common.SeedWork;

public static class DomainEventServiceCollectionExtensions
{
    public static IServiceCollection AddDomainEventDispatcher(this IServiceCollection services,
        params Assembly[] assembliesToScan)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        var handlerInterfaceType = typeof(IDomainEventHandler<>);

        foreach (var assembly in assembliesToScan)
        {
            var handlerTypes = assembly
                .GetTypes()
                .Where(type => type is { IsAbstract: false, IsInterface: false })
                .SelectMany(type =>
                    type.GetInterfaces()
                        .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterfaceType)
                        .Select(i => new { Interface = i, Implementation = type }));

            foreach (var handler in handlerTypes)
            {
                services.AddScoped(handler.Interface, handler.Implementation);
            }
        }

        return services;
    }
}

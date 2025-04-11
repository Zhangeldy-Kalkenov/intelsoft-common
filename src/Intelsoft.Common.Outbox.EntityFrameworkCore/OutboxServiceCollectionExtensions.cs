using Microsoft.Extensions.DependencyInjection;

namespace Intelsoft.Common.Outbox.EntityFrameworkCore;

/// <summary>
///     Extension methods for registering EF Core-based outbox support.
/// </summary>
public static class OutboxServiceCollectionExtensions
{
    /// <summary>
    ///     Registers EF Core-based outbox writer, publisher, serializer and background publisher.
    /// </summary>
    public static IServiceCollection AddEfOutbox(this IServiceCollection services)
    {
        services.AddScoped<IOutboxWriter, EfOutboxWriter>();
        services.AddScoped<IOutboxPublisher, EfOutboxPublisher>();
        services.AddScoped<IOutboxMessageStore, EfOutboxMessageStore>();
        services.AddSingleton<IOutboxSerializer, JsonOutboxSerializer>();

        services.AddHostedService<OutboxBackgroundService>();

        return services;
    }
}

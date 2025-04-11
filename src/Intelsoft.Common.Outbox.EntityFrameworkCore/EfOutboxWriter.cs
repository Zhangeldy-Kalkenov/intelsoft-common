using Intelsoft.Common.IntegrationEvents.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Intelsoft.Common.Outbox.EntityFrameworkCore;

/// <summary>
///     Persists integration events into the outbox table using Entity Framework.
/// </summary>
public sealed class EfOutboxWriter(DbContext dbContext, IOutboxSerializer serializer) : IOutboxWriter
{
    public Task WriteAsync<TEvent>(string topic, TEvent integrationEvent, CancellationToken cancellationToken = default)
        where TEvent : IIntegrationEvent
    {
        var message = new OutboxMessage
        {
            Id = Guid.NewGuid(),
            Type = typeof(TEvent).AssemblyQualifiedName!,
            Payload = serializer.Serialize(integrationEvent),
            OccurredOn = integrationEvent.OccurredOn,
            CorrelationId = integrationEvent.CorrelationId,
            Topic = topic
        };

        dbContext.Set<OutboxMessage>().Add(message);
        return Task.CompletedTask;
    }
}

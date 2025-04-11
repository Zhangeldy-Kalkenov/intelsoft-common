using Intelsoft.Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;

namespace Intelsoft.Common.Outbox.EntityFrameworkCore;

public sealed class EfOutboxPublisher(
    DbContext db,
    IIntegrationEventPublisher eventPublisher,
    IOutboxSerializer serializer)
    : IOutboxPublisher
{
    public async Task PublishPendingAsync(CancellationToken cancellationToken = default)
    {
        var messages = await db.Set<OutboxMessage>()
            .Where(m => m.ProcessedAt == null)
            .OrderBy(m => m.OccurredOn)
            .Take(100)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        if (messages.Count == 0)
            return;

        foreach (var message in messages)
        {
            var type = Type.GetType(message.Type);
            if (type is null) continue;

            var @event = serializer.Deserialize(message.Payload, message.Type);

            await eventPublisher.PublishAsync(message.Topic, @event, cancellationToken).ConfigureAwait(false);

            message.ProcessedAt = DateTime.UtcNow;
        }

        await db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}

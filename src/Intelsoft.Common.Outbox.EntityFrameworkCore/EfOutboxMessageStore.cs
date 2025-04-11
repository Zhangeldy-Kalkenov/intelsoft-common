using Microsoft.EntityFrameworkCore;

namespace Intelsoft.Common.Outbox.EntityFrameworkCore;

/// <summary>
///     Provides access to unprocessed outbox messages via Entity Framework.
/// </summary>
public sealed class EfOutboxMessageStore(DbContext dbContext) : IOutboxMessageStore
{
    public async Task<IReadOnlyList<OutboxMessage>> GetUnprocessedAsync(int maxCount,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<OutboxMessage>()
            .Where(x => x.ProcessedAt == null)
            .OrderBy(x => x.OccurredOn)
            .Take(maxCount)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task MarkAsProcessedAsync(Guid messageId, DateTime processedAtUtc,
        CancellationToken cancellationToken = default)
    {
        var message = await dbContext.Set<OutboxMessage>()
            .FirstOrDefaultAsync(m => m.Id == messageId, cancellationToken)
            .ConfigureAwait(false);

        if (message is null) return;

        message.ProcessedAt = processedAtUtc;
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}

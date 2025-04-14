namespace Nomad.Common.Outbox;

/// <summary>
///     Provides access to the underlying store of outbox messages.
/// </summary>
public interface IOutboxMessageStore
{
    Task<IReadOnlyList<OutboxMessage>> GetUnprocessedAsync(int maxCount, CancellationToken cancellationToken = default);

    Task MarkAsProcessedAsync(Guid messageId, DateTime processedAtUtc, CancellationToken cancellationToken = default);
}

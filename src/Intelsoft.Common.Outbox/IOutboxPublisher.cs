namespace Intelsoft.Common.Outbox;

/// <summary>
///     Processes and publishes integration events stored in the outbox.
/// </summary>
public interface IOutboxPublisher
{
    /// <summary>
    ///     Publishes all pending events from the outbox store.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task PublishPendingAsync(CancellationToken cancellationToken = default);
}

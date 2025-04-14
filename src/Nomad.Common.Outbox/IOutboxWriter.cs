using Nomad.Common.IntegrationEvents.Contracts;

namespace Nomad.Common.Outbox;

/// <summary>
///     Provides a contract for storing integration events into an outbox store during the current transaction.
/// </summary>
public interface IOutboxWriter
{
    /// <summary>
    ///     Writes a serialized integration event to the outbox.
    /// </summary>
    /// <typeparam name="TEvent">The type of the integration event.</typeparam>
    /// <param name="topic">The target topic name for the event (e.g., Kafka or RabbitMQ topic).</param>
    /// <param name="integrationEvent">The event to persist.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task WriteAsync<TEvent>(
        string topic,
        TEvent integrationEvent,
        CancellationToken cancellationToken = default)
        where TEvent : IIntegrationEvent;
}

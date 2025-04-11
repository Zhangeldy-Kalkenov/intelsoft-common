using Intelsoft.Common.IntegrationEvents.Contracts;

namespace Intelsoft.Common.IntegrationEvents;

/// <summary>
///     Publishes integration events to an external bus or transport.
/// </summary>
public interface IIntegrationEventPublisher
{
    Task PublishAsync<TEvent>(
        string topic,
        TEvent integrationEvent,
        CancellationToken cancellationToken = default)
        where TEvent : IIntegrationEvent;
}

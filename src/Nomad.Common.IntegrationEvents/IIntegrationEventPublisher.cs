using Nomad.Common.IntegrationEvents.Contracts;

namespace Nomad.Common.IntegrationEvents;

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

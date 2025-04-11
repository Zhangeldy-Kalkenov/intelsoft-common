using Intelsoft.Common.IntegrationEvents.Contracts;

namespace Intelsoft.Common.IntegrationEvents;

/// <summary>
///     Handles incoming integration events from other services.
/// </summary>
/// <typeparam name="TEvent">The type of the integration event.</typeparam>
public interface IIntegrationEventHandler<in TEvent>
    where TEvent : IIntegrationEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}

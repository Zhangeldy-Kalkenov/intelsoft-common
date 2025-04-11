namespace Intelsoft.Common.SeedWork;

/// <summary>
///     Defines a handler for a specific type of domain event.
///     Implementations contain the logic to react to domain events when they are dispatched.
/// </summary>
/// <typeparam name="TDomainEvent">The type of the domain event.</typeparam>
public interface IDomainEventHandler<in TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    /// <summary>
    ///     Handles the specified domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event to handle.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous handling operation.</returns>
    Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}

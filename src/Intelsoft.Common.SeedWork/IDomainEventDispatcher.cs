namespace Intelsoft.Common.SeedWork;

/// <summary>
///     Defines a contract for dispatching domain events to their respective handlers.
///     Typically used after changes have been persisted to notify other parts of the system.
/// </summary>
public interface IDomainEventDispatcher
{
    /// <summary>
    ///     Dispatches a collection of domain events to their registered handlers.
    /// </summary>
    /// <param name="domainEvents">The collection of domain events to dispatch.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous dispatch operation.</returns>
    ValueTask DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}

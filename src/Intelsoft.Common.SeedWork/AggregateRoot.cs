using System.Collections.ObjectModel;

namespace Intelsoft.Common.SeedWork;

/// <summary>
///     Represents the root of an aggregate in Domain-Driven Design.
///     An aggregate root is an entity that controls access to other entities within the aggregate.
/// </summary>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    private List<IDomainEvent>? _domainEvents;

    /// <summary>
    ///     Gets the domain events that occurred within the aggregate.
    ///     Returns an empty collection if no events have been added.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents =>
        _domainEvents != null
            ? new ReadOnlyCollection<IDomainEvent>(_domainEvents)
            : Array.Empty<IDomainEvent>();

    /// <summary>
    ///     Adds a domain event to the aggregate.
    /// </summary>
    /// <param name="domainEvent">The domain event to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when the domain event is null.</exception>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);
        _domainEvents ??= [];
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    ///     Removes a domain event from the aggregate, if it exists.
    /// </summary>
    /// <param name="domainEvent">The domain event to remove.</param>
    protected void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents?.Remove(domainEvent);
        if (_domainEvents is { Count: 0 })
            _domainEvents = null;
    }

    /// <summary>
    ///     Clears all domain events from the aggregate.
    /// </summary>
    public void ClearDomainEvents() => _domainEvents = null;

    /// <summary>
    ///     Returns true if there are any domain events queued in the aggregate.
    /// </summary>
    public bool HasDomainEvents => _domainEvents is { Count: > 0 };
}

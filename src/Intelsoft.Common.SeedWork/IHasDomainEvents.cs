namespace Intelsoft.Common.SeedWork;

/// <summary>
/// Indicates that an entity exposes domain events for dispatching after persistence.
/// </summary>
public interface IHasDomainEvents
{
    /// <summary>
    /// Gets the domain events associated with the entity.
    /// </summary>
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// Clears the domain events after dispatching.
    /// </summary>
    void ClearDomainEvents();
}

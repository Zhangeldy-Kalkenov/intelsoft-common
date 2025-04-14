namespace Nomad.Common.SeedWork;

/// <summary>
///     Represents a domain event that captures something important that happened within the domain.
///     Domain events are typically used to notify other parts of the system about state changes.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    ///     Gets the UTC timestamp when the domain event occurred.
    /// </summary>
    DateTime OccurredOn { get; }
}

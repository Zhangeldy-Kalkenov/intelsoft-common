using Nomad.Common.SeedWork;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Nomad.Common.EntityFrameworkCore;

/// <summary>
///     Extension methods for extracting domain events from EF Core ChangeTracker.
/// </summary>
public static class ChangeTrackerExtensions
{
    /// <summary>
    ///     Retrieves all domain events from tracked entities that implement <see cref="IHasDomainEvents"/>.
    /// </summary>
    public static List<IDomainEvent> GetDomainEvents(this ChangeTracker changeTracker)
    {
        return changeTracker.Entries<IHasDomainEvents>()
            .Where(e => e.Entity.DomainEvents.Count > 0)
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();
    }

    /// <summary>
    ///     Clears domain events on all tracked entities that implement <see cref="IHasDomainEvents"/>.
    /// </summary>
    public static void ClearDomainEvents(this ChangeTracker changeTracker)
    {
        foreach (var entry in changeTracker.Entries<IHasDomainEvents>())
        {
            entry.Entity.ClearDomainEvents();
        }
    }
}

using Intelsoft.Common.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Intelsoft.Common.EntityFrameworkCore;

/// <summary>
///     Extension methods for working with domain events in DbContext.
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    ///     Dispatches domain events after calling SaveChangesAsync and clears them.
    /// </summary>
    /// <param name="context">The current DbContext instance.</param>
    /// <param name="dispatcher">Domain event dispatcher to use.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public static async Task<int> SaveChangesAndDispatchDomainEventsAsync(
        this DbContext context,
        IDomainEventDispatcher dispatcher,
        CancellationToken cancellationToken = default)
    {
        var domainEvents = context.ChangeTracker.GetDomainEvents();

        if (domainEvents.Count > 0)
            await dispatcher.DispatchAsync(domainEvents, cancellationToken).ConfigureAwait(false);

        context.ChangeTracker.ClearDomainEvents();

        return await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}

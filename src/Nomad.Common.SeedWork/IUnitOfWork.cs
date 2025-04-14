namespace Nomad.Common.SeedWork;

/// <summary>
///     Defines a unit of work that commits a set of changes atomically.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Persists all changes made within the current unit of work.
    /// </summary>
    /// <returns>The number of affected entities.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

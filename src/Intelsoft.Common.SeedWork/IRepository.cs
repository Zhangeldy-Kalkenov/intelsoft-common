namespace Intelsoft.Common.SeedWork;

/// <summary>
///     Represents a generic repository for working with aggregate roots,
///     providing querying, paging, existence checks, and CRUD operations.
/// </summary>
/// <typeparam name="TAggregate">The aggregate root type.</typeparam>
/// <typeparam name="TId">The aggregate identifier type.</typeparam>
public interface IRepository<TAggregate, in TId>
    where TAggregate : AggregateRoot<TId>
    where TId : notnull
{
    /// <summary>
    ///     Gets an aggregate by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the aggregate.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task<TAggregate?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns all aggregates in the repository.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task<IReadOnlyList<TAggregate>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Adds a new aggregate to the repository.
    /// </summary>
    /// <param name="aggregate">The aggregate to add.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task AddAsync(TAggregate aggregate, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Removes an aggregate from the repository.
    /// </summary>
    /// <param name="aggregate">The aggregate to remove.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task RemoveAsync(TAggregate aggregate, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updates a detached or tracked aggregate in the repository.
    /// </summary>
    /// <param name="aggregate">The aggregate to update.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns all aggregates that match the provided specification.
    /// </summary>
    /// <param name="specification">The filtering criteria.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task<IReadOnlyList<TAggregate>> FindAsync(
        ISpecification<TAggregate> specification,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns the total number of aggregates.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task<int> CountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns the total number of aggregates matching the specification.
    /// </summary>
    /// <param name="specification">The filtering criteria.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task<int> CountAsync(
        ISpecification<TAggregate> specification,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns true if at least one aggregate matches the specification.
    /// </summary>
    /// <param name="specification">The filtering criteria.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task<bool> ExistsAsync(
        ISpecification<TAggregate> specification,
        CancellationToken cancellationToken = default);
}

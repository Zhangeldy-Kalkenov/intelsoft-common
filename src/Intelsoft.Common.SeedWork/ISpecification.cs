using System.Linq.Expressions;

namespace Intelsoft.Common.SeedWork;

/// <summary>
///     Describes a query specification that encapsulates filtering logic,
///     includes, ordering, pagination, and query options.
/// </summary>
/// <typeparam name="T">The type of the entity being queried.</typeparam>
public interface ISpecification<T>
{
    /// <summary>
    ///     Gets the filtering criteria for the specification.
    /// </summary>
    Expression<Func<T, bool>>? Criteria { get; }

    /// <summary>
    ///     Gets a collection of expression-based includes for eager loading.
    /// </summary>
    IReadOnlyList<Expression<Func<T, object>>> Includes { get; }

    /// <summary>
    ///     Gets a collection of string-based includes for advanced scenarios.
    /// </summary>
    IReadOnlyList<string> IncludeStrings { get; }

    /// <summary>
    ///     Gets the ascending order expression (if any).
    /// </summary>
    Expression<Func<T, object>>? OrderBy { get; }

    /// <summary>
    ///     Gets the descending order expression (if any).
    /// </summary>
    Expression<Func<T, object>>? OrderByDescending { get; }

    /// <summary>
    ///     Gets the number of items to skip.
    /// </summary>
    int? Skip { get; }

    /// <summary>
    ///     Gets the number of items to take.
    /// </summary>
    int? Take { get; }

    /// <summary>
    ///     Indicates whether the query should be executed without tracking.
    /// </summary>
    bool AsNoTracking { get; }
}

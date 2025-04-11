using System.Linq.Expressions;

namespace Intelsoft.Common.SeedWork;

/// <summary>
///     A flexible specification pattern implementation with support for filtering, includes, ordering and paging.
/// </summary>
/// <typeparam name="T">The type of entity to filter.</typeparam>
public abstract class Specification<T> : ISpecification<T>
{
    private readonly List<Expression<Func<T, object>>> _includes = new();
    private readonly List<string> _includeStrings = new();

    public Expression<Func<T, bool>> Criteria { get; protected init; } = _ => true;

    public IReadOnlyList<Expression<Func<T, object>>> Includes => _includes;
    public IReadOnlyList<string> IncludeStrings => _includeStrings;

    public Expression<Func<T, object>>? OrderBy { get; protected set; }
    public Expression<Func<T, object>>? OrderByDescending { get; protected set; }

    public int? Skip { get; protected set; }
    public int? Take { get; protected set; }

    public bool AsNoTracking { get; protected set; }

    /// <summary>
    ///     Returns the predicate used to filter entities.
    /// </summary>
    public Expression<Func<T, bool>> ToExpression() => Criteria;

    /// <summary>
    ///     Adds an expression-based include for eager loading.
    /// </summary>
    protected void AddInclude(Expression<Func<T, object>> includeExpression) =>
        _includes.Add(includeExpression);

    /// <summary>
    ///     Adds a string-based include (e.g. for nested navigation properties).
    /// </summary>
    protected void AddInclude(string includeString) =>
        _includeStrings.Add(includeString);

    /// <summary>
    ///     Applies an ascending ordering to the query.
    /// </summary>
    protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression) =>
        OrderBy = orderByExpression;

    /// <summary>
    ///     Applies a descending ordering to the query.
    /// </summary>
    protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescExpression) =>
        OrderByDescending = orderByDescExpression;

    /// <summary>
    ///     Applies pagination to the query.
    /// </summary>
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
    }

    /// <summary>
    ///     Marks the query as no-tracking (read-only).
    /// </summary>
    protected void ApplyAsNoTracking() => AsNoTracking = true;
}

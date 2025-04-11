using Microsoft.EntityFrameworkCore;
using Intelsoft.Common.SeedWork;

namespace Intelsoft.Common.EntityFrameworkCore;

/// <summary>
///     Applies specifications to a query in an optimized, low-allocation manner.
/// </summary>
public static class SpecificationEvaluator
{
    public static IQueryable<T> Apply<T>(IQueryable<T> source, ISpecification<T> spec) where T : class
    {
        var query = source;

        if (spec.Criteria is not null)
        {
            query = query.Where(spec.Criteria);
        }

        if (spec.AsNoTracking)
        {
            query = query.AsNoTracking();
        }

        if (spec.Includes.Count > 0)
        {
            for (var i = 0; i < spec.Includes.Count; i++)
            {
                query = query.Include(spec.Includes[i]);
            }
        }

        if (spec.IncludeStrings.Count > 0)
        {
            for (var i = 0; i < spec.IncludeStrings.Count; i++)
            {
                query = query.Include(spec.IncludeStrings[i]);
            }
        }

        if (spec.OrderBy is not null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        else if (spec.OrderByDescending is not null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        if (spec.Skip.HasValue)
        {
            query = query.Skip(spec.Skip.Value);
        }

        if (spec.Take.HasValue)
        {
            query = query.Take(spec.Take.Value);
        }

        return query;
    }
}

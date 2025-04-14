using Nomad.Common.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Nomad.Common.EntityFrameworkCore;

public class EfRepository<TAggregate, TId>(DbContext db) : IRepository<TAggregate, TId>
    where TAggregate : AggregateRoot<TId>
    where TId : notnull
{
    public Task AddAsync(TAggregate aggregate, CancellationToken cancellationToken = default) =>
        db.Set<TAggregate>().AddAsync(aggregate, cancellationToken).AsTask();

    public Task RemoveAsync(TAggregate aggregate, CancellationToken cancellationToken = default)
    {
        db.Set<TAggregate>().Remove(aggregate);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken = default)
    {
        db.Set<TAggregate>().Update(aggregate);
        return Task.CompletedTask;
    }

    public Task<TAggregate?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        var key = ExtractKey(id);
        return db.Set<TAggregate>().FindAsync([key], cancellationToken).AsTask();
    }

    public async Task<IReadOnlyList<TAggregate>> GetAllAsync(CancellationToken cancellationToken = default)
        => await db.Set<TAggregate>().ToListAsync(cancellationToken).ConfigureAwait(false);

    public async Task<IReadOnlyList<TAggregate>> FindAsync(ISpecification<TAggregate> specification,
        CancellationToken cancellationToken = default)
        => await SpecificationEvaluator.Apply(db.Set<TAggregate>(), specification).ToListAsync(cancellationToken)
            .ConfigureAwait(false);

    public Task<int> CountAsync(CancellationToken cancellationToken = default) =>
        db.Set<TAggregate>().CountAsync(cancellationToken);

    public Task<int> CountAsync(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default)
        => SpecificationEvaluator.Apply(db.Set<TAggregate>(), specification).CountAsync(cancellationToken);

    public Task<bool> ExistsAsync(ISpecification<TAggregate> specification,
        CancellationToken cancellationToken = default)
        => SpecificationEvaluator.Apply(db.Set<TAggregate>(), specification).AnyAsync(cancellationToken);

    private static object ExtractKey(TId id)
    {
        var identityInterface = typeof(TId).GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IIdentity<>));

        if (identityInterface == null)
        {
            return id;
        }

        var valueProp = typeof(TId).GetProperty(nameof(IIdentity<object>.Value));
        return valueProp?.GetValue(id)
               ?? throw new InvalidOperationException("Id.Value is null");
    }
}

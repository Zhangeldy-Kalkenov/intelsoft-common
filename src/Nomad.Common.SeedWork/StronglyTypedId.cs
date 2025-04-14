namespace Nomad.Common.SeedWork;

/// <summary>
///     A strongly-typed identifier base class for domain entities and value objects.
/// </summary>
/// <typeparam name="TValue">The type of the underlying value.</typeparam>
public abstract record StronglyTypedId<TValue>(TValue Value) : IIdentity<TValue>
    where TValue : notnull
{
    public override string ToString() => Value.ToString()!;
}

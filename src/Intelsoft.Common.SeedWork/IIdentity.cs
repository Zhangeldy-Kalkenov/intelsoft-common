namespace Intelsoft.Common.SeedWork;

/// <summary>
///     Represents a strongly-typed identifier with a scalar underlying value.
/// </summary>
/// <typeparam name="TValue">The underlying scalar value type (e.g. Guid, int).</typeparam>
public interface IIdentity<out TValue>
    where TValue : notnull
{
    /// <summary>
    ///     Gets the underlying scalar value.
    /// </summary>
    TValue Value { get; }
}

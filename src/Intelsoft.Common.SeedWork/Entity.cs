namespace Intelsoft.Common.SeedWork;

/// <summary>
///     Represents the base class for all entities in a domain model.
///     Entities are compared by their identity, not by their attributes.
/// </summary>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public abstract class Entity<TId>
    where TId : notnull
{
    /// <summary>
    ///     Gets the unique identifier of the entity.
    /// </summary>
    public required TId Id { get; init; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Entity{TId}"/> class.
    ///     Required by some ORMs and serializers.
    /// </summary>
    protected Entity()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Entity{TId}"/> class with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    protected Entity(TId id)
    {
        Id = id;
    }

    /// <summary>
    ///     Determines whether the specified object is equal to the current entity.
    ///     Entities are equal if their types and identifiers are equal.
    /// </summary>
    /// <param name="obj">The object to compare with the current entity.</param>
    /// <returns>true if the specified object is equal to the current entity; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
            return false;

        if (GetType() != other.GetType())
            return false;

        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    /// <summary>
    ///     Returns a hash code for the entity based on its identifier.
    /// </summary>
    /// <returns>A hash code for the current entity.</returns>
    public override int GetHashCode() =>
        EqualityComparer<TId>.Default.GetHashCode(Id);
}

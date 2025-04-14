namespace Nomad.Common.Results;

/// <summary>
///     Interface representing a result of an operation.
/// </summary>
public interface IResult
{
    /// <summary>
    ///     Indicates whether the operation succeeded.
    /// </summary>
    bool Succeeded { get; }

    /// <summary>
    ///     Indicates whether the operation failed.
    /// </summary>
    bool Failed { get; }

    /// <summary>
    ///     The reason for failure if the operation did not succeed.
    /// </summary>
    Error? FailureReason { get; }
}

/// <summary>
///     Represents a non-generic result of an operation, indicating success or failure.
/// </summary>
public readonly record struct Result : IResult
{
    /// <inheritdoc />
    public bool Succeeded { get; init; }

    /// <inheritdoc />
    public bool Failed => !Succeeded;

    /// <inheritdoc />
    public Error? FailureReason { get; init; }

    /// <summary>
    ///     Creates a successful result.
    /// </summary>
    public static Result Success() => new() { Succeeded = true };

    /// <summary>
    ///     Creates a failed result with the specified error.
    /// </summary>
    /// <param name="error">The reason for the failure.</param>
    public static Result Failure(Error error) => new() { Succeeded = false, FailureReason = error };

    public static implicit operator Result(Error error) => Failure(error);
}

/// <summary>
///     Represents a result of an operation that returns a value on success.
/// </summary>
/// <typeparam name="T">The type of the value returned on success.</typeparam>
public readonly record struct Result<T> : IResult
{
    /// <inheritdoc />
    public bool Succeeded { get; init; }

    /// <inheritdoc />
    public bool Failed => !Succeeded;

    /// <inheritdoc />
    public Error? FailureReason { get; init; }

    /// <summary>
    ///     The value returned when the operation succeeds. Will be <c>null</c> if failed.
    /// </summary>
    public T? Value { get; init; }

    /// <summary>
    ///     Creates a successful result with the given value.
    /// </summary>
    /// <param name="value">The result value.</param>
    public static Result<T> Success(T value) => new() { Succeeded = true, Value = value };

    /// <summary>
    ///     Creates a failed result with the specified error.
    /// </summary>
    /// <param name="error">The reason for the failure.</param>
    public static Result<T> Failure(Error error) => new() { Succeeded = false, FailureReason = error };

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
}

/// <summary>
///     Represents a paginated result of an operation.
/// </summary>
/// <typeparam name="T">The type of the items returned on success.</typeparam>
public readonly record struct PagedResult<T> : IResult
{
    /// <inheritdoc />
    public bool Succeeded { get; init; }

    /// <inheritdoc />
    public bool Failed => !Succeeded;

    /// <inheritdoc />
    public Error? FailureReason { get; init; }

    /// <summary>
    ///     The list of items returned on success.
    /// </summary>
    public IReadOnlyList<T>? Items { get; init; }

    /// <summary>
    ///     The total number of items in the data source.
    /// </summary>
    public int TotalCount { get; init; }

    /// <summary>
    ///     The number of items per page.
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    ///     The current page index (starting from 1).
    /// </summary>
    public int PageNumber { get; init; }

    /// <summary>
    ///     Creates a successful paged result.
    /// </summary>
    public static PagedResult<T> Success(
        IReadOnlyList<T> items,
        int totalCount,
        int pageSize,
        int pageNumber) =>
        new()
        {
            Succeeded = true,
            Items = items,
            TotalCount = totalCount,
            PageSize = pageSize,
            PageNumber = pageNumber
        };

    /// <summary>
    ///     Creates a failed paged result with the specified error.
    /// </summary>
    public static PagedResult<T> Failure(Error error) =>
        new() { Succeeded = false, FailureReason = error };

    public static implicit operator PagedResult<T>(Error error) => Failure(error);
}

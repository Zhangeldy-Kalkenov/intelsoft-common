namespace Intelsoft.Common.Results;

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

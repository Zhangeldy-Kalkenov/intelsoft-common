namespace Intelsoft.Common.Results;

/// <summary>
///     Represents a standardized error with a type, code, and message.
/// </summary>
public sealed record Error
{
    /// <summary>
    ///     Gets the error code representing the specific type of error.
    /// </summary>
    public string Code { get; init; } = null!;

    /// <summary>
    ///     Gets the human-readable message that describes the error.
    /// </summary>
    public string Message { get; init; } = null!;

    /// <summary>
    ///     Gets the type of the error, indicating its category.
    /// </summary>
    public ErrorType Type { get; init; } = ErrorType.Internal;

    /// <summary>
    ///     Creates a validation error.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="code">Optional custom error code. Defaults to "validation_error".</param>
    /// <returns>A new <see cref="Error"/> instance of type <see cref="ErrorType.Validation"/>.</returns>
    public static Error Validation(string message, string? code = null) =>
        new() { Code = code ?? "validation_error", Message = message, Type = ErrorType.Validation };

    /// <summary>
    ///     Creates a not found error.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="code">Optional custom error code. Defaults to "not_found".</param>
    /// <returns>A new <see cref="Error"/> instance of type <see cref="ErrorType.NotFound"/>.</returns>
    public static Error NotFound(string message, string? code = null) =>
        new() { Code = code ?? "not_found", Message = message, Type = ErrorType.NotFound };

    /// <summary>
    ///     Creates a conflict error.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="code">Optional custom error code. Defaults to "conflict".</param>
    /// <returns>A new <see cref="Error"/> instance of type <see cref="ErrorType.Conflict"/>.</returns>
    public static Error Conflict(string message, string? code = null) =>
        new() { Code = code ?? "conflict", Message = message, Type = ErrorType.Conflict };

    /// <summary>
    ///     Creates an unexpected/internal error.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="code">Optional custom error code. Defaults to "internal_error".</param>
    /// <returns>A new <see cref="Error"/> instance of type <see cref="ErrorType.Internal"/>.</returns>
    public static Error Internal(string message, string? code = null) =>
        new() { Code = code ?? "internal_error", Message = message, Type = ErrorType.Internal };
}

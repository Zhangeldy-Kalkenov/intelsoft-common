namespace Intelsoft.Common.Results;

/// <summary>
///     Represents the type or category of an error in a result.
/// </summary>
public enum ErrorType
{
    /// <summary>
    ///     The error is due to a validation failure (e.g., invalid input).
    /// </summary>
    Validation,

    /// <summary>
    ///     The requested resource was not found.
    /// </summary>
    NotFound,

    /// <summary>
    ///     The operation caused a conflict, such as a duplicate entry.
    /// </summary>
    Conflict,

    /// <summary>
    ///     An unexpected or internal system error occurred.
    /// </summary>
    Internal
}

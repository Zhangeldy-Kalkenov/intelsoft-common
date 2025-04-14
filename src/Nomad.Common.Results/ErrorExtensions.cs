namespace Nomad.Common.Results;

/// <summary>
///     Provides extension methods for converting <see cref="Error"/> instances into <see cref="Result"/> types.
/// </summary>
public static class ErrorExtensions
{
    /// <summary>
    ///     Converts the specified <see cref="Error"/> into a failed <see cref="Result"/>.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    /// <returns>A failed result containing the specified error.</returns>
    public static Result ToResult(this Error error) =>
        Result.Failure(error);

    /// <summary>
    ///     Converts the specified <see cref="Error"/> into a failed <see cref="Result{T}"/>.
    /// </summary>
    /// <typeparam name="T">The expected type of the result value.</typeparam>
    /// <param name="error">The error to convert.</param>
    /// <returns>A failed result of type <typeparamref name="T"/> containing the specified error.</returns>
    public static Result<T> ToResult<T>(this Error error) =>
        Result<T>.Failure(error);
}

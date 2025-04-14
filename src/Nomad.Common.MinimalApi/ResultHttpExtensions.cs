using Nomad.Common.Results;
using Microsoft.AspNetCore.Http;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Nomad.Common.MinimalApi;

public static class ResultHttpExtensions
{
    public static IResult ToHttpResult(this Result result)
    {
        if (result.Succeeded)
            return TypedResults.NoContent();

        return result.FailureReason?.Type switch
        {
            ErrorType.Validation => TypedResults.BadRequest(result.FailureReason),
            ErrorType.NotFound   => TypedResults.NotFound(result.FailureReason),
            ErrorType.Conflict   => TypedResults.Conflict(result.FailureReason),
            _                    => TypedResults.Problem(
                title: result.FailureReason?.Type.ToString(),
                detail: result.FailureReason?.Message,
                statusCode: StatusCodes.Status500InternalServerError)
        };
    }

    public static IResult ToHttpResult<T>(this Result<T> result)
    {
        if (result.Succeeded)
            return TypedResults.Ok(result.Value);

        return result.FailureReason?.Type switch
        {
            ErrorType.Validation => TypedResults.BadRequest(result.FailureReason),
            ErrorType.NotFound   => TypedResults.NotFound(result.FailureReason),
            ErrorType.Conflict   => TypedResults.Conflict(result.FailureReason),
            _                    => TypedResults.Problem(
                title: result.FailureReason?.Type.ToString(),
                detail: result.FailureReason?.Message,
                statusCode: StatusCodes.Status500InternalServerError)
        };
    }

    public static IResult ToHttpResult<T>(this Error error) =>
        error.ToResult<T>().ToHttpResult();

    public static IResult ToHttpResult(this Error error) =>
        error.ToResult().ToHttpResult();
}

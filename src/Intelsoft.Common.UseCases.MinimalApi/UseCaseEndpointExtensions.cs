using Intelsoft.Common.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Intelsoft.Common.UseCases.MinimalApi;

public static class UseCaseEndpointExtensions
{
    public static RouteHandlerBuilder MapUseCasePost<TRequest, TResponse>(
        this IEndpointRouteBuilder app, string pattern, Func<TResponse, string>? locationBuilder = null)
        where TRequest : class
    {
        return app.MapPost(pattern, async (
            TRequest request,
            IUseCaseDispatcher dispatcher,
            CancellationToken ct) =>
        {
            var response = await dispatcher.ExecuteAsync<TRequest, TResponse>(request, ct).ConfigureAwait(false);
            if (locationBuilder is null)
            {
                return ToHttpResult(response);
            }

            var location = locationBuilder(response);
            return TypedResults.Created(location);

        });
    }

    public static RouteHandlerBuilder MapUseCaseGetById<TRequest, TResponse>(
        this IEndpointRouteBuilder app, string pattern, Func<Guid, TRequest> factory)
    {
        return app.MapGet(pattern, async (
            Guid id,
            IUseCaseDispatcher dispatcher,
            CancellationToken ct) =>
        {
            var request = factory(id);
            var response = await dispatcher.ExecuteAsync<TRequest, TResponse>(request, ct).ConfigureAwait(false);
            return ToHttpResult(response);
        });
    }

    public static RouteHandlerBuilder MapUseCasePut<TRequest, TResponse>(
        this IEndpointRouteBuilder app, string pattern)
        where TRequest : class
    {
        return app.MapPut(pattern, async (
            TRequest request,
            IUseCaseDispatcher dispatcher,
            CancellationToken ct) =>
        {
            var response = await dispatcher.ExecuteAsync<TRequest, TResponse>(request, ct).ConfigureAwait(false);
            return ToHttpResult(response);
        });
    }

    public static RouteHandlerBuilder MapUseCasePatch<TRequest, TResponse>(
        this IEndpointRouteBuilder app, string pattern)
        where TRequest : class
    {
        return app.MapPatch(pattern, async (
            TRequest request,
            IUseCaseDispatcher dispatcher,
            CancellationToken ct) =>
        {
            var response = await dispatcher.ExecuteAsync<TRequest, TResponse>(request, ct).ConfigureAwait(false);
            return ToHttpResult(response);
        });
    }

    public static RouteHandlerBuilder MapUseCaseDelete<TRequest, TResponse>(
        this IEndpointRouteBuilder app, string pattern, Func<Guid, TRequest> factory)
    {
        return app.MapDelete(pattern, async (
            Guid id,
            IUseCaseDispatcher dispatcher,
            CancellationToken ct) =>
        {
            var request = factory(id);
            var response = await dispatcher.ExecuteAsync<TRequest, TResponse>(request, ct).ConfigureAwait(false);
            return ToHttpResult(response);
        });
    }

    private static IResult ToHttpResult<TResponse>(TResponse response)
    {
        return response switch
        {
            Result r => r.ToHttpResult(),
            Result<TResponse> rt => rt.ToHttpResult(),
            _ => TypedResults.Ok(response)
        };
    }
}

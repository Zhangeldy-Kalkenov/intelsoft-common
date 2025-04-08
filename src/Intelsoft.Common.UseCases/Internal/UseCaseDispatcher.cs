using Microsoft.Extensions.DependencyInjection;

namespace Intelsoft.Common.UseCases.Internal;

internal sealed class UseCaseDispatcher(
    IServiceProvider provider,
    IEnumerable<IUseCaseMiddleware> middlewareEnumerable)
    : IUseCaseDispatcher
{
    private readonly IUseCaseMiddleware[] _middlewares = middlewareEnumerable as IUseCaseMiddleware[]
                                                         ?? middlewareEnumerable.ToArray();

    public Task<TResponse> ExecuteAsync<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        return InvokePipeline<TRequest, TResponse>(request, 0, cancellationToken);
    }

    private Task<TResponse> InvokePipeline<TRequest, TResponse>(
        TRequest request,
        int index,
        CancellationToken cancellationToken)
    {
        if (index < _middlewares.Length)
        {
            var middleware = _middlewares[index];
            return middleware.InvokeAsync<TRequest, TResponse>(
                request,
                static (req, state, ct) =>
                {
                    var dispatcherState = (DispatcherState)state!;
                    return dispatcherState.Dispatcher.InvokePipeline<TRequest, TResponse>(req,
                        dispatcherState.Index + 1, ct);
                },
                new DispatcherState(this, index),
                cancellationToken
            );
        }

        var handler = provider.GetRequiredService<IUseCase<TRequest, TResponse>>();
        return handler.ExecuteAsync(request, cancellationToken);
    }
}
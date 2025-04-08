namespace Intelsoft.Common.UseCases;

public interface IUseCaseMiddleware
{
    Task<TResponse> InvokeAsync<TRequest, TResponse>(
        TRequest request,
        Func<TRequest, object?, CancellationToken, Task<TResponse>> next,
        object? state = null,
        CancellationToken cancellationToken = default);
}
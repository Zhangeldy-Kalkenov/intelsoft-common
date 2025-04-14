namespace Nomad.Common.UseCases;

public interface IUseCaseDispatcher
{
    Task<TResponse> ExecuteAsync<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken = default);
}

using System.Reflection;
using BenchmarkDotNet.Attributes;
using Nomad.Common.UseCases;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Nomad.Common.Benchmarks;

public class CreateOrderRequest : IRequest<string>
{
}

public class CreateOrderResponse
{
}

public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, string>
{
    public Task<string> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        => Task.FromResult("Order created");
}

public class CustomUseCase : IUseCase<CreateOrderRequest, string>
{
    public Task<string> ExecuteAsync(CreateOrderRequest request, CancellationToken cancellationToken = default)
        => Task.FromResult("Order created");
}

public class NoOpMiddleware : IUseCaseMiddleware
{
    public Task<TResponse> InvokeAsync<TRequest, TResponse>(TRequest request, Func<TRequest, object?, CancellationToken, Task<TResponse>> next, object? state = null,
        CancellationToken cancellationToken = default)
    {
        return next(request, state, cancellationToken);
    }
}

[MemoryDiagnoser]
public class DispatcherBenchmarks
{
    private IMediator? _mediator;
    private IUseCaseDispatcher? _useCaseDispatcher;

    [GlobalSetup]
    public void Setup()
    {
        var services = new ServiceCollection();

        // MediatR setup
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Custom UseCaseDispatcher
        services.AddUseCases(Assembly.GetExecutingAssembly())
            .AddUseCaseMiddleware<NoOpMiddleware>();

        _useCaseDispatcher = services.BuildServiceProvider().GetRequiredService<IUseCaseDispatcher>() ??
                             throw new InvalidOperationException("IUseCaseDispatcher is null");

        _mediator = services.BuildServiceProvider().GetRequiredService<IMediator>() ??
                    throw new InvalidOperationException("IMediator is null");
    }

    private static readonly CreateOrderRequest _request = new();

    [Benchmark]
    public async Task<string> UseCaseDispatcher_UseCase()
    {
        return await _useCaseDispatcher!.ExecuteAsync<CreateOrderRequest, string>(_request).ConfigureAwait(false);
    }

    [Benchmark]
    public async Task<string> MediatR_Dispatch()
    {
        return await _mediator!.Send(_request).ConfigureAwait(false);
    }
}

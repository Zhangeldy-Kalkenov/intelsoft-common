# Intelsoft.Common.UseCases

🚀 A lightweight and high-performance Use Case dispatcher for Clean Architecture and DDD in .NET.

- ⚡ Faster than MediatR (~40% faster)
- 🧩 Middleware support (logging, validation, metrics, etc.)
- 🧼 No reflection, no magic — just clean DI
- ✅ Zero-allocation pipeline for hot paths
- 🧠 Built for monoliths, microservices, CQRS and pure CRUD apps

---

## 📦 Installation

```bash
dotnet add package Intelsoft.Common.UseCases
```

---

## 🧱 Use Case Flow

```text
Controller → UseCaseDispatcher → IUseCase<TRequest, TResponse> → Result
```

---

## 🚀 Quick Start

### 1. Define your request and response:

```csharp
public class CreateOrderRequest
{
    public Guid CustomerId { get; set; }
    public List<Guid> ProductIds { get; set; }
}

public class CreateOrderResponse
{
    public Guid OrderId { get; set; }
}
```

---

### 2. Create your Use Case:

```csharp
public class CreateOrderUseCase : IUseCase<CreateOrderRequest, CreateOrderResponse>
{
    public async Task<CreateOrderResponse> ExecuteAsync(CreateOrderRequest request, CancellationToken cancellationToken = default)
    {
        var newOrderId = Guid.NewGuid();
        return new CreateOrderResponse { OrderId = newOrderId };
    }
}
```

---

### 3. Register Use Cases and Middleware:

```csharp
builder.Services
    .AddUseCases(typeof(CreateOrderUseCase).Assembly)
    .AddUseCaseMiddleware<LoggingMiddleware>();
```

---

### 4. Dispatch inside a controller:

```csharp
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IUseCaseDispatcher _dispatcher;

    public OrdersController(IUseCaseDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        var result = await _dispatcher.ExecuteAsync<CreateOrderRequest, CreateOrderResponse>(request);
        return Ok(result);
    }
}
```

---

## 🧩 Middleware Example

```csharp
public class LoggingMiddleware : IUseCaseMiddleware
{
    public async Task<TResponse> InvokeAsync<TRequest, TResponse>(
        TRequest request,
        Func<TRequest, object?, CancellationToken, Task<TResponse>> next,
        object? state = null,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"[START] {typeof(TRequest).Name}");
        var result = await next(request, state, cancellationToken);
        Console.WriteLine($"[END] {typeof(TResponse).Name}");
        return result;
    }
}
```

---

## 🔧 Interfaces

### IUseCase

```csharp
public interface IUseCase<in TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken = default);
}
```

### IUseCaseMiddleware

```csharp
public interface IUseCaseMiddleware
{
    Task<TResponse> InvokeAsync<TRequest, TResponse>(
        TRequest request,
        Func<TRequest, object?, CancellationToken, Task<TResponse>> next,
        object? state = null,
        CancellationToken cancellationToken = default);
}
```

### IUseCaseDispatcher

```csharp
public interface IUseCaseDispatcher
{
    Task<TResponse> ExecuteAsync<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken = default);
}
```

---

## 📈 Benchmark Results

Tested with .NET 9.0 · Apple M1 Pro · Release mode · BenchmarkDotNet

| Method               | Mean     | Allocated |
|----------------------|----------|-----------|
| ✅ `UseCase Dispatch` | **52.75 ns** | **200 B** |
| MediatR Dispatch     | 84.64 ns | 336 B     |

---

## 🧪 Testing Use Cases

Use Cases are fully testable without controllers or infrastructure.

```csharp
[Fact]
public async Task Should_Create_Order()
{
    var useCase = new CreateOrderUseCase();
    var result = await useCase.ExecuteAsync(new CreateOrderRequest { ... });
    Assert.NotEqual(Guid.Empty, result.OrderId);
}
```

---

## 📦 Recommended Project Structure

```text
src/
├── Application/
│   └── UseCases/
│       └── CreateOrder/
│           └── CreateOrderUseCase.cs
├── Infrastructure/
│   └── DI/
│       └── ServiceCollectionExtensions.cs
├── WebApi/
│   └── Controllers/
│       └── OrdersController.cs
```

---

## ✅ Use Case Best Practices

- UseCases should contain only business logic
- Keep controller thin — just dispatch the use case
- Use middleware for cross-cutting concerns:
    - Logging
    - Validation
    - Metrics
    - Retry/Fallback
- Keep input/output clean (DTOs or primitives)

---

## 📄 License

MIT License · © 2025 Intelsoft

---

Contributions, feedback, and issues are welcome!

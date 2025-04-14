# Nomad.Common.UseCases

A high-performance, middleware-capable use case dispatcher for Clean Architecture and DDD applications in .NET.
Built for developers who want simplicity, speed, and structure — without magic or reflection.

---

## ✨ Highlights

- 🚀 ~40% faster than MediatR (based on benchmarks)
- 🧩 First-class middleware support (logging, validation, metrics, etc.)
- ✅ Clean DI, zero reflection, no scanning magic
- 🧼 Minimal allocations — optimized for hot paths
- 🔌 Seamlessly fits into monoliths, microservices, CQRS, or CRUD-style applications

---

## 📦 Installation

```bash
dotnet add package Nomad.Common.UseCases
```

---

## 📌 Execution Flow

```text
Controller → UseCaseDispatcher → IUseCase<TRequest, TResponse> → Result
```

---

## 🚀 Quick Start

### 1. Define a Request & Response:

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

### 2. Implement the Use Case:

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

### 3. Register Use Cases & Middleware:

```csharp
builder.Services
    .AddUseCases(typeof(CreateOrderUseCase).Assembly)
    .AddUseCaseMiddleware<LoggingMiddleware>();
```

---

### 4. Dispatch in Controller:

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

## 🧠 Interfaces

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

## 📈 Benchmark

Tested on .NET 9 · Apple M1 Pro · Release build · BenchmarkDotNet

| Method               | Mean     | Allocated |
|----------------------|----------|-----------|
| ✅ `UseCase Dispatch` | **52.75 ns** | **200 B** |
| MediatR Dispatch     | 84.64 ns | 336 B     |

---

## 🧪 Testing

Use cases can be tested in isolation — no controller or infrastructure needed:

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

## ✅ Best Practices

- Keep Use Cases small and focused on business logic
- Avoid service location — use constructor injection
- Let controllers just dispatch and return
- Use middleware for:
    - Logging
    - Validation
    - Retry / metrics
- Prefer DTOs over domain types at the boundary

---

## 📄 License

MIT License · © Nomad

All feedback and contributions are welcome.

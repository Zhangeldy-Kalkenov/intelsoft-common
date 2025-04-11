# Intelsoft.Common.MinimalApi

A clean and efficient framework for building Minimal API endpoints in ASP.NET Core using use cases and the `Result` pattern.
Designed to keep HTTP interfaces thin, modular, and focused on business logic — not plumbing.

---

## ✨ Features

- 🧩 Endpoint-first design with `IEndpointModule`
- 🚀 Auto-discovery of endpoint modules from the entry assembly
- ✅ Native support for `Result` / `Result<T>` return types
- 🔁 Smart HTTP status code mapping: 200, 201, 204, 400, 404, 409, etc.
- 🧼 Minimal allocations, no magic, no reflection
- 🔌 Seamlessly integrates with `IUseCaseDispatcher` (Clean Architecture)

---

## 📦 Installation

```bash
dotnet add package Intelsoft.Common.MinimalApi
```

---

## 🚀 Quick Example

### 1. Create a Use Case

```csharp
public class CreateProductUseCase : IUseCase<CreateProductRequest, Result<ProductResponse>>
{
    public Task<Result<ProductResponse>> ExecuteAsync(CreateProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = new ProductResponse { Id = Guid.NewGuid(), Name = request.Name };
        return Task.FromResult(Result.Success(product));
    }
}
```

---

### 2. Define Endpoint Module

```csharp
public class ProductEndpoints : IEndpointModule
{
    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapUseCasePost<CreateProductRequest, ProductResponse>(
            "/products", created => $"/products/{created.Id}");
    }
}
```

---

### 3. Register in Program.cs

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddUseCases(typeof(CreateProductUseCase).Assembly)
    .AddEndpointModulesFromEntryAssembly();

var app = builder.Build();

app.MapEndpointModulesFromEntryAssembly();

app.Run();
```

---

## 🧠 Smart HTTP Mapping

The library maps domain-level results to proper HTTP responses:

| Result                      | HTTP Response     |
|----------------------------|-------------------|
| `Result.Success()`         | `200 OK`          |
| `Result<T>.Success(...)`   | `200 OK` (with body) |
| `MapUseCasePost(..., routeFactory)` | `201 Created` (with Location header) |
| `Result.Failure(...)`      | `400 / 404 / 409` (based on error code) |
| `Result.NotFound()`        | `404 Not Found`   |

---

## ✅ When to Use

- You use `Minimal APIs` with Clean Architecture
- You want consistent and testable HTTP endpoints
- You want to avoid controller overload and infrastructure leakage
- You prefer business logic to live in `UseCases`, not endpoints

---

## 📄 License

MIT License · © Intelsoft

All feedback and contributions are welcome.

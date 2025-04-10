# Intelsoft.Common.UseCases.MinimalApi

A minimal, high-performance extension library for registering HTTP endpoints in ASP.NET Core using clean architecture
and the Result pattern. Designed with modularity, scalability, and separation of concerns in mind.

## ✨ Key Features

- Modular endpoint registration via `IEndpointModule`
- Automatic discovery and DI registration of endpoint modules
- Support for `Result` and `Result<T>` response types
- Consistent HTTP status code mapping (e.g., 200, 201, 204, 400, 404, 409)
- Zero reflection during request execution
- Clean and idiomatic API surface

## 📦 Installation

```bash
dotnet add package Intelsoft.Common.UseCases.MinimalApi
```

## 🚀 Quick Start

### 1. Define your endpoint module

```csharp
public class ProductEndpoints : IEndpointModule
{
    public void RegisterEndpoints(IEndpointRouteBuilder app)
    {
        app.MapUseCasePost<CreateProductRequest, ProductResponse>("/products", p => $"/products/{p.Id}");
        app.MapUseCaseGetById<GetProductByIdRequest, ProductResponse>("/products/{id}", id => request.Id);
        app.MapUseCasePut<UpdateProductRequest, Result>("/products/{id}", id => request.Id);
    }
}
```

### 2. Register modules and map them

```csharp
var builder = WebApplication.CreateBuilder(args);

// Automatically register all endpoint modules via DI
builder.Services.AddEndpointModulesFromEntryAssembly();

var app = builder.Build();

// Automatically resolve and register endpoints
app.MapEndpointModulesFromEntryAssembly();

app.Run();
```

## 🧩 Integration with `Result` Pattern

```csharp
public static IResult ToHttpResult(this Result result) => ...;
public static IResult ToHttpResult<T>(this Result<T> result) => ...;
```

All endpoints using `Result` or `Result<T>` will automatically return appropriate HTTP responses such as:

- `200 OK`
- `201 Created` (with Location header)
- `204 NoContent`
- `400 BadRequest`, `404 NotFound`, `409 Conflict`, `500 Internal Server Error`

## 💡 Designed For

- Minimal API projects following clean architecture
- Scalable enterprise-grade service applications
- Use case–driven development with dispatcher/handler separation

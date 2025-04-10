# Intelsoft.Common.Results

A lightweight, high-performance implementation of the Result pattern with strong typing, minimal allocations, and clean error handling.

## ✨ Features

- `Result` and `Result<T>` are `readonly record structs` (value types)
- No heap allocations, optimized for performance
- Clearly separates `Success` and `Failure` states
- Built-in support for rich `Error` objects with types like:
  - `Validation`
  - `NotFound`
  - `Conflict`
  - `Internal`
- `implicit` conversions for seamless use:
  ```csharp
  Result<int> result = 42;
  Result<string> failure = Error.NotFound("Item not found");
  ```

## 📦 Installation

```bash
dotnet add package Intelsoft.Common.Results
```

## 🚀 Usage

```csharp
// Success without a value
Result result = Result.Success();

// Success with a value
Result<string> result = "Done";

// Failure
Result failure = Error.Validation("Invalid input");
Result<Guid> notFound = Error.NotFound("Not found");

// Pattern check
if (result.Succeeded)
{
    Console.WriteLine("All good");
}
else
{
    Console.WriteLine(result.FailureReason?.Message);
}
```

## 🧪 Test Examples

```csharp
Result<string> result = GetUser();

if (result is { Succeeded: true, Value: var user })
{
    Console.WriteLine($"Hello {user}!");
}
else
{
    Console.WriteLine(result.FailureReason?.Message);
}
```

## 💡 When to Use

- Application services and domain logic
- Minimal API or controller responses
- Encapsulating and returning validation or business errors

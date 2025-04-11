# Intelsoft.Common.Results

A clean, lightweight, and high-performance implementation of the Result pattern for .NET.
Designed for domain and application layers where clarity, control, and correctness matter.

---

## ✨ Features

- `Result` and `Result<T>` as `readonly record struct` — zero heap allocations
- Clear distinction between success and failure states
- Rich error modeling via `Error` type: `Validation`, `NotFound`, `Conflict`, `Internal`, etc.
- Seamless `implicit` conversion support for primitives and errors
- Optimized for functional-style flow and clear return semantics

---

## 📦 Installation

```bash
dotnet add package Intelsoft.Common.Results
```

---

## 🚀 Quick Usage

```csharp
// Success
Result result = Result.Success();
Result<string> ok = "Done";

// Failure
Result fail = Error.Validation("Invalid data");
Result<Guid> notFound = Error.NotFound("Entity not found");

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

---

## 💡 Use Cases

- Application services and use cases
- Domain services with predictable output
- Clean validation and error propagation
- Replacing exceptions in workflows
- Consistent API responses in Minimal APIs

---

## 🧪 Testing Pattern

```csharp
Result<string> user = GetUser();

if (user is { Succeeded: true, Value: var name })
{
    Console.WriteLine($"Hello {name}!");
}
else
{
    Console.WriteLine(user.FailureReason?.Message);
}
```

---

## 🧱 Design Notes

- `Error` is a strongly typed record with built-in categories
- All results are immutable
- Works great with `MinimalApi`, `UseCaseDispatcher`, and CQRS

---

## 📄 License

MIT License · © Intelsoft

All feedback and contributions are welcome.

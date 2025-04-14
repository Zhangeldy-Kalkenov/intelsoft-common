# Nomad.Common.EntityFrameworkCore

Provides Entity Framework Core-based infrastructure support for Clean Architecture patterns, including:

- `IRepository<T, TId>` and `EfRepository<T, TId>`
- `SpecificationEvaluator` for applying specifications to EF queries

---

## ✨ Features

- Clean repository & unit of work abstractions
- Optimized LINQ specification evaluator
- Compatible with `Nomad.Common.SeedWork`
- Extension method for DI registration

---

## 📦 Installation

```bash
dotnet add package Nomad.Common.EntityFrameworkCore
```

---

## 🔌 Usage

In your service registration:

```csharp
services.AddEfRepositoriesFromAssemblies(
    typeof(AppDbContext).Assembly,
    typeof(MyCustomRepository).Assembly,
    Assembly.GetExecutingAssembly()
);
```

---

## 📄 License

MIT License · © Nomad

All feedback and contributions are welcome.

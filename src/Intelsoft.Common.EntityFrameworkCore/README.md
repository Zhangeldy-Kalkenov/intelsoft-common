# Intelsoft.Common.EntityFrameworkCore

Provides Entity Framework Core-based infrastructure support for Clean Architecture patterns, including:

- `IRepository<T, TId>` and `EfRepository<T, TId>`
- `SpecificationEvaluator` for applying specifications to EF queries

---

## ✨ Features

- Clean repository & unit of work abstractions
- Optimized LINQ specification evaluator
- Compatible with `Intelsoft.Common.SeedWork`
- Extension method for DI registration

---

## 📦 Installation

```bash
dotnet add package Intelsoft.Common.EntityFrameworkCore
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

MIT License · © Intelsoft

All feedback and contributions are welcome.

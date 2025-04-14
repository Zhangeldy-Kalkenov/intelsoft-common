# Nomad.Common.Outbox.EntityFrameworkCore

Provides an EF Core–based implementation of the Outbox Pattern.

---

## ✨ Features

- `EfOutboxWriter` and `EfOutboxPublisher`
- Hosted `OutboxBackgroundService` for async publishing
- `OutboxMessageConfiguration` with explicit schema
- Extension method `AddEfOutbox()` for DI registration
- Works with any `DbContext` via `DbSet<OutboxMessage>`

---

## 📦 Installation

```bash
dotnet add package Nomad.Common.Outbox.EntityFrameworkCore
```

---

## 🔌 Usage

In your application:

```csharp
services.AddEfOutbox();

modelBuilder.ApplyOutbox(); // inside OnModelCreating
```

---

## 📄 License

MIT License · © Nomad

All feedback and contributions are welcome.

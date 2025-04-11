# Intelsoft.Common.Outbox

Provides infrastructure-agnostic contracts and models for the Outbox Pattern, enabling reliable event publication.

---

## ✨ Features

- OutboxMessage model with full metadata
- Interfaces for outbox writing and publishing
- JSON serializer abstraction
- Fully decoupled from EF or any specific transport

---

## 📦 Installation

```bash
dotnet add package Intelsoft.Common.Outbox
```

---

## 🔌 Interfaces

- `IOutboxWriter`
- `IOutboxPublisher`
- `IOutboxSerializer`
- `IOutboxMessageStore`

Use with EF, Dapper, or any persistence engine.

---

## 📄 License

MIT License · © Intelsoft

All feedback and contributions are welcome.

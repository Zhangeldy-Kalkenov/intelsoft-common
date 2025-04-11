# Intelsoft.Common.IntegrationEvents.Contracts

Defines the core contract `IIntegrationEvent` used for all integration events.
Keeps event definitions cleanly separated from their transport and infrastructure concerns.

---

## 🔹 Interface

```csharp
public interface IIntegrationEvent
{
    public DateTime OccurredOn { get; init; }
    public string? CorrelationId { get; init; }
}
```

This abstraction can be shared across services to define common event types.

---

## 📦 Installation

```bash
dotnet add package Intelsoft.Common.IntegrationEvents.Contracts
```

---

## 📄 License

MIT License · © Intelsoft

All feedback and contributions are welcome.

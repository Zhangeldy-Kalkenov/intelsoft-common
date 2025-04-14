# Nomad.Common.IntegrationEvents.Contracts

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
dotnet add package Nomad.Common.IntegrationEvents.Contracts
```

---

## 📄 License

MIT License · © Nomad

All feedback and contributions are welcome.

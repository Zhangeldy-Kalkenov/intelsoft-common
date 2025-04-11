# Intelsoft.Common.IntegrationEvents

A foundational library for working with integration events in distributed systems.
Provides contracts and core building blocks for event-based communication, serialization, and dispatching.

---

## ✨ Features

- `IIntegrationEventPublisher` interface for abstracted publishing
- `IIntegrationEventHandler<T>` for processing incoming events
- `IntegrationEventEnvelope` for metadata and serialization support
- `IIntegrationSerializer` for customizable JSON serialization strategies
- Compatible with Kafka, RabbitMQ, Azure Service Bus, etc.
- Designed to integrate cleanly with DDD and Clean Architecture

---

## 📦 Installation

```bash
dotnet add package Intelsoft.Common.IntegrationEvents
```

---

## 🔌 Interfaces

```csharp
public interface IIntegrationEventPublisher
{
    Task PublishAsync<TEvent>(string topic, TEvent integrationEvent, CancellationToken cancellationToken = default)
        where TEvent : IIntegrationEvent;
}
```

```csharp
public interface IIntegrationEventHandler<in TEvent>
    where TEvent : IIntegrationEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}
```

---

## 📄 License

MIT License · © Intelsoft

All feedback and contributions are welcome.

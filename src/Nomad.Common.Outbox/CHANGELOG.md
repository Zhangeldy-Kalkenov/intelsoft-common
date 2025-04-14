# Changelog — Nomad.Common.Outbox

## [1.0.0] - 2025-04-12

### Added
- Core interfaces: `IOutboxWriter`, `IOutboxPublisher`, `IOutboxMessageStore`, `IOutboxSerializer`
- `OutboxMessage` model for representing persisted integration events
- JSON-based serializer: `JsonOutboxSerializer`
- Designed to be persistence-agnostic and transport-neutral

---

# Changelog — Nomad.Common.Outbox.EntityFrameworkCore

## [1.0.0] - 2025-04-12

### Added
- `EfOutboxWriter`: saves integration events to `DbSet<OutboxMessage>` within transaction
- `EfOutboxPublisher`: publishes unprocessed events and marks them as processed
- `EfOutboxMessageStore`: retrieves and updates outbox entries
- `OutboxBackgroundService`: background service to automate publishing
- `OutboxMessageConfiguration`: fluent schema configuration for EF Core
- `AddEfOutbox()` extension method for DI integration

---

# Intelsoft.Common.SeedWork

A clean and extensible seedwork foundation for building Domain-Driven Design (DDD) applications in .NET.

This package provides essential building blocks for defining expressive, rich domain models using aggregates, entities, value objects, and specifications. It is built for maintainability, testability, and separation of concerns between domain logic and infrastructure.

---

## ✨ Key Features

- Aggregate and entity base types with identity handling
- Lightweight value object support via interfaces and records
- Domain event model: event types, dispatchers, handlers
- Specification pattern with support for filtering, includes, sorting, and paging
- Generic repository and unit of work abstractions
- Strongly-typed ID support (`OrderId`, `CustomerId`, etc.)
- Designed to be infrastructure-agnostic and ORM-neutral

---

## 🧼 Design Philosophy

This seedwork follows modern DDD and clean architecture practices:

- Domain logic is central, independent from infrastructure
- Persistence and transport concerns stay outside the model
- Domain model expresses invariants and business rules clearly
- Specifications and repositories abstract away query logic and storage
- Events capture meaningful state changes across aggregates

---

## 🧪 Testing Support

- Specifications expose expressions and predicates for LINQ and in-memory testing
- Entities expose `DomainEvents` for behavior-driven assertions
- Strongly-typed IDs can be tested like primitives

---

## 🚀 Integration Ready

This seedwork is fully compatible with:

- EF Core (via ValueConverters and SpecificationEvaluator)
- CQRS and mediator pipelines
- In-memory or fake implementations for tests
- Custom ORM or document stores

---

## 📄 License

MIT License · © Intelsoft

All feedback and contributions are welcome.

# Changelog — Nomad.Common.MinimalApi

## [1.0.0] - 2025-04-10

### Added
- Support for modular endpoint registration using `IEndpointModule`
- Extension methods for mapping use case-based endpoints: `MapUseCasePost`, `MapUseCaseGetById`, `MapUseCasePut`, `MapUseCasePatch`, `MapUseCaseDelete`
- Automatic result-to-HTTP mapping with support for `Result` and `Result<T>`
- HTTP code translation for business errors: `Validation (400)`, `NotFound (404)`, `Conflict (409)`, `Internal (500)`
- DI-compatible endpoint module discovery using `AddEndpointModules` and `MapEndpointModules`
- Clean architecture–oriented design, supporting separation of concerns and scalability

---

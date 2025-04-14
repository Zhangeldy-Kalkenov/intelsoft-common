# Changelog — Nomad.Common.UseCases

## [1.0.0] – 2025-04-08

### Added

- `IUseCase<TRequest, TResponse>` interface for request/response-based business logic
- `IUseCaseDispatcher` for executing use cases through DI
- `IUseCaseMiddleware` pipeline support (logging, validation, etc.)
- `AddUseCases(Assembly)` for automatic DI registration
- Cancellation token support
- Middleware with static delegate and state to avoid allocations

---

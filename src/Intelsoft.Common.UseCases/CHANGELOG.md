# Changelog — Intelsoft.Common.UseCases

## [1.0.0] – 2025-04-08

### Added

- `IUseCase<TRequest, TResponse>` interface for request/response-based business logic
- `IUseCaseDispatcher` for executing use cases through DI
- `IUseCaseMiddleware` pipeline support (logging, validation, etc.)
- `AddUseCases(Assembly)` for automatic DI registration
- Cancellation token support
- Middleware with static delegate and state to avoid allocations

### Performance

- ~52 ns per use case dispatch
- ~40% faster than MediatR
- 200B allocation per call (vs 336B in MediatR)
- Zero allocations in the middleware

### Testability

- Use cases are testable directly without infrastructure
- Middleware can be injected manually in tests

---

## License

MIT License
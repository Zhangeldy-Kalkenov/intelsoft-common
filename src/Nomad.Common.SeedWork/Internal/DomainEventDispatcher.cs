using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nomad.Common.SeedWork.Internal;

/// <summary>
///     Default implementation of <see cref="IDomainEventDispatcher"/>.
///     Resolves and invokes all registered domain event handlers for each event.
/// </summary>
/// <param name="serviceProvider">The service provider used to resolve handlers.</param>
internal sealed class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    /// <inheritdoc />
    public async ValueTask DispatchAsync(IEnumerable<IDomainEvent> domainEvents,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(domainEvents);

        foreach (var domainEvent in domainEvents)
        {
            var eventType = domainEvent.GetType();
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
            var handlers = serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                if (handler is null) return;
                var handleMethod = handlerType.GetMethod(
                    nameof(IDomainEventHandler<IDomainEvent>.HandleAsync),
                    BindingFlags.Instance | BindingFlags.Public
                );

                if (handleMethod is null)
                    continue;

                var result = handleMethod.Invoke(handler, [domainEvent, cancellationToken]);

                switch (result)
                {
                    case ValueTask valueTask:
                        await valueTask.ConfigureAwait(false);
                        break;

                    case Task task:
                        await task.ConfigureAwait(false);
                        break;

                    default:
                        throw new InvalidOperationException(
                            $"Handler {handler.GetType().Name} returned unsupported result.");
                }
            }
        }
    }
}

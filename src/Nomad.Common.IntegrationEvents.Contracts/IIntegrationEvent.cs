namespace Nomad.Common.IntegrationEvents.Contracts;

/// <summary>
///     Represents a contract for an integration event published between services.
/// </summary>
public interface IIntegrationEvent
{
    public DateTime OccurredOn { get; init; }
    public string? CorrelationId { get; init; }
}

namespace Nomad.Common.IntegrationEvents;

/// <summary>
///     Wraps integration event metadata for transport purposes.
/// </summary>
public sealed class IntegrationEventEnvelope
{
    public required string Type { get; init; }
    public required string Data { get; init; }
    public DateTime OccurredOn { get; init; }
    public string? CorrelationId { get; init; }
}

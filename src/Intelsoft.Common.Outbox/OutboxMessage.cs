namespace Intelsoft.Common.Outbox;

/// <summary>
///     Represents a persisted integration event intended for later delivery via an event bus.
/// </summary>
public sealed class OutboxMessage
{
    public Guid Id { get; set; }

    /// <summary>
    /// Fully qualified type name of the integration event.
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    ///     Serialized event payload.
    /// </summary>
    public required string Payload { get; set; }

    /// <summary>
    ///     UTC timestamp when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; set; }

    /// <summary>
    ///     Optional correlation ID for tracing across systems.
    /// </summary>
    public string? CorrelationId { get; set; }

    /// <summary>
    ///     Optional name of the topic or queue to which the event should be sent.
    /// </summary>
    public required string Topic { get; set; }

    /// <summary>
    ///     When the event was successfully published to the bus.
    /// </summary>
    public DateTime? ProcessedAt { get; set; }
}

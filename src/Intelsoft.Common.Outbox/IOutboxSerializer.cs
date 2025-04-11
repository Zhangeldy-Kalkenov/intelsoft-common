using Intelsoft.Common.IntegrationEvents.Contracts;

namespace Intelsoft.Common.Outbox;

/// <summary>
///     Serializes and deserializes integration events for outbox storage.
/// </summary>
public interface IOutboxSerializer
{
    /// <summary>
    ///     Serializes an integration event into a string payload.
    /// </summary>
    string Serialize(IIntegrationEvent integrationEvent);

    /// <summary>
    /// Deserializes a payload into an integration event of the specified type.
    /// </summary>
    IIntegrationEvent Deserialize(string payload, string type);
}

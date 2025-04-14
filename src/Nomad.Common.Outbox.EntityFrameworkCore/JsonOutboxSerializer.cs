using System.Text.Json;
using Nomad.Common.Outbox;
using Nomad.Common.IntegrationEvents.Contracts;

namespace Nomad.Common.Outbox.EntityFrameworkCore;

public sealed class JsonOutboxSerializer : IOutboxSerializer
{
    private static readonly JsonSerializerOptions _options = new(JsonSerializerDefaults.Web);

    public string Serialize(IIntegrationEvent integrationEvent)
        => JsonSerializer.Serialize(integrationEvent, integrationEvent.GetType(), _options);

    public IIntegrationEvent Deserialize(string payload, string type)
    {
        var clrType = Type.GetType(type)
                      ?? throw new InvalidOperationException($"Cannot resolve type: {type}");

        return (IIntegrationEvent)(JsonSerializer.Deserialize(payload, clrType, _options)
                                   ?? throw new InvalidOperationException(
                                       $"Failed to deserialize event of type {type}"));
    }
}

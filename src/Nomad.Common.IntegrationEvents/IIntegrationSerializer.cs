using Nomad.Common.IntegrationEvents.Contracts;

namespace Nomad.Common.IntegrationEvents;

public interface IIntegrationEventSerializer
{
    string Serialize<T>(T integrationEvent) where T : IIntegrationEvent;
    IIntegrationEvent Deserialize(string json, string typeName);
}

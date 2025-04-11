using Intelsoft.Common.IntegrationEvents.Contracts;

namespace Intelsoft.Common.IntegrationEvents;

public interface IIntegrationEventSerializer
{
    string Serialize<T>(T integrationEvent) where T : IIntegrationEvent;
    IIntegrationEvent Deserialize(string json, string typeName);
}

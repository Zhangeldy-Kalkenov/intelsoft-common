using Microsoft.AspNetCore.Routing;

namespace Nomad.Common.MinimalApi;

public interface IEndpointModule
{
    void RegisterEndpoints(IEndpointRouteBuilder app);
}

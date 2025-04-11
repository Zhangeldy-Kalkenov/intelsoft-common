using Microsoft.AspNetCore.Routing;

namespace Intelsoft.Common.MinimalApi;

public interface IEndpointModule
{
    void RegisterEndpoints(IEndpointRouteBuilder app);
}

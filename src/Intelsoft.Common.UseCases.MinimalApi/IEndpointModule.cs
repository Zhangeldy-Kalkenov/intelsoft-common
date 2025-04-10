using Microsoft.AspNetCore.Routing;

namespace Intelsoft.Common.UseCases.MinimalApi;

public interface IEndpointModule
{
    void RegisterEndpoints(IEndpointRouteBuilder app);
}

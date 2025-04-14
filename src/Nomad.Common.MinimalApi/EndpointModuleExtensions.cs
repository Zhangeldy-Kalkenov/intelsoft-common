using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Nomad.Common.MinimalApi;

public static class EndpointModuleExtensions
{
    /// <summary>
    ///     Registers all IEndpointModule implementations from the specified assemblies using DI.
    /// </summary>
    public static WebApplication MapEndpointModules(this WebApplication app, params Assembly[] assemblies)
    {
        var modules = app.Services.GetServices<IEndpointModule>();

        foreach (var module in modules)
        {
            module.RegisterEndpoints(app);
        }

        return app;
    }

    /// <summary>
    ///     Registers all IEndpointModule implementations from the entry assembly using DI.
    /// </summary>
    public static WebApplication MapEndpointModulesFromEntryAssembly(this WebApplication app)
    {
        var assembly = Assembly.GetEntryAssembly();
        return assembly is not null ? app.MapEndpointModules(assembly) : app;
    }

    /// <summary>
    ///     Registers all IEndpointModule implementations from the calling assembly using DI.
    /// </summary>
    public static WebApplication MapEndpointModulesFromCurrentAssembly(this WebApplication app)
    {
        var assembly = Assembly.GetCallingAssembly();
        return app.MapEndpointModules(assembly);
    }
}

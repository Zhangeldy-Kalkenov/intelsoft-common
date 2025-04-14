// using System.Net;
// using System.Net.Http.Json;
// using Nomad.Common.Results;
// using Nomad.Common.UseCases;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Microsoft.AspNetCore.Routing;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.VisualStudio.TestPlatform.TestHost;
//
// namespace Nomad.Common.MinimalApi.Tests;
//
// public class MinimalApiIntegrationTests
// {
//     private readonly WebApplicationFactory<Program> _factory;
//
//     public MinimalApiIntegrationTests()
//     {
//         _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
//         {
//             builder.ConfigureServices(services =>
//             {
//                 services.AddUseCases(typeof(TestUseCase).Assembly);
//                 services.AddEndpointModules(typeof(FakeEndpointModule).Assembly);
//             });
//
//             builder.Configure(app =>
//             {
//                 app.UseRouting();
//
//                 app.UseEndpoints(endpoints =>
//                 {
//                     var modules = app.ApplicationServices.GetServices<IEndpointModule>();
//                     foreach (var module in modules)
//                     {
//                         module.RegisterEndpoints(endpoints);
//                     }
//                 });
//             });
//         });
//     }
//
//     [Fact]
//     public async Task Post_ReturnsSuccessResult()
//     {
//         var client = _factory.CreateClient();
//
//         var request = new TestRequest { Value = "test" };
//         var response = await client.PostAsJsonAsync("/test", request);
//
//         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//
//         var result = await response.Content.ReadFromJsonAsync<string>();
//         Assert.Equal("test", result);
//     }
//
//     [Fact]
//     public async Task Post_ReturnsValidationError()
//     {
//         var client = _factory.CreateClient();
//
//         var request = new TestRequest { Value = "fail" };
//         var response = await client.PostAsJsonAsync("/test", request);
//
//         Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
//     }
//
//     // Test dependencies
//     public record TestRequest
//     {
//         public string Value { get; init; } = null!;
//     }
//
//     public record TestResponse
//     {
//     }
//
//     public class FakeEndpointModule : IEndpointModule
//     {
//         public void RegisterEndpoints(IEndpointRouteBuilder app)
//         {
//             app.MapUseCasePost<TestRequest, Result<TestResponse>>("/test");
//         }
//     }
//
//     public class TestUseCase : IUseCase<TestRequest, Result<TestResponse>>
//     {
//         public Task<Result<TestResponse>> ExecuteAsync(TestRequest request,
//             CancellationToken cancellationToken = default)
//         {
//             if (request is not { Value: "fail" })
//             {
//                 return Task.FromResult(Result<TestResponse>.Success(new TestResponse()));
//             }
//
//             var result = Result<TestResponse>.Failure(Error.Validation("Invalid value"));
//             return Task.FromResult(result);
//         }
//     }
// }

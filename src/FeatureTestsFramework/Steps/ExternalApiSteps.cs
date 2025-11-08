using FeatureTestsFramework.Extensions;
using Reqnroll;
using WireMock.FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using HttpMethod = Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpMethod;

namespace FeatureTestsFramework.Steps;

[Binding]
public class ExternalApiSteps
{
    private readonly ScenarioContext context;
    private readonly WireMockServer _wireMockServer;

    public ExternalApiSteps(ScenarioContext scenarioContext)
    {
        this.context = scenarioContext;
        _wireMockServer = context.GetService<WireMockServer>();
    }
    
    [Given(@"service {string} api for {string} {string} returns")]
    public void ThenExternalApiForGetpostputdeletepatchheadoptionsStringReturns(string service, string method,
        string url,
        string content)
    {
        var trimmedUrl = url.TrimEnd('/').TrimStart('/');
        var fullPath = $"/{service}/{trimmedUrl}";
        
        var request = Request.Create()
            .WithPath(fullPath)
            .UsingMethod(method);

        var response = Response.Create()
            .WithStatusCode(200)
            .WithHeader("Content-Type", "application/json")
            .WithBody(content);

        _wireMockServer.Given(request).RespondWith(response);
    }

    [Then("service {string} was called with {string} {string}")]
    public void ThenServiceShouldBeCalled(string service, string method, string url)
    {
        var trimmedUrl = url.TrimEnd('/').TrimStart('/');
        var fullPath = $"/{service}/{trimmedUrl}";
        
        _wireMockServer.Should()
            .HaveReceivedACall()
            .UsingMethod(method)
            .And
            .AtAbsolutePath(fullPath);
    }
}
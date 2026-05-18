using FeatureTestsFramework.Assertions;
using FeatureTestsFramework.Extensions;
using FluentAssertions;
using Reqnroll;
using WireMock.FluentAssertions;
using WireMock.Matchers;
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
    [Obsolete]
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

    [Given(@"service {string} returns {int} for {string} {string} with body")]
    public void GivenServiceStringReturnsIntForStringStringWithBody(string service, int statusCode, string method,
        string url,
        string content)
    {
        var trimmedUrl = url.TrimEnd('/').TrimStart('/');
        var fullPath = $"/{service}/{trimmedUrl}";

        var request = Request.Create()
            .WithPath(fullPath)
            .UsingMethod(method);

        var response = Response.Create()
            .WithStatusCode(statusCode)
            .WithHeader("Content-Type", "application/json")
            .WithBody(content);

        _wireMockServer.Given(request).RespondWith(response);
    }

    [Given(@"service {string} returns {int} for {string} {string}")]
    public void GivenServiceStringReturnsIntForStringString(string service, int statusCode, string method,
        string url)
    {
        var trimmedUrl = url.TrimEnd('/').TrimStart('/');
        var fullPath = $"/{service}/{trimmedUrl}";

        var request = Request.Create()
            .WithPath(fullPath)
            .UsingMethod(method);

        var response = Response.Create()
            .WithStatusCode(statusCode)
            .WithHeader("Content-Type", "application/json");

        _wireMockServer.Given(request).RespondWith(response);
    }

    [Then("service {string} was called with {string} {string}")]
    [Then("service {string} should be called with {string} {string}")]
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

    [Then("service {string} was called with {string} {string} and body")]
    [Then("service {string} should be called with {string} {string} and body")]
    public void ThenServiceShouldBeCalled(string service, string method, string url, string body)
    {
        var trimmedUrl = url.TrimEnd('/').TrimStart('/');
        var fullPath = $"/{service}/{trimmedUrl}";

        body.IsValidJson().Should().BeTrue("Body should be valid JSON");

        try
        {
            _wireMockServer.Should()
                .HaveReceivedACall()
                .UsingMethod(method)
                .And
                .AtAbsolutePath(fullPath)
                .And
                .WithBody(new PartialJsonMatcher(body));
        }
        catch (Exception e)
        {
            throw;
        }
    }

    [Then("service {string} should be called")]
    public void ThenServiceShouldBeCalled(string service)
    {
        _wireMockServer.Should()
            .HaveReceivedACall()
            .AtAbsolutePath(new WildcardMatcher("*service*"));
    }
}
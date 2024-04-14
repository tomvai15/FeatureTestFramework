using FeatureTestsFramework.Extensions;
using FluentAssertions;
using TechTalk.SpecFlow;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace FeatureTestsFramework.SOA.Tests.Steps
{
    [Binding]
    public class ExternalApiSteps
    {
        private const string ExternalApiUrl = "/postman/get";
        private readonly ScenarioContext _context;
        private readonly WireMockServer _wireMockServer;

        private readonly IRequestBuilder _externalApiEndpoint;

        public ExternalApiSteps(ScenarioContext context)
        {
            _context = context;
            _wireMockServer = _context.GetRequiredService<WireMockServer>();

            _externalApiEndpoint = Request.Create()
                .WithPath(ExternalApiUrl)
                .UsingGet();
        }

        [Given(@"external api returns response body")]
        public void ThenExternalApiShouldBeCalled(string response)
        {
            SetEndpointResponse(response);

            //while (true)
            //{
            //    Thread.Sleep(1000);
            //}

            return;
        }

        [Then(@"external api should be called")]
        public void ThenExternalApiShouldBeCalled()
        {
            var calls = _wireMockServer.FindLogEntries(_externalApiEndpoint).ToList();
            calls.Count.Should().NotBe(0);
        }

        private void SetEndpointResponse(string response)
        {
            _wireMockServer
                .Given(_externalApiEndpoint)
                .RespondWith(
                    Response.Create()
                    .WithStatusCode(200)
                    .WithBody(response)
                    );
        }
    }
}

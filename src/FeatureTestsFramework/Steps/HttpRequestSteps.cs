using FeatureTestsFramework.Client;
using FeatureTestsFramework.Placeholders.Replacers;
using System.Net;
using TechTalk.SpecFlow;
using FeatureTestsFramework.Extensions;
using FeatureTestsFramework.HttpRequest;
using FeatureTestsFramework.Placeholders.Response;
using FeatureTestsFramework.Placeholders.Request;
using FeatureTestsFramework.Placeholders.Uri;
using FeatureTestsFramework.Assertions;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Primitives;

namespace FeatureTestsFramework.Steps
{
    [Binding]
    public class HttpRequestSteps : TechTalk.SpecFlow.Steps
    {
        public const string ResponseKey = "Response";
        public const string RequestKey = "Request";

        private FeatureTestResponse response;

        private readonly ScenarioContext context;
        private readonly IFeatureTestClient featureTestClient;
        private readonly IFeatureTestRequestBuilder requestBuilder;
        private readonly IRequestStore requestsStore;
        private readonly IPlaceholderReplacer<IResponsePlaceholderEvaluator> responsePlaceholderReplacer;
        private readonly IPlaceholderReplacer<IRequestPlaceholderEvaluator> requestPlaceholderReplacer;
        private readonly IPlaceholderReplacer<IUriPlaceholderEvaluator> uriPlaceholderReplacer;

        public HttpRequestSteps(ScenarioContext scenarioContext)
        {
            this.context = scenarioContext;
            featureTestClient = context.GetService<IFeatureTestClient>();
            requestBuilder = context.GetRequiredService<IFeatureTestRequestBuilder>();
            requestsStore = context.GetRequiredService<IRequestStore>();
            responsePlaceholderReplacer = context.GetRequiredService<IPlaceholderReplacer<IResponsePlaceholderEvaluator>>();
            requestPlaceholderReplacer = context.GetRequiredService<IPlaceholderReplacer<IRequestPlaceholderEvaluator>>();
            uriPlaceholderReplacer = context.GetRequiredService<IPlaceholderReplacer<IUriPlaceholderEvaluator>>();
        }

        [Given(@"I have an HTTP ""([^""]*)"" ""([^""]*)"" request with body")]
        public void GivenIHaveAnHttpRequestWithBody(string httpMethod, string url, string requestBody)
        {
            GivenIHaveAnHttpRequest(httpMethod, url);
            var replacedBody = requestPlaceholderReplacer.Replace(requestBody, context);
            requestBuilder.SetBody(replacedBody);
        }

        [When(@"I send the request")]
        [Given(@"I have sent the request")]
        public async Task SendARequest()
        {
            var request = requestBuilder.Build();
            response = await featureTestClient.SendRequest(request);
            context[RequestKey] = request;
            context[ResponseKey] = response;
            requestsStore.AddRequest(request);
        }

        [Then(@"the response body should match '([^']*)'")]
        [Then(@"the response body should match")]
        public void ThenTheResponseBodyShouldMatch(string expectedResponseBody)
        {
            var expectedRegexedResponseBody = expectedResponseBody;
            var responseIsJson = expectedResponseBody.TrimStart().StartsWith('{');
            if (responseIsJson)
            {
                var formatedJson = expectedResponseBody.FormatJsonWithPlaceholders();
                expectedRegexedResponseBody = responsePlaceholderReplacer.Replace(formatedJson, context);
                response.Should().NotBeNull();
            }
            response.ResponseBody.ShouldMatchRegexLineByLine(expectedRegexedResponseBody);
        }


        [Then(@"the response body should match exact formatting '([^']*)'")]
        [Then(@"the response body should match exact formatting")]
        public void ThenTheResponseBodyShouldMatchExactFormatting(string expectedResponseBody)
        {
            response.Should().NotBeNull();
            var expectedRegexedResponseBody = responsePlaceholderReplacer.Replace(expectedResponseBody, context);
            response.ResponseBody.ShouldMatchRegexLineByLine(expectedRegexedResponseBody);
        }

        [Given(@"I have an HTTP ""([^""]*)"" ""([^""]*)"" request")]
        private void GivenIHaveAnHttpRequest(string httpMethod, string url)
        {
            requestBuilder.SetMethod(new HttpMethod(httpMethod));
            var uri = new Uri(uriPlaceholderReplacer.Replace(url, context), UriKind.Relative);
            requestBuilder.SetRelativeUrl(uri);
        }

        [Then(@"the response status code should be (.*)")]
        public async Task ThenTheResponseStatusCodeShouldBe(int statusCode)
        {
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be((HttpStatusCode)statusCode, response.ResponseBody);
        }

        [Given(@"I set API version to ""([^""]*)""")]
        public void GivenISetAPIVersionTo(string version)
        {
            requestBuilder.SetUriApiVersion(version);
        }

        [Given(@"I set sub Uri to ""([^""]*)""")]
        public void GivenISetSubUriTo(string suburi)
        {
            requestBuilder.SetSubUri(uriPlaceholderReplacer.Replace(suburi, context));
        }

        [Then(@"the response headers should contain name")]
        public void ThenTheResponseHeadersReturnedShouldContainName(Table table)
        {
            AssertHeaders(table, (responseHeaders, expectedHeader) => responseHeaders.Should().Contain(expectedHeader));
        }

        [Then(@"the response headers should not contain")]
        public void WhenTheResponseHeadersReturnedShouldNotContain(Table table)
        {
            AssertHeaders(table, (responseHeaders, expectedHeader) => responseHeaders.Should().NotContain(expectedHeader));
        }

        [Then(@"the response header ""([^""]*)"" should be ""([^""]*)""")]
        public void ThenTheHeaderShouldBe(string name, string value)
        {
            response.Should().NotBeNull();
            response.Headers.Should().Contain(h => h.Key == name && h.Value.Contains(value));
        }

        [Then(@"response headers should contain name and value")]
        public void ThenHeadersShouldContainNameAndValue(Table table)
        {
            var expectedHeaders = table.Rows.Select(r => new Header(r["Header"], r["Value"])).ToList();
            var actualHeaders = response.Headers.Select(r => new Header(r.Key, string.Join("; ", r.Value)));
            using (new AssertionScope())
            {
                foreach (var expectedHeades in expectedHeaders)
                {
                    actualHeaders.Should().Contain(expectedHeades);
                }
            }
        }

        [Given(@"I add a request header ""([^""])""")]
        public void GivenIAddARequestHeader(string header)
        {
            requestBuilder.AddHeader(header, new StringValues("Can-be-anything"));
        }

        [Given(@"I add a request header ""([^""]*)"" with value ""([^""]*)""")]
        public void WhenIAddHeaderWithValue(string name, string value)
        {
            requestBuilder.AddHeader(name, value);
        }

        [Given(@"I add a query parameter "" ([^""]*)"" with value ""([^""]*) """)]
        public void GivenIAddQueryParameter(string parameterName, string value)
        {
            requestBuilder.AddQueryParameter(parameterName, value);
        }

        [Given(@"the header removed is (.*)")]
        [Given(@"I remove header ""([^""]*)""")]
        public void GivenTheHeaderRemovedIs(string header)
        {
            requestBuilder.RemoveHeader(header);
        }

        private static IEnumerable<string> GetValuesOfColumn(Table table, string column)
        {
            return table.Rows.Select(r => r[column]);
        }
        private void AssertHeaders(Table table, Action<IEnumerable<string>, string> assertion)
        {
            var expectedHeaders = GetValuesOfColumn(table, "Name"); using (new AssertionScope())
            {
                var responseHeaders = response.Headers.Select(h => h.Key);
                foreach (var expectedHeader in expectedHeaders)
                {
                    assertion(responseHeaders, expectedHeader);
                }
            }
        }   

        struct Header
        {
            public readonly string Name;
            public readonly string Value;
            public Header(string name, string value)
            {
                Name = name;
                Value = value;
            }
        }
    }
}

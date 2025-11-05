using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace FeatureTestsFramework.HttpRequest;

public interface IFeatureTestClient
{
    Task<FeatureTestResponse> SendRequest(FeatureTestRequest featureTestRequest);
}

public class FeatureTestClient : IFeatureTestClient
{
    private readonly HttpClient _httpClient;

    public FeatureTestClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<FeatureTestResponse> SendRequest(FeatureTestRequest featureTestRequest)
    {
        using var requestMessage = featureTestRequest.ToHttpRequestMessage();
        var response = await _httpClient.SendAsync(requestMessage);
        return await ConvertToFeatureTestResponse(featureTestRequest, response);
    }

    private async Task<FeatureTestResponse> ConvertToFeatureTestResponse(FeatureTestRequest featureTestRequest, HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();

        var containsContentType = response.Headers.TryGetValues("Content-Type", out var values);
        var isJson = containsContentType ? values.Any(value => value.Contains("json")) : false;
        var responseBody = isJson ? Format(responseContent) : responseContent;

        return new FeatureTestResponse(featureTestRequest, response, responseBody);

    }

    private static string Format(string json) => JsonConvert.SerializeObject(JToken.Parse(json), Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "O" });
}
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace FeatureTestsFramework.Client
{
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
            var responseStream = await response.Content.ReadAsStreamAsync();
            string responseBody;
            try
            {
                using var responseReader = new StreamReader(responseStream);
                var responseContent = await responseReader.ReadToEndAsync();
                var isJson = response.Headers.GetValues("Content-Type").Any(value => value.Contains("json"));
                responseBody = isJson ? Format(responseContent) : responseContent;

                return new FeatureTestResponse(featureTestRequest, response, responseBody);
            }
            finally
            {
                await responseStream.DisposeAsync();
            }
        }

        private static string Format(string json) => JsonConvert.SerializeObject(JToken.Parse(json), Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "O" });
    }
}

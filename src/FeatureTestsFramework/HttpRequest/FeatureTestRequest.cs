using Microsoft.Extensions.Primitives;
using System.Text;

namespace FeatureTestsFramework.HttpRequest
{
    public class FeatureTestRequest
    {
        public Dictionary<string, StringValues> AdditionalHeaders { get; set; } = new Dictionary<string, StringValues>();
        public string ApiVersion { get; set; }
        public string Body { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public Uri EndpointRelativeUri { get; set; }
        public string SubUri { get; set; }
        public DateTime RequestTimestampUtc { get; set; }

        public HttpRequestMessage ToHttpRequestMessage()
        {
            var request = new HttpRequestMessage();
            request.Method = HttpMethod;
            request.RequestUri = EndpointRelativeUri;

            foreach (var header in AdditionalHeaders)
            {
                request.Headers.Add(header.Key, header.Value.ToString().Split(";", StringSplitOptions.RemoveEmptyEntries));
            }

            if (Body != null)
            {
                request.Content = new StringContent(Body, Encoding.UTF8, "application/hal+json");
            }

            if (!string.IsNullOrEmpty(SubUri))
            {
                const char slash = '/';
                var uri = EndpointRelativeUri.ToString().TrimStart(slash);
                var subUri = SubUri.TrimStart(slash).TrimEnd(slash);
                request.RequestUri = new Uri($"{slash}{subUri}{slash}{uri}", UriKind.Relative);
            }

            return request;
        }
    }
}

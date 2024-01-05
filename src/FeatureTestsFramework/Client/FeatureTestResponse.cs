using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FeatureTestsFramework.Client
{
    public class FeatureTestResponse
    {
        public FeatureTestRequest FeatureTestRequest { get; }
        public DateTime ResponseTimestampUtc { get; }
        public List<KeyValuePair<string, IEnumerable<string>>> Headers { get; }
        public HttpStatusCode HttpStatusCode { get; }
        public string ReasonPhrase { get; }
        public Uri? RequestUri { get; }
        public string ResponseBody { get; }

        public FeatureTestResponse(FeatureTestRequest featureTestRequest, HttpResponseMessage response, string responseBody)
        {
            FeatureTestRequest = featureTestRequest ?? throw new ArgumentNullException(nameof(featureTestRequest));
            ResponseTimestampUtc = DateTime.UtcNow;

            HttpStatusCode = response.StatusCode;
            ReasonPhrase = response.ReasonPhrase;
            RequestUri = response.RequestMessage?.RequestUri;
            Headers = response.Content.Headers
                .Concat(response.Headers)
                .Concat(response.TrailingHeaders)
                .ToList();

            ResponseBody = responseBody;
        }
    }
}

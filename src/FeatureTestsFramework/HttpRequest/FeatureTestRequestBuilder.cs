using FeatureTestsFramework.Client;
using Microsoft.Extensions.Primitives;

namespace FeatureTestsFramework.HttpRequest
{
    public interface IFeatureTestRequestBuilder
    {
        public IFeatureTestRequestBuilder AddHeader(string header, StringValues value);

        public IFeatureTestRequestBuilder RemoveHeader(string header);

        public IFeatureTestRequestBuilder SetMethod(HttpMethod httpMethod);

        public IFeatureTestRequestBuilder SetRelativeUrl(Uri uri);

        public IFeatureTestRequestBuilder SetUriApiVersion(string version);

        public IFeatureTestRequestBuilder AddQueryParameter(string parameterName, string value);

        public IFeatureTestRequestBuilder SetSubUri(string suburi);
        public IFeatureTestRequestBuilder SetBody(string body);

        public FeatureTestRequest Build();
    }

    public class FeatureTestRequestBuilder : IFeatureTestRequestBuilder
    {
        private FeatureTestRequest featureTestRequest = new();
        private List<QueryParameter> queryParameters = new();

        public IFeatureTestRequestBuilder AddHeader(string header, StringValues value)
        {
            if (featureTestRequest.AdditionalHeaders.ContainsKey(header))
            {
                return this;
            }

            featureTestRequest.AdditionalHeaders.Add(header, value);
            return this;
        }

        public IFeatureTestRequestBuilder SetBody(string body)
        {
            featureTestRequest.Body = body;
            return this;
        }

        public IFeatureTestRequestBuilder RemoveHeader(string header)
        {
            featureTestRequest.AdditionalHeaders.Remove(header);
            return this;
        }

        public IFeatureTestRequestBuilder SetMethod(HttpMethod httpMethod)
        {
            featureTestRequest.HttpMethod = httpMethod;
            return this;
        }

        public IFeatureTestRequestBuilder SetRelativeUrl(Uri uri)
        {
            featureTestRequest.EndpointRelativeUri = uri;
            return this;
        }

        public IFeatureTestRequestBuilder SetUriApiVersion(string version)
        {
            featureTestRequest.ApiVersion = version;
            return this;
        }

        public IFeatureTestRequestBuilder AddQueryParameter(string parameterName, string value)
        {
            queryParameters.Add(new QueryParameter { Name = parameterName, Value = value });
            return this;
        }

        public IFeatureTestRequestBuilder SetSubUri(string suburi)
        {
            featureTestRequest.SubUri = suburi;
            return this;
        }

        public FeatureTestRequest Build()
        {
            var path = featureTestRequest.EndpointRelativeUri.ToString();

            // TODO: consider using UriBuilder.
            char seperator = '?';
            foreach (var query in queryParameters)
            {
                path += $"{seperator}{query.Name}={query.Value}";
                seperator = '&';
            }

            featureTestRequest.EndpointRelativeUri = new Uri(path, UriKind.Relative);
            return featureTestRequest;
        }
    }
}

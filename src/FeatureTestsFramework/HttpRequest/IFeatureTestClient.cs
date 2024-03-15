namespace FeatureTestsFramework.HttpRequest
{
    public interface IFeatureTestClient
    {
        Task<FeatureTestResponse> SendRequest(FeatureTestRequest featureTestRequest);
    }
}

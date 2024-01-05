namespace FeatureTestsFramework.Client
{
    public interface IFeatureTestClient
    {
        Task<FeatureTestResponse> SendRequest(FeatureTestRequest featureTestRequest);
    }
}

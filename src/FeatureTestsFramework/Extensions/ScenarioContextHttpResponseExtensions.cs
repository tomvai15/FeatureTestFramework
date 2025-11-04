using FeatureTestsFramework.HttpRequest;
using FeatureTestsFramework.Steps;
using Reqnroll;

namespace FeatureTestsFramework.Extensions
{
    public static class ScenarioContextHttpResponseExtensions
    {
        public static FeatureTestResponse GetHttpResponse(this ScenarioContext context)
            => context.Get<FeatureTestResponse>(HttpRequestSteps.ResponseKey);
    }
}

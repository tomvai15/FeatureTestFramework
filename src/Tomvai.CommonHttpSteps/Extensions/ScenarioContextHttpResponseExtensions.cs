using Reqnroll;
using Tomvai.CommonHttpSteps.HttpRequest;
using Tomvai.CommonHttpSteps.Steps;

namespace Tomvai.CommonHttpSteps.Extensions;

public static class ScenarioContextHttpResponseExtensions
{
    public static FeatureTestResponse GetHttpResponse(this ScenarioContext context)
        => context.Get<FeatureTestResponse>(HttpRequestSteps.ResponseKey);
}
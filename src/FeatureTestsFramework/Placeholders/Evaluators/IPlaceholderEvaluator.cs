using TechTalk.SpecFlow;

namespace FeatureTestsFramework.Placeholders.Evaluators
{
    public interface IPlaceholderEvaluator
    {
        string Evaluate(string key, ScenarioContext context);
    }
}

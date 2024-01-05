using TechTalk.SpecFlow;

namespace FeatureTestsFramework.Placeholders.Replacers
{
    public interface IPlaceholderEvaluator
    {
        string Evaluate(string key, ScenarioContext context);
    }
}

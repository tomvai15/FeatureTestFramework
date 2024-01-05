using TechTalk.SpecFlow;

namespace FeatureTestsFramework.Placeholders.Evaluators
{
    public interface ICustomPlaceholderEvaluator
    {
        string Key { get; }
        string Evaluate(ScenarioContext context);
    }
}

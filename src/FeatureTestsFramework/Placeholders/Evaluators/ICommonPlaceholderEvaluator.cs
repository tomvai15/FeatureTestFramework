using TechTalk.SpecFlow;

namespace FeatureTestsFramework.Placeholders.Evaluators
{
    public interface ICommonPlaceholderEvaluator
    {
        string Key { get; }
        string Evaluate(ScenarioContext context);
    }
}

using Reqnroll;

namespace FeatureTestsFramework.Placeholders.Evaluators;

public interface ICommonPlaceholderEvaluator
{
    string Key { get; }
    string Evaluate(IScenarioContext context);
}
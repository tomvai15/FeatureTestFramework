using Reqnroll;

namespace FeatureTestsFramework.Placeholders.Evaluators;

public interface IPlaceholderEvaluator
{
    string Evaluate(string key, IScenarioContext context);
}
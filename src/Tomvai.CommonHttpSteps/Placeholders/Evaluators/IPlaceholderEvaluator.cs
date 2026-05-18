using Reqnroll;

namespace Tomvai.CommonHttpSteps.Placeholders.Evaluators;

public interface IPlaceholderEvaluator
{
    string Evaluate(string key, IScenarioContext context);
}
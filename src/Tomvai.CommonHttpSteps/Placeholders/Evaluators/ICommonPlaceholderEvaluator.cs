using Reqnroll;

namespace Tomvai.CommonHttpSteps.Placeholders.Evaluators;

public interface ICommonPlaceholderEvaluator
{
    string Key { get; }
    string Evaluate(IScenarioContext context);
}
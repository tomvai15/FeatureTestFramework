using Reqnroll;
using Tomvai.CommonHttpSteps.Placeholders.Evaluators;

namespace Tomvai.CommonHttpSteps.Placeholders;

public class PlaceholderEvaluatorBase : TextEvaluator, IPlaceholderEvaluator
{
    public PlaceholderEvaluatorBase(IEnumerable<ICommonPlaceholderEvaluator> customEvaluators) : base(customEvaluators)
    {
    }

    public string Evaluate(string key, IScenarioContext context) => GetValueOfKey(key, context);
}
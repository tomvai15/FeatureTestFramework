using FeatureTestsFramework.Placeholders.Evaluators;
using Reqnroll;

namespace FeatureTestsFramework.Placeholders;

public class PlaceholderEvaluatorBase : TextEvaluator, IPlaceholderEvaluator
{
    public PlaceholderEvaluatorBase(IEnumerable<ICommonPlaceholderEvaluator> customEvaluators) : base(customEvaluators)
    {
    }

    public string Evaluate(string key, IScenarioContext context) => GetValueOfKey(key, context);
}
using FeatureTestsFramework.Placeholders.Evaluators;
using TechTalk.SpecFlow;

namespace FeatureTestsFramework.Placeholders
{
    public abstract class PlaceholderEvaluatorConfiguration : TextEvaluator, IPlaceholderEvaluator
    {
        protected PlaceholderEvaluatorConfiguration(IEnumerable<ICommonPlaceholderEvaluator> customEvaluators) : base(customEvaluators)
        {
        }

        public string Evaluate(string key, ScenarioContext context) => GetValueOfKey(key, context);
    }
}

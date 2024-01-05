using FeatureTestsFramework.Placeholders.Evaluators;
using TechTalk.SpecFlow;

namespace FeatureTestsFramework.Placeholders.Uri
{
    public class UriPlaceholderEvaluator : TextEvaluator, IUriPlaceholderEvaluator
    {
        public UriPlaceholderEvaluator(IEnumerable<ICustomPlaceholderEvaluator> customEvaluators) : base(customEvaluators)
        {
            this.AddAnyGeneratedValueEvaluator();
            this.AddLastRequestEvaluator();
            this.AddTableOrConstantEvaluator();
        }

        public string Evaluate(string key, ScenarioContext context)
        {
            return GetValueOfKey(key, context);
        }
    }
}

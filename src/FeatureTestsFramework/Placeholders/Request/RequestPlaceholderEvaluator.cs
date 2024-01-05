using FeatureTestsFramework.Placeholders.Evaluators;
using TechTalk.SpecFlow;

namespace FeatureTestsFramework.Placeholders.Request
{
    public class RequestPlaceholderEvaluator : TextEvaluator, IRequestPlaceholderEvaluator
    {
        public RequestPlaceholderEvaluator(IEnumerable<ICustomPlaceholderEvaluator> customEvaluators) : base(customEvaluators)
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

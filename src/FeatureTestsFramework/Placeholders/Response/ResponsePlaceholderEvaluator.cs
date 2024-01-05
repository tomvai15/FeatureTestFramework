using FeatureTestsFramework.Placeholders.Evaluators;
using TechTalk.SpecFlow;

namespace FeatureTestsFramework.Placeholders.Response
{
    public class ResponsePlaceholderEvaluator : TextEvaluator, IResponsePlaceholderEvaluator
    {
        public ResponsePlaceholderEvaluator(IEnumerable<ICustomPlaceholderEvaluator> customEvaluators) : base(customEvaluators)
        {
            this.AddValueFormatExpectationEvaluators();
            this.AddLastRequestEvaluator();
            this.AddTableOrConstantEvaluator();
        }

        public string Evaluate(string key, ScenarioContext context)
        {
            return GetValueOfKey(key, context);
        }
    }
}

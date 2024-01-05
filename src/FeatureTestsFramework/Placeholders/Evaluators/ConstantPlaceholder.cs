using TechTalk.SpecFlow;

namespace FeatureTestsFramework.Placeholders.Evaluators
{
    public class ConstantPlaceholder : ICustomPlaceholderEvaluator
    {
        public string Key { get; }

        private readonly string _value;

        public ConstantPlaceholder(string key, string value)
        {
            Key = key;
            _value = value;
        }

        public string Evaluate(ScenarioContext context) => _value;
    }
}

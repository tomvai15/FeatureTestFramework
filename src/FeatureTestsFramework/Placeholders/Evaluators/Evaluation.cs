using TechTalk.SpecFlow;

namespace FeatureTestsFramework.Placeholders.Evaluators
{
    public class Evaluation
    {
        public Func<string, ScenarioContext, string> Evaluate { get; set; }
        public string FailureReason { get; set; }
    }
}

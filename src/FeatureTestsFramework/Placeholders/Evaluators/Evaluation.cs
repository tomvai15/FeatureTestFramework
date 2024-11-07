using TechTalk.SpecFlow;

namespace FeatureTestsFramework.Placeholders.Evaluators
{
    public class Evaluation
    {
        public Func<string, IScenarioContext, string> Evaluate { get; set; }
        public string FailureReason { get; set; }
    }
}

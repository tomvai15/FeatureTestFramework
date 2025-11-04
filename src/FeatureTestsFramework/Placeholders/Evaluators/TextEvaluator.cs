using Reqnroll;
using static System.Environment;

namespace FeatureTestsFramework.Placeholders.Evaluators
{
    public abstract class TextEvaluator
    {
        private readonly HashSet<Evaluation> evaluations = new();
        private readonly Dictionary<string, Func<IScenarioContext, string>> constantEvaluators = new();

        protected virtual string EvaluatorName => this.GetType().Name;

        protected TextEvaluator(IEnumerable<ICommonPlaceholderEvaluator> customEvaluators)
        {
            foreach (var customEvaluator in customEvaluators)
            {
                AddEvaluation(customEvaluator.Key, customEvaluator.Evaluate);
            }
        }

        public void AddEvaluation(string key, string result)
        {
            constantEvaluators.Add(key, (_) => result);
        }

        public void AddEvaluation(string key, Func<IScenarioContext, string> evaluation)
        {
            constantEvaluators.Add(key, (context) => evaluation(context));
        }

        public void AddEvaluation(Func<string, IScenarioContext, string> evaluation, string failureReason)
        {
            evaluations.Add(new Evaluation { Evaluate = evaluation, FailureReason = failureReason });
        }

        public virtual string GetValueOfKey(string key, IScenarioContext context)
        {
            if (constantEvaluators.ContainsKey(key))
            {
                return constantEvaluators[key](context);
            }

            string? value = null;
            foreach (var evaluation in evaluations)
            {
                value = evaluation.Evaluate(key, context);
                if (value != null)
                {
                    break;
                }
            }

            if (value == null)
            {
                throw new Exception($"\"{EvaluatorName}\" couldn't evaluate placeholder \"{key}\", because:{NewLine}" +
                    $"- placeholder did not match the following placeholders: {string.Join(", ", constantEvaluators.Keys)} or{NewLine}" +
                    "- " + string.Join(" or" + NewLine + "- ", evaluations.Select(x => x.FailureReason)) + 
                    ".");
            }

            return value;
        }
    }
}

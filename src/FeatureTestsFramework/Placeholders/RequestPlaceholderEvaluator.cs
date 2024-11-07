using FeatureTestsFramework.Placeholders.Evaluators;

namespace FeatureTestsFramework.Placeholders
{
    public interface IRequestPlaceholderEvaluator : IPlaceholderEvaluator
    {
    }

    public class RequestPlaceholderEvaluator : PlaceholderEvaluatorBase, IRequestPlaceholderEvaluator
    {
        public RequestPlaceholderEvaluator(IEnumerable<ICommonPlaceholderEvaluator> customEvaluators) : base(customEvaluators)
        {
            this.AddAnyGeneratedValueEvaluator();
            this.AddLastRequestEvaluator();
            this.AddTableOrConstantEvaluator();
        }
    }
}

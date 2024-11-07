using FeatureTestsFramework.Placeholders.Evaluators;

namespace FeatureTestsFramework.Placeholders
{
    public interface IUriPlaceholderEvaluator : IPlaceholderEvaluator
    {
    }

    public class UriPlaceholderEvaluator : PlaceholderEvaluatorBase, IUriPlaceholderEvaluator
    {
        public UriPlaceholderEvaluator(IEnumerable<ICommonPlaceholderEvaluator> customEvaluators) : base(customEvaluators)
        {
            this.AddAnyGeneratedValueEvaluator();
            this.AddLastRequestEvaluator();
            this.AddTableOrConstantEvaluator();
        }
    }
}

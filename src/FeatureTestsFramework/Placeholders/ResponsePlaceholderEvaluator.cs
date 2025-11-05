using FeatureTestsFramework.Placeholders.Evaluators;

namespace FeatureTestsFramework.Placeholders;

public interface IResponsePlaceholderEvaluator : IPlaceholderEvaluator
{
}

public class ResponsePlaceholderEvaluator : PlaceholderEvaluatorBase, IResponsePlaceholderEvaluator
{
    public ResponsePlaceholderEvaluator(IEnumerable<ICommonPlaceholderEvaluator> customEvaluators) : base(customEvaluators)
    {
        this.AddValueFormatExpectationEvaluators();
        this.AddLastRequestEvaluator();
        this.AddTableOrConstantEvaluator();
    }
}
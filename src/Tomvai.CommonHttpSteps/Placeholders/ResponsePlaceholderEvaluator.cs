using Tomvai.CommonHttpSteps.Placeholders.Evaluators;

namespace Tomvai.CommonHttpSteps.Placeholders;

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
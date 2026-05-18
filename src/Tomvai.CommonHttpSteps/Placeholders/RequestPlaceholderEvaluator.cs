using Tomvai.CommonHttpSteps.Placeholders.Evaluators;

namespace Tomvai.CommonHttpSteps.Placeholders;

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
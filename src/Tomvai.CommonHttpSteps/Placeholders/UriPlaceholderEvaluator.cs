using Tomvai.CommonHttpSteps.Placeholders.Evaluators;

namespace Tomvai.CommonHttpSteps.Placeholders;

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
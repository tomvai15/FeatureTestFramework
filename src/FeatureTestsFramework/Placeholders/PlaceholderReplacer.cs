using System.Text;
using System.Text.RegularExpressions;
using FeatureTestsFramework.Exceptions;
using FeatureTestsFramework.Placeholders.Evaluators;
using Reqnroll;

namespace FeatureTestsFramework.Placeholders;

public interface IPlaceholderReplacer<T> where T : IPlaceholderEvaluator
{
    string Replace(string text, IScenarioContext context);
}

public class PlaceholderReplacer<TPlaceholderEvaluator> : IPlaceholderReplacer<TPlaceholderEvaluator>
    where TPlaceholderEvaluator : IPlaceholderEvaluator
{
    private const string PlaceholderStart = "{{";
    private const string PlaceholderEnd = "}}";

    private readonly TPlaceholderEvaluator _placeholderEvaluator;

    public PlaceholderReplacer(TPlaceholderEvaluator placeholderEvaluator)
    {
        _placeholderEvaluator = placeholderEvaluator;
    }

    public string Replace(string text, IScenarioContext context)
    {
        var palceholders = FindAllPlaceholders(text);
        var replacedText = new StringBuilder(text);
        foreach (var placeholder in palceholders)
        {
            ReplacePlaceholderWithValue(replacedText, placeholder, context);
        }
        return replacedText.ToString();
    }

    private IEnumerable<string> FindAllPlaceholders(string text)
    {
        return Regex.Matches(text, "{{[^{{}}]+}}")
            .Select(capture => capture.Value
                .Replace(PlaceholderStart, string.Empty)
                .Replace(PlaceholderEnd, string.Empty));
    }

    private void ReplacePlaceholderWithValue(StringBuilder json, string placeholder, IScenarioContext context)
    {
        var placeholderValue = _placeholderEvaluator.Evaluate(placeholder, context);

        if (placeholderValue == null)
        {
            throw new MissingPlaceholderValueException($"Placeholder {placeholder} was not found");
        }

        var valueToReplace = PlaceholderStart + placeholder + PlaceholderEnd;
        json.Replace(valueToReplace, placeholderValue);
    }
}
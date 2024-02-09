using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace FeatureTestsFramework.Placeholders.Replacers
{
    public interface IPlaceholderReplacer<T> where T : IPlaceholderEvaluator
    {
        string Replace(string text, ScenarioContext context);
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

        public string Replace(string text, ScenarioContext context)
        {
            var palceholders = FindAllPlaceholders(text);
            var replacedText = text;
            foreach (var placeholder in palceholders)
            {
                replacedText = ReplacePlaceholderWithValue(replacedText, placeholder, context);
            }
            return replacedText;
        }

        private IEnumerable<string> FindAllPlaceholders(string text)
        {
            return Regex.Matches(text, "{{[^{{}}]+}}")
                .Select(capture => capture.Value
                    .Replace(PlaceholderStart, string.Empty)
                    .Replace(PlaceholderEnd, string.Empty));
        }

        private string ReplacePlaceholderWithValue(string json, string placeholder, ScenarioContext context)
        {
            var placeholderValue = _placeholderEvaluator.Evaluate(placeholder, context);

            if (placeholderValue == null)
            {
                throw new Exception($"Placeholder {placeholder} was not found");
            }

            var valueToReplace = PlaceholderStart + placeholder + PlaceholderEnd;
            return json.Replace(valueToReplace, placeholderValue);
        }
    }
}

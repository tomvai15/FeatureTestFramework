using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace FeatureTestsFramework.Extensions;

public static class JsonExtensions
{
    public static string FormatJsonWithPlaceholders(this string json)
    {
        var matches = Regex.Matches(json, "(?<!\\\"[^\":]*){{.[^{}]*}}(?![^\",]*\\\")");

        foreach (var match in matches.Reverse())
        {
            var placeholderValue = EscapePlaceholder(match.Value);
            json = json.ReplaceSegment(match.Index, match.Length, placeholderValue);
        }

        json = json.FormatAsJToken();

        foreach (var match in matches.AsEnumerable())
        {
            var placeholderValue = EscapePlaceholder(match.Value);
            json = json.Replace(placeholderValue, match.Value);
        }

        json = json.Replace("[", "\\[").Replace("]", "\\]");

        return json;
    }

    public static string FormatAsJToken(this string json)
    {
        return JObject.Parse(json).ToString();
    }

    public static string ReplaceSegment(this string text, int index, int length, string replacement)
    {
        return text.Remove(index, length).Insert(index, replacement);
    }

    private static string EscapePlaceholder(string palceholder)
    {
        const string markerStart = "\"__";
        const string markerEnd = "__\"";

        const string placeholderStart = "{{";
        const string placeholderEnd = "}}";

        return palceholder.Replace(placeholderStart, markerStart).Replace(placeholderEnd, markerEnd);
    }
}
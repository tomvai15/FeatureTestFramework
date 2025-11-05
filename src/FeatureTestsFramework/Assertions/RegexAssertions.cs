using FluentAssertions;
using System.Text.RegularExpressions;
using static System.Environment;

namespace FeatureTestsFramework.Assertions;

public static class RegexAssertions
{
    public static string ShouldMatchRegexLineByLine(this string actualResponseJson, string expectedRegexResponseJson)
    {
        var expressionsFailedToMatch = FindExpressionsFailedToMatch(actualResponseJson, expectedRegexResponseJson);
        expressionsFailedToMatch.Should().BeEmpty(BuildErrorMessage(actualResponseJson, expressionsFailedToMatch));
        return actualResponseJson;
    }

    private static List<string> FindExpressionsFailedToMatch(string actualResponseJson, string expectedRegexResponseJson)
    {
        var expressions = expectedRegexResponseJson.Split(NewLine);
        return expressions.Where(regex => !Regex.IsMatch(actualResponseJson, regex)).ToList();
    }

    private static string BuildErrorMessage(string actualJson, List<string> expressionsFailedToMatch)
    {
        var failedMatch = string.Join(NewLine, expressionsFailedToMatch);
        return $"Expected regexed response to match. {NewLine}" +
               $"The following did not contain any matches. {NewLine}" +
               $"{failedMatch}{NewLine}" +
               $"Actual: {NewLine}" +
               $"{actualJson}";

    }
}
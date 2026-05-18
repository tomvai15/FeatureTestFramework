using AnyOfTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WireMock.Matchers;
using WireMock.Models;

namespace Tomvai.CommonHttpSteps.Assertions;

public class PartialJsonMatcher : IStringMatcher
{
    private readonly string _expectedJson;
    private readonly JToken _expectedToken;

    public PartialJsonMatcher(string expectedJson)
    {
        _expectedJson = expectedJson;
        _expectedToken = JToken.Parse(expectedJson);
    }

    public MatchResult IsMatch(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return 0;

        JToken actual;
        try
        {
            actual = JToken.Parse(input);
        }
        catch
        {
            return 0;
        }

        return Matches(_expectedToken, actual)
            ? 1
            : 0;
    }

    public AnyOf<string, StringPattern>[] GetPatterns()
    {
        return new AnyOf<string, StringPattern>[] { _expectedJson };
    }

    public MatchOperator MatchOperator { get; }

    public string GetCSharpCodeArguments()
    {
        // This is used when WireMock exports mappings as C#
        // Must return valid C# constructor arguments
        return JsonConvert.SerializeObject(_expectedJson);
    }

    public string Name { get; }
    public MatchBehaviour MatchBehaviour { get; }

    private static bool Matches(JToken expected, JToken actual)
    {
        if (expected == null)
            return true;

        if (actual == null || expected.Type != actual.Type)
            return false;

        switch (expected.Type)
        {
            case JTokenType.Object:
                foreach (var prop in expected.Children<JProperty>())
                {
                    if (!Matches(prop.Value, actual[prop.Name]))
                        return false;
                }
                return true;

            case JTokenType.Array:
                var expArr = (JArray)expected;
                var actArr = (JArray)actual;

                if (actArr.Count < expArr.Count)
                    return false;

                for (int i = 0; i < expArr.Count; i++)
                {
                    if (!Matches(expArr[i], actArr[i]))
                        return false;
                }
                return true;

            default:
                return JToken.DeepEquals(expected, actual);
        }
    }
}
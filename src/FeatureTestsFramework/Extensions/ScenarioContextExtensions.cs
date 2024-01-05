using TechTalk.SpecFlow;

namespace FeatureTestsFramework.Extensions
{
    public static class ScenarioContextExtensions
    {
        public static string? GetTableArg(this ScenarioContext context, string column)
        {
            var args = context.ScenarioInfo.Arguments;
            var isInRow = args.Contains(column);
            if (!isInRow)
            {
                return null;
            }

            return args?[column]?.ToString();
        }
    }
}

using FeatureTestsFramework.Extensions;
using FeatureTestsFramework.HttpRequest;
using static FeatureTestsFramework.Dummy;

namespace FeatureTestsFramework.Placeholders.Evaluators
{
    public static class TextEvaluatorExtensions
    {
        private const string AnyNumberRegex = "-?[0-9]?[.,]?[0-9]+";

        private const string AnyStringRegex = ".*";

        private const string IdRegex = "[1-9][0-9]*";

        private const string IsoStandardDateRegex = "(\\d{4})-(\\d{2})-(\\d{2})T(\\d{2}):(\\d{2}):(\\d{2}(?:\\.\\d*)?)((-(\\d{2}):(\\d{2})|Z)?)";

        private const string GuidRegex = "[0-9A-Fa-f]{8}-?([0-9A-Fa-f]{4}-?){3}[0-9A-Fa-f]{12}";

        private const string EmptyArrayRegex = "\\[\\s*\\]";

        public static void AddValueFormatExpectationEvaluators(this TextEvaluator placeholderEvaluator)
        {
            placeholderEvaluator.AddEvaluation("AnyId", IdRegex);
            placeholderEvaluator.AddEvaluation("AnyIsoDate", IsoStandardDateRegex);
            placeholderEvaluator.AddEvaluation("AnyGuid", GuidRegex);
            placeholderEvaluator.AddEvaluation("AnyString", AnyStringRegex);
            placeholderEvaluator.AddEvaluation("Any", AnyStringRegex);
            placeholderEvaluator.AddEvaluation("AnyNumber", AnyNumberRegex);
            placeholderEvaluator.AddEvaluation("EmptyArray", EmptyArrayRegex);
        }

        public static void AddAnyGeneratedValueEvaluator(this TextEvaluator placeholderEvaluator)
        {
            placeholderEvaluator.AddEvaluation("AnyId", (_) => Any<long>().ToString());
            placeholderEvaluator.AddEvaluation("NotExistingId", (_) => (long.MaxValue - 1).ToString());
            placeholderEvaluator.AddEvaluation("AnyNegativeNumber", (_) => (-Any<uint>()).ToString());
            placeholderEvaluator.AddEvaluation("AnyPositiveNumber", (_) => Any<uint>().ToString());
            placeholderEvaluator.AddEvaluation("AnyNumber", (_) => Any<int>().ToString());
            placeholderEvaluator.AddEvaluation("AnyDecimal", (_) => (Any<decimal>() + 0.1m).ToString());
            placeholderEvaluator.AddEvaluation("AnyString", (_) => Any<string>()); placeholderEvaluator.AddEvaluation("AnyGuid", (_) => Guid.NewGuid().ToString());
            placeholderEvaluator.AddEvaluation("Today", (_) => DateTime.UtcNow.ToString(format: "O"));
            placeholderEvaluator.AddEvaluation("Midnight", (_) => DateTime.UtcNow.Date.ToString(format: "O"));
            placeholderEvaluator.AddEvaluation("JustBeforeMidnight", (_) => DateTime.UtcNow.Date.AddSeconds(-1).ToString(format: "O"));
            placeholderEvaluator.AddEvaluation("JustAfterMidnight", (_) => DateTime.UtcNow.Date.AddSeconds(1).ToString(format: "O"));
            placeholderEvaluator.AddEvaluation("Future", (_) => Future.ToString(format: "O"));
            placeholderEvaluator.AddEvaluation("Past", (_) => Past.ToString(format: "O"));
            placeholderEvaluator.AddEvaluation("NoDate", (_) => default(DateTime).ToString(format: "O"));

            placeholderEvaluator.AddEvaluation((key, context) =>
            {
                if (IsTextFormula(key))
                {
                    var length = GetLength(key, "Text");
                    return Text(length);
                }
                if (IsDaysInFutureFormula(key))
                {
                    var length = GetLength(key, "DaysInFuture");
                    return DateTime.UtcNow.AddDays(length).ToString(format: "O");
                }
                if (IsDaysInPastFormulate(key))
                {
                    var length = GetLength(value: key, placeholder: "DaysInPast");
                    return DateTime.UtcNow.AddDays(-length).ToString(format: "O");
                }
                return null;
            }, failureReason: "placeholder did not match format for TextX, DaysInFutureX, DaysInPastX");
        }

        private static bool IsTextFormula(string value) => value.StartsWith("Text");
        private static bool IsDaysInFutureFormula(string value) => value.StartsWith("DaysInFuture");
        private static bool IsDaysInPastFormulate(string value) => value.StartsWith("DaysInPast");
        private static int GetLength(string value, string placeholder) => int.Parse(value.Substring(placeholder.Length));

        public static void AddLastRequestEvaluator(this TextEvaluator placeholderEvaluator)
        {
            placeholderEvaluator.AddEvaluation((key, context) =>
            { 
                var requestsStore = context.GetRequiredService<IRequestStore>();
                var value = requestsStore.GetValue(key);

                if (value == null)
                {
                    return null;
                }

                // value types have only lowercase letters
                if (value.GetType().IsValueType)
                {
                    return value.ToString().ToLower();
                }
                return value.ToString();
            }, failureReason: "placeholder was not found in previous request properties");         
        }

        public static void AddTableOrConstantEvaluator(this TextEvaluator placeholderEvaluator)
        {
            placeholderEvaluator.AddEvaluation((key, context) =>
            {
                var valueInContext = context.TryGetValue(key, out string value);
                return valueInContext ? value : context.GetTableArg(key);
            },
            failureReason: "placeholder was not found in the examples table or ScenarioContext Dictionary");
        }
    }
}

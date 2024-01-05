using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureTestsFramework.Placeholders.Evaluators
{
    public static class TextEvaluatorExtensions
    {
        private const string AnyRegex = "-?[0-9]?[.,]?[0-9]+";

        private const string AnyStringRegex = ".*";

        private const string IdRegex = "[1-9][0-9]*";
    }
}

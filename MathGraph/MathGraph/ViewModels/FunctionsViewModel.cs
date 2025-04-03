using System.Collections.Generic;
using System.Text;

namespace MathGraph.ViewModels
{
    public class FunctionsViewModel : ViewModelBase
    {
        public string FunctionsList { get; }

        public FunctionsViewModel()
        {
            List<string> functions = new()
            {
                "cos(x) - cosine",
                "sin(x) - sine",
                "tan(x) - tangent",
                "cot(x) - cotangent",
                "sec(x) - secant",
                "csc(x) - cosecant",
                "acos(x) - inverse cosine (arccos)",
                "asin(x) - inverse sine (arcsin)",
                "atan(x) - inverse tangent (arctan)",
                "acot(x) - inverse cotangent (arccot)",
                "asec(x) - inverse secant (arcsec)",
                "acsc(x) - inverse cosecant (arccsc)",
                "cosh(x) - hyperbolic cosine",
                "sinh(x) - hyperbolic sine",
                "tanh(x) - hyperbolic tangent",
                "coth(x) - hyperbolic cotangent",
                "sech(x) - hyperbolic secant",
                "csch(x) - hyperbolic cosecant",
                "acosh(x) - inverse hyperbolic cosine",
                "asinh(x) - inverse hyperbolic sine",
                "atanh(x) - inverse hyperbolic tangent",
                "acoth(x) - inverse hyperbolic cotangent",
                "asech(x) - inverse hyperbolic secant",
                "acsch(x) - inverse hyperbolic cosecant",
                "log(base, x) - logarithm with custom base",
                "ln(x) - natural logarithm (base e)",
                "e^x - exponential function (e^x)",
                "sqrt(x) - square root",
                "cbrt(x) - cube root",
                "abs(x) - absolute value",
                "sign(x) - sign function",
                "sqr(x) - square",
                "signum(x) - sign function (alternative notation)",
            };

            StringBuilder sb = new();
            foreach (var func in functions)
                sb.AppendLine(func);

            FunctionsList = sb.ToString();
        }
    }
}

using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NCalc;

namespace MathApp.secondary_objects
{
    /// <summary>
    /// Represents a function interpreter that is able to parse strings and return Function series.
    /// </summary>
    internal class FunctionInterpreter
    {
        /// <summary>
        /// Parses the specified function string and returns a Function series that represents it.
        /// </summary>
        /// <param name="functionString">The function string to parse.</param>
        /// <param name="xmin">The minimum value of the x-axis.</param>
        /// <param name="xmax">The maximum value of the x-axis.</param>
        /// <param name="step">The step size of the Function series.</param>
        /// <returns>A Function series that represents the specified function string.</returns>
        public static FunctionSeries ParseFunction(string functionString, double xmin, double xmax, double step)
        {
            if (xmin >= xmax)
            {
                throw new ArgumentException("Invalid range: xmin must be less than xmax.");
            }

            if (step <= 0)
            {
                throw new ArgumentException("Invalid step size: must be greater than zero.");
            }

            try
            {
                // Create a new Expression object with the specified function string
                var expression = new NCalc.Expression(functionString);

                // Define a lambda expression that evaluates the Expression object with the current value of x
                Func<double, double> function = x =>
                {
                    expression.Parameters["x"] = x;
                    double result = (double)expression.Evaluate();
                    return result;
                };

                // Create a new FunctionSeries object using the lambda expression and the specified plot range and step
                var series = new FunctionSeries(function, xmin, xmax, step);

                return series;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during parsing
                throw new ArgumentException("Invalid function string.", ex);
            }
        }
    }

}

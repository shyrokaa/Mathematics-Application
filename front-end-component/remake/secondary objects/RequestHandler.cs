using Antlr.Runtime;
using NCalc;
using OxyPlot;
using OxyPlot.Series;
using System.Data;
using System.Linq.Dynamic.Core.Parser;

namespace MathApp.secondary_objects
{
    public class RequestHandler
    {
        public FunctionSeries PlotRequestParser(string request)
        {
            // Parse the request string to extract the function parameters
            string[] parts = request.Split(new char[] { ' ', '=', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            string functionExpression = parts[1];
            double min = -10;
            double max = 10;
            int resolution = 100;

            // Create the list of data points for the function
            List<DataPoint> dataPoints = new List<DataPoint>();
            for (int i = 0; i < resolution; i++)
            {
                double x = min + (max - min) * i / (resolution - 1);

                try
                {
                    object result = new Expression(functionExpression.Replace("x", x.ToString())).Evaluate();
                    if (result is double)
                    {
                        double y = (double)result;
                        dataPoints.Add(new DataPoint(x, y));
                    }
                    else
                    {
                        throw new ArgumentException("Invalid expression format. Please use a valid mathematical expression.");
                    }
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            // Create the FunctionSeries object
            FunctionSeries series = new FunctionSeries();
            series.ItemsSource = dataPoints;

            return series;
        }
    }

    public class Parser
    {
        public static double? Evaluate(string expression)
        {
            try
            {
                // Create an expression evaluator
                Expression evaluator = new Expression(expression);

                // Register custom functions
                evaluator.EvaluateFunction += delegate (string name, FunctionArgs args)
                {
                    switch (name.ToLower())
                    {
                        case "sin":
                            args.Result = Math.Sin(Convert.ToDouble(args.Parameters[0].Evaluate()));
                            break;
                        case "cos":
                            args.Result = Math.Cos(Convert.ToDouble(args.Parameters[0].Evaluate()));
                            break;
                        case "tan":
                            args.Result = Math.Tan(Convert.ToDouble(args.Parameters[0].Evaluate()));
                            break;
                        case "log":
                            args.Result = Math.Log(Convert.ToDouble(args.Parameters[0].Evaluate()));
                            break;
                        default:
                            throw new EvaluationException($"Function '{name}' not supported");
                    }
                };

                // Evaluate the expression
                object result = evaluator.Evaluate();

                // Convert the result to a double
                double value;
                if (double.TryParse(result.ToString(), out value))
                {
                    return value;
                }
                else
                {
                    MessageBox.Show("Invalid expression format. Please use a valid mathematical expression.", "Invalid Expression", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid expression format. Please use a valid mathematical expression.", "Invalid Expression", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}

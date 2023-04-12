using Antlr.Runtime;
using MathApp.algorithms;
using NCalc;
using OxyPlot;
using OxyPlot.Series;
using System.Data;
using System.Globalization;
using System.Linq.Dynamic.Core.Parser;
using System.Numerics;

namespace MathApp.parsers
{
    public class RequestHandler
    {
        public FunctionSeries PlotRequestParser(string request, double min = -10, double max = 10)
        {
            // Parse the request string to extract the function parameters
            string[] parts = request.Split(new char[] { ' ', '=', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            string functionExpression = parts[1];
            int resolution = 100;

            // Create the list of data points for the function
            List<DataPoint> dataPoints = new List<DataPoint>();
            for (int i = 0; i < resolution; i++)
            {
                double x = min + (max - min) * i / (resolution - 1);

                try
                {
                    double? result = Evaluate(functionExpression.Replace("x", x.ToString()));
                    if (result.HasValue)
                    {
                        double y = result.Value;
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

                // Define the variable "x"
                evaluator.Parameters.Add("x", 0);

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
                MessageBox.Show("Evaluator failed: Invalid expression format. Please use a valid mathematical expression.", "Invalid Expression", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        // Genetic algorithm
        public string EquationRequestParser(string request)
        {
            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm();
            // use the SolveEquation method to find the solution
            Complex solution = geneticAlgorithm.SolveEquation(request);

            // return the solution as a string
            return $"The solution to the equation {request} is {solution}";
        }



        // Calculator
        public double? CalculatorRequestParser(string request)
        {
            try
            {
                // Create an expression evaluator
                Expression evaluator = new Expression(request);

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
                            MessageBox.Show($"Function '{name}' not supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                };

                // Define the variable "x"
                evaluator.Parameters.Add("x", 0);

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
                    MessageBox.Show("Invalid expression format. Please use a valid mathematical expression.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid expression format. Please use a valid mathematical expression.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }



    }
}

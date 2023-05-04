using NCalc;
using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Series;

using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

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


        // Genetic algorithm request to the specific flask service
        public async Task<string> EquationRequestParserAsync(string equation, string intervalBottom, string intervalTop, string nrVals, string ngGenerations, string nrParents, string populationSize)
        {
            // Set up the request body as a JSON object
            var requestBody = new
            {
                generations = ngGenerations,
                population_size = populationSize,
                num_parents = nrParents,
                num_vars = nrVals,
                bottom = intervalBottom,
                top = intervalTop,
                equation = equation
            };
            var json = JsonConvert.SerializeObject(requestBody);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Set up the request URL and headers
            string apiUrl = "http://localhost:5000/genetic_algorithm";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Send the POST request and read the response
            HttpResponseMessage response = await client.PostAsync(apiUrl, data);
            string result = await response.Content.ReadAsStringAsync();

            return result;
        }


        // Integrate request to the specific flask service
        public async Task<string> IntegralRequestParserAsync(string function, string intervalBottom, string intervalTop, string nrTrapezoids)
        {
            // Set up the request body as a JSON object
            var requestBody = new
            {
                function = function,
                a = intervalBottom,
                b = intervalTop,
                n = nrTrapezoids
            };
            var json = JsonConvert.SerializeObject(requestBody);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Set up the request URL and headers
            string apiUrl = "http://localhost:5000/integrate_trapezoid";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Send the POST request and read the response
            HttpResponseMessage response = await client.PostAsync(apiUrl, data);
            string result = await response.Content.ReadAsStringAsync();

            return result;
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

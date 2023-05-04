from flask import Flask, request, jsonify
from genetic_algorithm import run_genetic_algorithm
from integral_numerics import trapezoidal_rule_integral

app = Flask(__name__)


@app.route('/genetic_algorithm', methods=['POST'])
def genetic_algorithm():
    # Parse input data from request
    data = request.get_json()
    # Run genetic algorithm with input data
    result = run_genetic_algorithm(data)
    # Return result as JSON
    return jsonify(result)


@app.route('/integrate_trapezoid', methods=['POST'])
def integrate_func():
    # Parse input data from request
    data = request.get_json()
    func_str = data['function']
    a = float(data['a'])
    b = float(data['b'])
    n = int(data['n'])

    result = trapezoidal_rule_integral(func_str, a, b, n)
    return jsonify(result)


if __name__ == '__main__':
    app.run()

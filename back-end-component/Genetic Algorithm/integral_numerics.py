from sympy import sympify, Symbol


def trapezoidal_rule_integral(integral_str, a, b, n):
    # Define the function to be integrated
    x = Symbol('x')
    f = sympify(integral_str)

    # Calculate the width of each trapezoid
    h = (b - a) / n

    # Calculate the area of each trapezoid
    area = h * (f.subs(x, a) + f.subs(x, b)) / 2
    for i in range(1, n):
        x_i = a + i * h
        area += h * f.subs(x, x_i)

    # Format the result as a string and return it
    result = f"The approximated value of the integral of {integral_str} from {a} to {b} using the trapezoidal rule with {n} trapezoids is {area:.6f}."
    return result

import random
import numpy as np
import sys


# Define the fitness function to minimize
def evaluate(individual, equation):
    # Convert the equation string to a function using eval()
    equation_str = "lambda " + ", ".join([f"x{i}" for i in range(len(individual))]) + ": " + equation
    func = eval(equation_str)
    # Evaluate the function with the current individual's values
    values = tuple(individual)
    fitness = func(*values)
    # Calculate the fitness as the absolute distance from 0
    fitness = abs(fitness)
    return fitness


# Create the individual
def create_individual(size, interval_low, interval_high):
    return np.array([random.uniform(interval_low, interval_high) for _ in range(size)])


# Initialize the population
def create_population(size, num_vars, interval_low, interval_high):
    return [create_individual(num_vars, interval_low, interval_high) for _ in range(size)]


# Select the best individuals
def select(population, num_parents, equation):
    fitnesses = [evaluate(individual, equation) for individual in population]
    parents = np.empty((num_parents, population[0].shape[0]))
    for i in range(num_parents):
        best_idx = np.argmin(fitnesses)
        parents[i, :] = population[best_idx]
        fitnesses[best_idx] = np.inf
    return parents


# Perform crossover
def crossover(parents, offspring_size):
    offspring = np.empty(offspring_size)
    crossover_point = offspring_size[1] // 2
    for i in range(offspring_size[0]):
        parent1_idx = i % parents.shape[0]
        parent2_idx = (i + 1) % parents.shape[0]
        offspring[i, :crossover_point] = parents[parent1_idx, :crossover_point]
        offspring[i, crossover_point:] = parents[parent2_idx, crossover_point:]
    return offspring


# Perform mutation
def mutate(offspring):
    for i in range(offspring.shape[0]):
        mutation_idx = random.randint(0, offspring.shape[1] - 1)
        offspring[i, mutation_idx] += random.uniform(-1.0, 1.0)
    return offspring


def run_genetic_algorithm(data):
    # Read the genetic algorithm parameters from the data dictionary
    num_generations = int(data['generations'])
    population_size = int(data['population_size'])
    num_parents = int(data['num_parents'])
    num_vars = int(data['num_vars'])
    bottom = int(data['bottom'])
    top = int(data['top'])
    equation = data['equation']

    # Define the fitness function
    def evaluate(individual):
        # Convert the equation string to a function using eval()
        equation_str = "lambda " + ", ".join([f"x{i}" for i in range(num_vars)]) + ": " + equation
        try:
            func = eval(equation_str)
        except Exception as e:
            with open("results.txt", "w") as file:
                file.write(f"Error: {e}")
            return

        # Evaluate the function with the current individual's values
        values = tuple(individual[i] for i in range(num_vars))
        try:
            fitness = func(*values)
        except Exception as e:
            with open("results.txt", "w") as file:
                file.write(f"Error: {e}")
            return

        # Calculate the fitness as the absolute distance from 0
        fitness = abs(fitness)
        return fitness

    # Create the individual
    def create_individual():
        return np.array([random.uniform(bottom, top) for _ in range(num_vars)])

    # Initialize the population
    population = [create_individual() for _ in range(population_size)]

    # Run the evolution
    for gen in range(num_generations):
        # Select the parents
        fitnesses = [evaluate(individual) for individual in population]
        if None in fitnesses:
            return
        parents = [population[i] for i in np.argsort(fitnesses)[:num_parents]]

        # Create the offspring
        offspring = []
        for i in range(population_size - num_parents):
            # Select two parents randomly
            parent1 = random.choice(parents)
            parent2 = random.choice(parents)
            # Perform crossover
            child = np.empty(num_vars)
            crossover_point = random.randint(0, num_vars - 1)
            child[:crossover_point] = parent1[:crossover_point]
            child[crossover_point:] = parent2[crossover_point:]
            # Perform mutation
            mutation_prob = 1.0 / num_vars
            for j in range(num_vars):
                if random.random() < mutation_prob:
                    child[j] += random.uniform(-1.0, 1.0)
                    child[j] = max(child[j], bottom)
                    child[j] = min(child[j], top)
            offspring.append(child)

        # Replace the old population with the new population
        population = parents + offspring

    # Write the best solution to a file
    fitnesses = [evaluate(individual) for individual in population]
    if None in fitnesses:
        return
    best_idx = np.argmin(fitnesses)
    best_solution = " ".join(str(x) for x in population[best_idx])
    return best_solution

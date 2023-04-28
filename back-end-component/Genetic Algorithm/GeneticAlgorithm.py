import random
import numpy as np
import sys


# Define the fitness function to minimize
def evaluate(individual, equation, num_vars):
    # Convert the equation string to a function using eval()
    equation_str = "lambda " + ", ".join([f"x{i}" for i in range(num_vars)]) + ": " + equation
    func = eval(equation_str)
    # Evaluate the function with the current individual's values
    values = tuple(individual[i] for i in range(num_vars))
    return func(*values)


# Create the individual
def create_individual(num_vars, interval_low, interval_high):
    return np.array([random.uniform(-5.0, 5.0) for _ in range(num_vars)])


# Initialize the population
def create_population(size, num_vars, interval_low, interval_high):
    return [create_individual(num_vars, interval_low, interval_high) for _ in range(size)]


# Select the best individuals
def select(population, num_parents, equation, num_vars):
    fitnesses = [evaluate(individual, equation, num_vars) for individual in population]
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


def main():
    # Define the genetic algorithm parameters
    num_generations = 100
    population_size = 50
    num_parents = 5

    # reading theese from system calls

    num_vars =  int(sys.argv[1])
    top =  int(sys.argv[2])
    bottom =  int(sys.argv[3])
    equation =  (sys.argv[4]) # Change this to the equation you want to minimize

    offspring_size = (population_size - num_parents, num_vars)
    # Initialize the population
    population = create_population(population_size, num_vars, interval_low=top, interval_high=bottom)

    # Run the evolution
    for gen in range(num_generations):
        # Select the parents
        parents = select(population, num_parents, equation, num_vars)

        # Create the offspring
        offspring = crossover(parents, offspring_size)

        # Mutate the offspring
        offspring = mutate(offspring)

        # Evaluate the fitness of the offspring
        offspring_fitnesses = [evaluate(individual, equation, num_vars) for individual in offspring]

        # Replace the worst individuals in the population with the offspring
        population_fitnesses = [evaluate(individual, equation, num_vars) for individual in population]
        for i in range(len(offspring)):
            worst_idx = np.argmax(population_fitnesses)
            if offspring_fitnesses[i] < population_fitnesses[worst_idx]:
                population[worst_idx] = offspring[i]
                population_fitnesses[worst_idx] = offspring_fitnesses[i]
                # Print the best individual in the current generation
                best_idx = np.argmin(population_fitnesses)
                print(
                    f"Generation {gen + 1}: Best individual = {population[best_idx]}, Best fitness = {population_fitnesses[best_idx]}")

            # Print the final best individual
            best_idx = np.argmin(population_fitnesses)
            print(
                f"Final result: Best individual = {population[best_idx]}, Best fitness = {population_fitnesses[best_idx]}")


if __name__ == "__main__":
    main()

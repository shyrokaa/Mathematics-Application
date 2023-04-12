using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MathApp.algorithms
{
    internal class GeneticAlgorithm
    {
        private Random _random;

        public GeneticAlgorithm()
        {
            _random = new Random();
            
        }

        public Complex SolveEquation(string equation)
        {
            int populationSize = 100;
            int chromosomeLength = 10;
            double minGeneValue = -10.0;
            double maxGeneValue = 10.0;
            int maxGenerations = 1000;
            double targetFitness = 0.99;

            // Generate an initial population
            List<Chromosome> population = GenerateInitialPopulation(populationSize, chromosomeLength, minGeneValue, maxGeneValue);

            for (int generation = 0; generation < maxGenerations; generation++)
            {
                // Evaluate the fitness of each chromosome in the population
                foreach (Chromosome chromosome in population)
                {
                    chromosome.Fitness = CalculateFitness(chromosome, equation);
                }

                // Check if the target fitness has been reached
                double bestFitness = population.Max(c => c.Fitness);
                if (bestFitness >= targetFitness)
                {
                    // Return the solution as a complex number
                    Chromosome bestChromosome = population.OrderByDescending(c => c.Fitness).First();
                    double[] solutionValues = bestChromosome.Genes;
                    Complex solution = new Complex(solutionValues[0], solutionValues[1]);
                    return solution;
                }

                // Evolve the population for the next generation
                population = EvolvePopulation(population);
            }

            // Return the best solution found as a complex number
            Chromosome bestChromosomeOverall = population.OrderByDescending(c => c.Fitness).First();
            double[] overallSolutionValues = bestChromosomeOverall.Genes;
            Complex overallSolution = new Complex(overallSolutionValues[0], overallSolutionValues[1]);
            return overallSolution;
        }


        private List<Chromosome> GenerateInitialPopulation(int populationSize, int chromosomeLength, double minGeneValue, double maxGeneValue)
        {
            List<Chromosome> population = new List<Chromosome>();

            for (int i = 0; i < populationSize; i++)
            {
                Chromosome chromosome = new Chromosome();

                for (int j = 0; j < chromosomeLength; j++)
                {
                    chromosome.Genes[j] = _random.NextDouble() * (maxGeneValue - minGeneValue) + minGeneValue;
                }

                population.Add(chromosome);
            }

            return population;
        }



        private List<Chromosome> EvolvePopulation(List<Chromosome> population)
        {
            List<Chromosome> offspringPopulation = new List<Chromosome>();

            // Perform tournament selection to select the parents for the crossover operation
            for (int i = 0; i < population.Count; i++)
            {
                Chromosome parent1 = TournamentSelection(population);
                Chromosome parent2 = TournamentSelection(population);

                // Perform crossover between the parents to create a new child chromosome
                Chromosome child = Crossover(parent1, parent2);

                // Perform mutation on the child chromosome
                Mutate(child);

                // Add the child chromosome to the offspring population
                offspringPopulation.Add(child);
            }

            // Replace the current population with the offspring population
            return offspringPopulation;
        }

        private Chromosome TournamentSelection(List<Chromosome> population)
        {
            int tournamentSize = 2;
            List<Chromosome> tournament = new List<Chromosome>();

            // Select a random subset of chromosomes from the population
            for (int i = 0; i < tournamentSize; i++)
            {
                int index = _random.Next(population.Count);
                tournament.Add(population[index]);
            }

            // Select the chromosome with the highest fitness from the tournament as the winner
            return tournament.OrderByDescending(c => c.Fitness).First();
        }


        private Chromosome Crossover(Chromosome parent1, Chromosome parent2)
        {
            // Perform single-point crossover between the parents to create a new child chromosome
            int crossoverPoint = _random.Next(parent1.Genes.Length);
            Chromosome child = new Chromosome();

            for (int i = 0; i < parent1.Genes.Length; i++)
            {
                if (i < crossoverPoint)
                {
                    child.Genes[i] = parent1.Genes[i];
                }
                else
                {
                    child.Genes[i] = parent2.Genes[i];
                }
            }

            return child;
        }

        private Chromosome Mutate(Chromosome chromosome)
        {
            // Perform uniform mutation on the chromosome
            double mutationProbability = 0.1;

            for (int i = 0; i < chromosome.Genes.Length; i++)
            {
                if (_random.NextDouble() < mutationProbability)
                {
                    chromosome.Genes[i] += _random.NextDouble() * 0.2 - 0.1;
                }
            }

            return chromosome;
        }

        private double CalculateFitness(Chromosome chromosome, string equation)
        {
            double totalFitness = 0.0;

            for (int i = 0; i < chromosome.Genes.Length; i++)
            {
                double geneValue = chromosome.Genes[i];
                string geneSymbol = "x" + (i + 1).ToString();

                // Replace the gene symbol with the gene value in the equation
                string evaluatedEquation = equation.Replace(geneSymbol, geneValue.ToString());

                try
                {
                    // Evaluate the equation and parse the result as a complex number
                    Complex solution = Complex.Parse(new DataTable().Compute(evaluatedEquation, null).ToString(), CultureInfo.InvariantCulture);

                    // Calculate the fitness based on the distance from the origin
                    double fitness = 1.0 / (1.0 + solution.Magnitude);
                    totalFitness += fitness;
                }
                catch (Exception ex)
                {
                    // Ignore exceptions caused by invalid equations
                    Console.WriteLine("Invalid equation: " + evaluatedEquation);
                }
            }

            // Calculate the average fitness for all the genes
            return totalFitness / chromosome.Genes.Length;
        }



        private class Chromosome
        {
            public double[] Genes { get; set; }
            public double Fitness { get; set; }

            public Chromosome()
            {
                Genes = new double[10];
                Fitness = 0.0;
            }
        }
    }

}

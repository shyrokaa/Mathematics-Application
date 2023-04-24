package com.mathapp.geneticalgorithm.model;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Random;

public class GeneticAlgorithm {
    // The size of the population
    private int populationSize;
    // The mutation rate
    private double mutationRate;
    // The crossover rate
    private double crossoverRate;
    // The tournament size for selection
    private int tournamentSize;
    // The target fitness score
    private double targetFitness;
    // The list of chromosomes in the population
    private List<Chromosome> population;

    /**
     * Constructor for creating a new GeneticAlgorithm object
     *
     * @param populationSize the size of the population
     * @param mutationRate   the mutation rate
     * @param crossoverRate  the crossover rate
     * @param tournamentSize the tournament size for selection
     * @param targetFitness  the target fitness score
     */
    public GeneticAlgorithm(int populationSize, double mutationRate, double crossoverRate, int tournamentSize, double targetFitness) {
        this.populationSize = populationSize;
        this.mutationRate = mutationRate;
        this.crossoverRate = crossoverRate;
        this.tournamentSize = tournamentSize;
        this.targetFitness = targetFitness;
        this.population = new ArrayList<>();
    }

    /**
     * Initializes the population with random chromosomes
     *
     * @param chromosomeLength the length of each chromosome
     */
    public void initializePopulation(int chromosomeLength) {
        for (int i = 0; i < populationSize; i++) {
            String genes = generateRandomGenes(chromosomeLength);
            double fitness = calculateFitness(genes);
            population.add(new Chromosome(genes, fitness));
        }
    }

    /**
     * Generates a random string of genes for a chromosome
     *
     * @param chromosomeLength the length of the chromosome
     * @return a string of random genes
     */
    private String generateRandomGenes(int chromosomeLength) {
        StringBuilder sb = new StringBuilder();
        Random rand = new Random();
        for (int i = 0; i < chromosomeLength; i++) {
            char gene = (char) (rand.nextInt(26) + 'a');
            sb.append(gene);
        }
        return sb.toString();
    }

    /**
     * Calculates the fitness score for a given set of genes
     *
     * @param genes the genes to evaluate
     * @return the fitness score
     */
    private double calculateFitness(String genes) {
        // Calculate the fitness score here based on your specific problem domain
        return 0.0;
    }

    /**
     * Selects a subset of the population for breeding using tournament selection
     *
     * @return a list of chromosomes selected for breeding
     */
    private List<Chromosome> tournamentSelection() {
        List<Chromosome> tournament = new ArrayList<>();
        Random rand = new Random();
        for (int i = 0; i < tournamentSize; i++) {
            int index = rand.nextInt(populationSize);
            tournament.add(population.get(index));
        }
        Collections.sort(tournament, Collections.reverseOrder());
        return tournament.subList(0, 2);
    }

    /**
     * Breeds two parent chromosomes to create two child chromosomes using single-point crossover
     *
     * @param parent1 the first parent chromosome
     * @param parent2 the second parent chromosome
     * @return a list of the resulting child chromosomes
     */
    private List<Chromosome> crossover(Chromosome parent1, Chromosome parent2) {
        List<Chromosome> children = new ArrayList<>();
        Random rand = new Random();
        if (rand.nextDouble() < crossoverRate) {
            int crossoverPoint = rand.nextInt(parent1.getGenes().length());
            String child1Genes = parent1.getGenes().substring(0, crossoverPoint) + parent2.getGenes().substring(crossoverPoint);
            String child2Genes = parent2.getGenes().substring(0, crossoverPoint) + parent1.getGenes().substring(crossoverPoint);
            double child1Fitness = calculateFitness(child1Genes);
            double child2Fitness = calculateFitness(child2Genes);
            children.add(new Chromosome(child1Genes, child1Fitness));
            children.add(new Chromosome(child2Genes, child2Fitness));
        } else {
            children.add(parent1);
            children.add(parent2);
        }
        return children;
    }

    /**
     * Mutates a given chromosome by randomly flipping some of its genes
     *
     * @param chromosome the chromosome to mutate
     * @return the mutated chromosome
     */
    private Chromosome mutate(Chromosome chromosome) {
        StringBuilder sb = new StringBuilder(chromosome.getGenes());
        Random rand = new Random();
        for (int i = 0; i < sb.length(); i++) {
            if (rand.nextDouble() < mutationRate) {
                char gene = (char) (rand.nextInt(26) + 'a');
                sb.setCharAt(i, gene);
            }
        }
        double fitness = calculateFitness(sb.toString());
        return new Chromosome(sb.toString(), fitness);
    }

    /**
     * Evolves the population by selecting parents, breeding them, and mutating the resulting children
     */
    public void evolve() {
        List<Chromosome> newPopulation = new ArrayList<>();
        while (newPopulation.size() < populationSize) {
            List<Chromosome> parents = tournamentSelection();
            List<Chromosome> children = crossover(parents.get(0), parents.get(1));
            Chromosome child1 = mutate(children.get(0));
            Chromosome child2 = mutate(children.get(1));
            newPopulation.add(child1);
            newPopulation.add(child2);
        }
        Collections.sort(newPopulation, Collections.reverseOrder());
        population = newPopulation;
    }

    /**
     * Runs the genetic algorithm until a solution with the target fitness score is found or the maximum number of generations is reached
     *
     * @param maxGenerations the maximum number of generations to run the algorithm for
     * @return the best chromosome found during the evolution process
     */
    public Chromosome run(int maxGenerations) {
        int generation = 0;
        Chromosome bestChromosome = population.get(0);
        while (bestChromosome.getFitness() < targetFitness && generation < maxGenerations) {
            evolve();
            bestChromosome = population.get(0);
            System.out.println("Generation " + generation + ": " + bestChromosome);
            generation++;
        }
        return bestChromosome;
    }

    // a bunch of generated setters for easy initialization in the controller


    public void setMutationRate(double mutationRate) {
        this.mutationRate = mutationRate;
    }

    public void setCrossoverRate(double crossoverRate) {
        this.crossoverRate = crossoverRate;
    }

    public void setTargetFitness(double targetFitness) {
        this.targetFitness = targetFitness;
    }
}

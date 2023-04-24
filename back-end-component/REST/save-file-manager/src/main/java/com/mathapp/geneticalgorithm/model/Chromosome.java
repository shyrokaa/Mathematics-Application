package com.mathapp.geneticalgorithm.model;

public class Chromosome {
    // A string representation of the chromosome's genetic makeup
    private String genes;
    // A double value representing the chromosome's fitness score
    private double fitness;

    /**
     * Constructor for creating a new Chromosome object
     * @param genes a string representation of the chromosome's genetic makeup
     * @param fitness a double value representing the chromosome's fitness score
     */
    public Chromosome(String genes, double fitness) {
        this.genes = genes;
        this.fitness = fitness;
    }

    /**
     * Getter method for the genes property
     * @return a string representation of the chromosome's genetic makeup
     */
    public String getGenes() {
        return genes;
    }

    /**
     * Setter method for the genes property
     * @param genes a string representation of the chromosome's genetic makeup
     */
    public void setGenes(String genes) {
        this.genes = genes;
    }

    /**
     * Getter method for the fitness property
     * @return a double value representing the chromosome's fitness score
     */
    public double getFitness() {
        return fitness;
    }

    /**
     * Setter method for the fitness property
     * @param fitness a double value representing the chromosome's fitness score
     */
    public void setFitness(double fitness) {
        this.fitness = fitness;
    }

    /**
     * Override of the Object class's toString() method
     * Returns a string representation of the chromosome's properties
     */
    @Override
    public String toString() {
        return "Chromosome [genes=" + genes + ", fitness=" + fitness + "]";
    }
}

package com.mathapp.geneticalgorithm.service;

import com.mathapp.geneticalgorithm.model.Chromosome;
import com.mathapp.geneticalgorithm.model.GeneticAlgorithm;
import org.springframework.stereotype.Service;

@Service
public class GeneticAlgorithmService {

    private final GeneticAlgorithm geneticAlgorithm;

    public GeneticAlgorithmService(GeneticAlgorithm geneticAlgorithm) {
        this.geneticAlgorithm = geneticAlgorithm;
    }

    /**
     * Runs the genetic algorithm to find a solution with fitness greater than or equal to the target fitness.
     * @return The best solution found.
     */
    public String run() {
        // Call the run() method of the GeneticAlgorithm instance to perform the genetic algorithm
        Chromosome bestChromosome = geneticAlgorithm.run(1000);
        // Return the genes of the best chromosome found
        return bestChromosome.getGenes();
    }
}

package com.mathapp.geneticalgorithm.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.mathapp.geneticalgorithm.model.GeneticAlgorithm;
import com.mathapp.geneticalgorithm.model.Chromosome;

@RestController
public class GeneticAlgorithmController {

    @Autowired
    private GeneticAlgorithm geneticAlgorithm;

    @Value("${genetic.population-size}")
    private int populationSize;

    @Value("${genetic.target-fitness}")
    private double targetFitness;

    @GetMapping("/genetic-algorithm")
    public ResponseEntity<Chromosome> runGeneticAlgorithm(
            @RequestParam(required = true) String targetString,
            @RequestParam(required = false, defaultValue = "100") int maxGenerations,
            @RequestParam(required = false, defaultValue = "0.75") double crossoverRate,
            @RequestParam(required = false, defaultValue = "0.01") double mutationRate) {

        geneticAlgorithm.initializePopulation(populationSize);

        geneticAlgorithm.setTargetFitness(targetFitness);
        geneticAlgorithm.setCrossoverRate(crossoverRate);
        geneticAlgorithm.setMutationRate(mutationRate);

        Chromosome solution = geneticAlgorithm.run(maxGenerations);

        if (solution.getFitness() >= targetFitness) {
            return new ResponseEntity<>(solution, HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }
}

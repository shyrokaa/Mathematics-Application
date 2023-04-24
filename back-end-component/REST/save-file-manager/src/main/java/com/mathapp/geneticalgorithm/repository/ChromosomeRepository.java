package com.mathapp.geneticalgorithm.repository;

import com.mathapp.geneticalgorithm.model.Chromosome;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface ChromosomeRepository extends JpaRepository<Chromosome, Long> {

}

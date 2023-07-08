package com.mathapp.savemanager.Repository;

import com.mathapp.savemanager.Model.Request;
import org.bson.types.ObjectId;
import org.springframework.data.mongodb.repository.MongoRepository;

import java.util.List;


public interface RequestRepository extends MongoRepository<Request, String> {
    List<Request> findByOwner(ObjectId owner);
    // Add any additional methods you may need for querying or manipulating user data
}


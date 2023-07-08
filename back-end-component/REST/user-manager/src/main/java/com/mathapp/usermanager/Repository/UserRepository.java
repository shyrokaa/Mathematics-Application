package com.mathapp.usermanager.Repository;

import com.mathapp.usermanager.Model.User;
import org.springframework.data.mongodb.repository.MongoRepository;


public interface UserRepository extends MongoRepository<User, String> {
    User findByUsernameAndPassword(String username, String password);

    boolean existsByUsername(String username);

    User findByUsername(String username);
    // Add any additional methods you may need for querying or manipulating user data
}


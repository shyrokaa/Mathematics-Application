package com.mathapp.usermanager.Repository;

import com.mathapp.usermanager.Model.User;
import org.springframework.data.mongodb.repository.MongoRepository;

import java.util.List;

public interface UserRepository extends MongoRepository<User, String> {
    List<User> findByName(String name);
    User findByEmail(String email);
}

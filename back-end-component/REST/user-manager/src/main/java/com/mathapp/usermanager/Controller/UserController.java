package com.mathapp.usermanager.Controller;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.mathapp.usermanager.Model.User;
import com.mathapp.usermanager.Repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.util.List;
import java.util.Map;
import java.util.Optional;

@RestController
@RequestMapping("/users")
public class UserController {
    private final UserRepository userRepository;

    @Autowired
    public UserController(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @GetMapping
    public List<User> getAllUsers() {
        return userRepository.findAll();
    }

    @GetMapping("/{id}")
    public ResponseEntity<User> getUserById(@PathVariable("id") String id) {
        Optional<User> userOptional = userRepository.findById(id);
        return userOptional.map(user -> new ResponseEntity<>(user, HttpStatus.OK))
                .orElseGet(() -> new ResponseEntity<>(HttpStatus.NOT_FOUND));
    }

    @PostMapping
    public User createUser(@RequestBody User user) {
        return userRepository.save(user);
    }

    @PutMapping("/{id}")
    public ResponseEntity<User> updateUser(@PathVariable("id") String id, @RequestBody User user) {
        Optional<User> userOptional = userRepository.findById(id);
        if (userOptional.isPresent()) {
            User existingUser = userOptional.get();
            existingUser.setUsername(user.getUsername());
            existingUser.setPassword(user.getPassword());
            return new ResponseEntity<>(userRepository.save(existingUser), HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<HttpStatus> deleteUser(@PathVariable("id") String id) {
        try {
            userRepository.deleteById(id);
            return new ResponseEntity<>(HttpStatus.NO_CONTENT);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @PostMapping("/login")
    public ResponseEntity<User> loginUser(@RequestBody String userJson) {

        try {
            // Create an instance of ObjectMapper
            ObjectMapper objectMapper = new ObjectMapper();

            // Deserialize the request body JSON to a Map
            Map<String, String> userMap = objectMapper.readValue(userJson, new TypeReference<Map<String, String>>() {});
            System.out.println(userMap);
            // Extract the username and password from the Map
            String username = userMap.get("Username");
            String password = userMap.get("Password");
            System.out.println("Login request received" + username + " " + password);
            User existingUser = userRepository.findByUsernameAndPassword(username, password);
            if (existingUser != null) {
                System.out.println("This user exists");
                return new ResponseEntity<>(existingUser, HttpStatus.OK);
            } else {
                System.out.println("This user does not exist");
                return new ResponseEntity<>(HttpStatus.UNAUTHORIZED);
            }
        } catch (Exception e) {
            // Handle any exceptions during deserialization
            e.printStackTrace();
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @PostMapping("/signup")
    public ResponseEntity<User> signupUser(@RequestBody String userJson) {
        try {
            // Create an instance of ObjectMapper
            ObjectMapper objectMapper = new ObjectMapper();

            // Deserialize the request body JSON to a Map
            Map<String, String> userMap = objectMapper.readValue(userJson, new TypeReference<Map<String, String>>() {});
            String username = userMap.get("Username");
            String password = userMap.get("Password");

            // Check if the username already exists
            if (userRepository.existsByUsername(username)) {
                return new ResponseEntity<>(HttpStatus.CONFLICT); // Username already taken
            }

            // Create a new User object
            User newUser = new User();
            newUser.setUsername(username);
            newUser.setPassword(password);

            // Save the new user
            User createdUser = userRepository.save(newUser);
            return new ResponseEntity<>(createdUser, HttpStatus.CREATED);
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @PostMapping("/get_id")
    public ResponseEntity<String> getUserId(@RequestBody String userJson) {
        try {
            // Create an instance of ObjectMapper
            ObjectMapper objectMapper = new ObjectMapper();

            // Deserialize the request body JSON to a Map
            Map<String, String> userMap = objectMapper.readValue(userJson, new TypeReference<Map<String, String>>() {});
            String username = userMap.get("Username");
            System.out.println("got the id");
            // Find the user by username
            User user = userRepository.findByUsername(username);

            if (user != null) {
                String userId = user.getId(); // Assuming the User class has an `getId()` method
                return new ResponseEntity<>(userId, HttpStatus.OK);
            } else {
                return new ResponseEntity<>(HttpStatus.NOT_FOUND);
            }
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }



}


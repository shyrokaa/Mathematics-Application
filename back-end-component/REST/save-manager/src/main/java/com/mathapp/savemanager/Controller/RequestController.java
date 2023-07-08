package com.mathapp.savemanager.Controller;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.mathapp.savemanager.Repository.RequestRepository;
import com.mathapp.savemanager.Model.Request;
import org.bson.types.ObjectId;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.bson.types.ObjectId;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/requests")
public class RequestController {
    private final RequestRepository requestRepository;

    @Autowired
    public RequestController(RequestRepository requestRepository) {
        this.requestRepository = requestRepository;
    }

    @GetMapping
    public List<Request> getAllRequests() {
        return requestRepository.findAll();
    }

    @GetMapping("/{id}")
    public Request getRequestById(@PathVariable("id") String id) {
        return requestRepository.findById(id).orElse(null);
    }

    @PostMapping
    public Request createRequest(@RequestBody Request request) {
        return requestRepository.save(request);
    }

    @PutMapping("/{id}")
    public Request updateRequest(@PathVariable("id") String id, @RequestBody Request request) {
        request.setId(id);
        return requestRepository.save(request);
    }

    @DeleteMapping("/{id}")
    public void deleteRequest(@PathVariable("id") String id) {
        requestRepository.deleteById(id);
    }

    // saving a new request
    @PostMapping("/save")
    public ResponseEntity<Request> createRequest(@RequestBody String requestJson) {
        try {
            // Create an instance of ObjectMapper
            ObjectMapper objectMapper = new ObjectMapper();

            // Deserialize the request body JSON to a Map
            Map<String, Object> requestMap = objectMapper.readValue(requestJson, new TypeReference<Map<String, Object>>() {});

            // Extract the components from the Map
            String ownerId = (String) requestMap.get("Owner");
            ObjectId owner = new ObjectId(ownerId);
            String requestType = (String) requestMap.get("RequestType");
            String requestBody = (String) requestMap.get("RequestBody");

            // Create a new Request object
            Request request = new Request();
            request.setOwner(owner);
            request.setRequestType(requestType);
            request.setRequestBody(requestBody);

            // Save the request
            Request createdRequest = requestRepository.save(request);
            return new ResponseEntity<>(createdRequest, HttpStatus.CREATED);
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }


    // getting all the requests associated with a user
    @PostMapping("/get_all_requests")
    public ResponseEntity<List<Map<String, String>>> getAllRequests(@RequestBody String ownerIdJson) {
        try {
            // Create an instance of ObjectMapper
            ObjectMapper objectMapper = new ObjectMapper();

            // Deserialize the request body JSON to a Map
            Map<String, Object> ownerIdMap = objectMapper.readValue(ownerIdJson, new TypeReference<Map<String, Object>>() {});

            // Extract the owner ID from the Map
            String ownerId = (String) ownerIdMap.get("Owner");
            ObjectId owner = new ObjectId(ownerId);

            // Retrieve all requests for the specified owner
            List<Request> requests = requestRepository.findByOwner(owner);

            // Create a new list to store the requestType and requestBody
            List<Map<String, String>> simplifiedRequests = new ArrayList<>();

            // Iterate through each request and extract the requestType and requestBody
            for (Request request : requests) {
                Map<String, String> simplifiedRequest = new HashMap<>();
                simplifiedRequest.put("requestType", request.getRequestType());
                simplifiedRequest.put("requestBody", request.getRequestBody());
                simplifiedRequests.add(simplifiedRequest);
            }

            // Return the list of simplified requests
            return new ResponseEntity<>(simplifiedRequests, HttpStatus.OK);
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

}

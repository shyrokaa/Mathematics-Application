package com.mathapp.savemanager.Model;

import org.bson.types.ObjectId;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

import java.util.Objects;

@Document(collection = "Request")
public class Request {
    @Id
    private String id;
    private ObjectId owner;
    private String requestType;
    private String requestBody;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public ObjectId getOwner() {
        return owner;
    }

    public void setOwner(ObjectId owner) {
        this.owner = owner;
    }

    public String getRequestType() {
        return requestType;
    }

    public void setRequestType(String requestType) {
        this.requestType = requestType;
    }

    public String getRequestBody() {
        return requestBody;
    }

    public void setRequestBody(String requestBody) {
        this.requestBody = requestBody;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Request request = (Request) o;
        return Objects.equals(id, request.id) && Objects.equals(owner, request.owner) && Objects.equals(requestType, request.requestType) && Objects.equals(requestBody, request.requestBody);
    }

    @Override
    public int hashCode() {
        return Objects.hash(id, owner, requestType, requestBody);
    }


    @Override
    public String toString() {
        return "Request{" +
                "id='" + id + '\'' +
                ", owner=" + owner +
                ", requestType='" + requestType + '\'' +
                ", requestBody='" + requestBody + '\'' +
                '}';
    }
}

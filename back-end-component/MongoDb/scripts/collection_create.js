


db.createCollection("User", {
  validator: {
    $jsonSchema: {
      bsonType: "object",
      required: ["username", "password"],
      properties: {
        _id: {
          bsonType: "objectId"
        },
        username: {
          bsonType: "string"
        },
        password: {
          bsonType: "string"
        }
      }
    }
  }
})

db.User.createIndex({ "username": 1 }, { unique: true })


db.createCollection("Request", {
  validator: {
    $jsonSchema: {
      bsonType: "object",
      required: ["owner", "requestType", "requestBody"],
      properties: {
        _id: {
          bsonType: "objectId"
        },
        owner: {
          bsonType: "objectId"
        },
        requestType: {
          bsonType: "string"
        },
        requestBody: {
          bsonType: "string"
        }
      }
    }
  }
})


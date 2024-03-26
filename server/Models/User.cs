﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("username")]
    public string? Username { get; set; }

    [BsonElement("email")]
    public string? Email { get; set; }

    [BsonElement("password")]
    public string? Password { get; set; }

    [BsonElement("admin")]
    public bool Admin { get; set; }

    [BsonElement("__v")]
    public int? Version { get; set; } 

}

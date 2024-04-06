using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("user")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? User { get; set; }

    [BsonElement("products")]
    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? ProductsArray { get; set; }

    public List<Product> Products { get; set; }

    [BsonElement("orderDate")]
    public DateTime OrderDate { get; set; }

    [BsonElement("__v")]
    public int? v { get; set; }
}

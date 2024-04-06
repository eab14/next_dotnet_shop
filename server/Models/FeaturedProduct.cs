using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models;

public class FeaturedProduct
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("product")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ProductId { get; set; }

    public Product Product { get; set; }

    [BsonElement("image")]
    public string Image { get; set; } = null!;

    [BsonElement("status")]
    public string Status { get; set; } = null!;

    [BsonElement("description")]
    public string Description { get; set; } = null!;

    [BsonElement("__v")]
    public int? v { get; set; }

}

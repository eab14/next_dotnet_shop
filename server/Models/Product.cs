using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace server.Models;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("title")]
    public string Title { get; set; } = null!;

    [BsonElement("price")]
    public decimal Price { get; set; }

    [BsonElement("featured")]
    public Boolean Featured { get; set; }

    [BsonElement("platforms")]
    public List<string> Platforms { get; set; } = null!;

    [BsonElement("images")]
    public List<string> Images { get; set; } = null!;

    [BsonElement("releaseDate")]
    public DateTime ReleaseDate { get; set; }

    [BsonElement("__v")]
    public int? v { get; set; }

}

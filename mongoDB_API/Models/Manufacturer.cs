using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mongoDB_API.Models;

public class Manufacturer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonElement("address")]
    public string Address { get; set; } = null!;

    [BsonElement("country")]
    public string Country { get; set; } = null!;
}
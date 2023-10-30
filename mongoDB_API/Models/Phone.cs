namespace mongoDB_API.Models;

using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Phone
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("model")]
    public string Model { get; set; } = null!;

    [BsonElement("manufacturer")]
    public Manufacturer Manufacturer { get; set; } = null!;

    [BsonElement("user")]
    public User User { get; set; } = null!;
}
namespace mongoDB_API.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Phone : BaseEntity
{
    [BsonElement("model")]
    public string Model { get; set; } = null!;

    [BsonElement("manufacturer")]
    public Manufacturer Manufacturer { get; set; } = null!;

    [BsonElement("user")]
    public User User { get; set; }
    
}
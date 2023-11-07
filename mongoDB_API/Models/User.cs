using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mongoDB_API.Models;

public class User : BaseEntity
{
    [BsonElement("index")]
    public int Index { get; set; }
    
    [BsonElement("firstname")]
    public string Firstname { get; set; } = null!;

    [BsonElement("secondname")]
    public string SecondName { get; set; } = null!;

    [BsonElement("age")]
    public int Age { get; set; }
    
    [BsonElement("country")]
    public string Country { get; set; } = null!;
    
    [BsonElement("address")]
    public string Address { get; set; } = null!;
}
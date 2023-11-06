

using mongoDB_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace mongoDB_API.Services;

public class PhonesService
{
    private readonly IMongoCollection<Phone> _phonesCollection;
    
    public PhonesService(
        IOptions<PhoneStoreDatabaseSettings> phonesStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            phonesStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            phonesStoreDatabaseSettings.Value.DatabaseName);

        _phonesCollection = mongoDatabase.GetCollection<Phone>(
            phonesStoreDatabaseSettings.Value.PhonesCollectionName);
        
        
    }

    public async Task<List<Phone>> GetAsync() =>
        await _phonesCollection.Find(_ => true).ToListAsync();

    public async Task<Phone?> GetAsync(string id) =>
        await _phonesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Phone newPhone) =>
        await _phonesCollection.InsertOneAsync(newPhone);

    public async Task UpdateAsync(string id, Phone updatedPhone) =>
        await _phonesCollection.ReplaceOneAsync(x => x.Id == id, updatedPhone);

    public async Task RemoveAsync(string id) =>
        await _phonesCollection.DeleteOneAsync(x => x.Id == id);
    
    public async Task<List<AggregatedPhone>> AggregatePhones()
    {
        var aggregationPipeline = new List<BsonDocument>
        {
            BsonDocument.Parse("{$match: {'manufacturer.name': {$exists: true}}}"),
            BsonDocument.Parse("{$group: {_id: '$manufacturer.name', total: {$sum: 1}}}")
        };

        var result = await _phonesCollection.Aggregate<BsonDocument>(aggregationPipeline).ToListAsync();

        var aggregatedPhones = result.Select(bson => new AggregatedPhone
        {
            Manufacturer = bson["_id"].AsString,
            Total = bson["total"].AsInt32
        }).ToList();

        return aggregatedPhones;
    }




}

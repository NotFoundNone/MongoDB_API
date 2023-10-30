using Microsoft.Extensions.Options;
using mongoDB_API.Models;
using MongoDB.Driver;

namespace mongoDB_API.Services;

public class ManufacturersService
{
    private readonly IMongoCollection<Manufacturer> _manufacturersCollection;

    public ManufacturersService(IOptions<PhoneStoreDatabaseSettings> phoneStoreDataBaseSettings)
    {
        var mongoClient = new MongoClient(
            phoneStoreDataBaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(phoneStoreDataBaseSettings.Value.DatabaseName);

        _manufacturersCollection = mongoDatabase.GetCollection<Manufacturer>(phoneStoreDataBaseSettings.Value.ManufacturersCollectionName);
    }

    public async Task<List<Manufacturer>> GetAsync() => await _manufacturersCollection.Find(_ => true).ToListAsync();

    public async Task<Manufacturer?> GetAsync(string id) =>
        await _manufacturersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Manufacturer newManufacturer) =>
        await _manufacturersCollection.InsertOneAsync(newManufacturer);

    public async Task UpdateAsync(string id, Manufacturer updatedManufacturer) =>
        await _manufacturersCollection.ReplaceOneAsync(x => x.Id == id, updatedManufacturer);

    public async Task RemoveAsync(string id) => await _manufacturersCollection.DeleteOneAsync(x => x.Id == id);
}
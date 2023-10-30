

using mongoDB_API.Models;
using Microsoft.Extensions.Options;
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
    
    
}

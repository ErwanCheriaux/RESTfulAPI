using MongoDB.Bson;
using MongoDB.Driver;
using MountainBike.Services.Entities;

namespace MountainBike.Services.Repositories;

public class MongoDBRiderRepository : IRiderRepository
{
    private const string DatabaseName = "mountainbike";
    private const string RidersCollectionName = "riders";
    private readonly IMongoCollection<RiderEntity> _ridersCollection;
    private readonly FilterDefinitionBuilder<RiderEntity> _ridersFilterBuilder = Builders<RiderEntity>.Filter;

    public MongoDBRiderRepository(IMongoClient mongoClient)
    {
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(DatabaseName);
        _ridersCollection = mongoDatabase.GetCollection<RiderEntity>(RidersCollectionName);
    }

    public async Task CreateRiderAsync(RiderEntity rider)
    {
        await _ridersCollection.InsertOneAsync(rider);
    }

    public async Task DeleteRiderAsync(Guid id)
    {
        var filter = _ridersFilterBuilder.Eq(rider => rider.Id, id);
        await _ridersCollection.DeleteOneAsync(filter);
    }

    public async Task<RiderEntity> GetRiderAsync(Guid id)
    {
        var filter = _ridersFilterBuilder.Eq(rider => rider.Id, id);
        return await _ridersCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<RiderEntity>> GetRidersAsync()
    {
        return await _ridersCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateRiderAsync(RiderEntity rider)
    {
        var filter = _ridersFilterBuilder.Eq(existingRider => existingRider.Id, rider.Id);
        await _ridersCollection.ReplaceOneAsync(filter, rider);
    }
}
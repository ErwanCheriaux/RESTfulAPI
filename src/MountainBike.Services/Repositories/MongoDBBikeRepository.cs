using MongoDB.Bson;
using MongoDB.Driver;
using MountainBike.Services.Entities;

namespace MountainBike.Services.Repositories;

public class MongoDBBikeRepository : IBikeRepository
{
    private const string DatabaseName = "mountainbike";
    private const string BikesCollectionName = "bikes";
    private readonly IMongoCollection<BikeEntity> _bikesCollection;
    private readonly FilterDefinitionBuilder<BikeEntity> _bikesFilterBuilder = Builders<BikeEntity>.Filter;

    public MongoDBBikeRepository(IMongoClient mongoClient)
    {
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(DatabaseName);
        _bikesCollection = mongoDatabase.GetCollection<BikeEntity>(BikesCollectionName);
    }

    public async Task CreateBikeAsync(BikeEntity bike)
    {
        await _bikesCollection.InsertOneAsync(bike);
    }

    public async Task DeleteBikeAsync(Guid id)
    {
        var filter = _bikesFilterBuilder.Eq(bike => bike.Id, id);
        await _bikesCollection.DeleteOneAsync(filter);
    }

    public async Task<BikeEntity> GetBikeAsync(Guid id)
    {
        var filter = _bikesFilterBuilder.Eq(bike => bike.Id, id);
        return await _bikesCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<BikeEntity>> GetBikesAsync()
    {
        return await _bikesCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateBikeAsync(BikeEntity bike)
    {
        var filter = _bikesFilterBuilder.Eq(existingBike => existingBike.Id, bike.Id);
        await _bikesCollection.ReplaceOneAsync(filter, bike);
    }
}
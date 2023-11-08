using MongoDB.Bson;
using MongoDB.Driver;
using MountainBike.Api.Models;

namespace MountainBike.Api.DataAccess;

public class MongoDBGarage : IGarage
{
    private const string DatabaseName = "mountainbike";
    private const string CollectionName = "bikes";
    private readonly IMongoCollection<Bike> _bikesCollection;
    private readonly FilterDefinitionBuilder<Bike> _filterBuilder = Builders<Bike>.Filter;

    public MongoDBGarage(IMongoClient mongoClient)
    {
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(DatabaseName);
        _bikesCollection = mongoDatabase.GetCollection<Bike>(CollectionName);
    }

    public void CreateBike(Bike bike)
    {
        _bikesCollection.InsertOne(bike);
    }

    public async Task CreateBikeAsync(Bike bike)
    {
        await _bikesCollection.InsertOneAsync(bike);
    }

    public Task CreateRiderAsync(Rider rider)
    {
        throw new NotImplementedException();
    }

    public void DeleteBike(Guid id)
    {
        var filter = _filterBuilder.Eq(bike => bike.Id, id);
        _bikesCollection.DeleteOne(filter);
    }

    public async Task DeleteBikeAsync(Guid id)
    {
        var filter = _filterBuilder.Eq(bike => bike.Id, id);
        await _bikesCollection.DeleteOneAsync(filter);
    }

    public Task DeleteRiderAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Bike? GetBike(Guid id)
    {
        var filter = _filterBuilder.Eq(bike => bike.Id, id);
        return _bikesCollection.Find(filter).SingleOrDefault();
    }

    public async Task<Bike> GetBikeAsync(Guid id)
    {
        var filter = _filterBuilder.Eq(bike => bike.Id, id);
        return await _bikesCollection.Find(filter).SingleOrDefaultAsync();
    }

    public IEnumerable<Bike> GetBikes()
    {
        return _bikesCollection.Find(new BsonDocument()).ToList();
    }

    public async Task<IEnumerable<Bike>> GetBikesAsync()
    {
        return await _bikesCollection.Find(new BsonDocument()).ToListAsync();
    }

    public Task<Rider> GetRiderAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Rider>> GetRidersAsync()
    {
        throw new NotImplementedException();
    }

    public void UpdateBike(Bike bike)
    {
        var filter = _filterBuilder.Eq(existingBike => existingBike.Id, bike.Id);
        _bikesCollection.ReplaceOne(filter, bike);
    }

    public async Task UpdateBikeAsync(Bike bike)
    {
        var filter = _filterBuilder.Eq(existingBike => existingBike.Id, bike.Id);
        await _bikesCollection.ReplaceOneAsync(filter, bike);
    }

    public Task UpdateRiderAsync(Rider rider)
    {
        throw new NotImplementedException();
    }
}
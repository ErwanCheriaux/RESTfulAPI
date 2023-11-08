using MongoDB.Bson;
using MongoDB.Driver;
using MountainBike.Api.Models;

namespace MountainBike.Api.DataAccess;

public class MongoDBGarage : IGarage
{
    private const string DatabaseName = "mountainbike";

    private const string BikesCollectionName = "bikes";
    private readonly IMongoCollection<Bike> _bikesCollection;
    private readonly FilterDefinitionBuilder<Bike> _bikesFilterBuilder = Builders<Bike>.Filter;

    private const string RidersCollectionName = "riders";
    private readonly IMongoCollection<Rider> _ridersCollection;
    private readonly FilterDefinitionBuilder<Rider> _ridersFilterBuilder = Builders<Rider>.Filter;

    public MongoDBGarage(IMongoClient mongoClient)
    {
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(DatabaseName);
        _bikesCollection = mongoDatabase.GetCollection<Bike>(BikesCollectionName);
        _ridersCollection = mongoDatabase.GetCollection<Rider>(RidersCollectionName);
    }

    public void CreateBike(Bike bike)
    {
        _bikesCollection.InsertOne(bike);
    }

    public async Task CreateBikeAsync(Bike bike)
    {
        await _bikesCollection.InsertOneAsync(bike);
    }

    public async Task CreateRiderAsync(Rider rider)
    {
        await _ridersCollection.InsertOneAsync(rider);
    }

    public void DeleteBike(Guid id)
    {
        var filter = _bikesFilterBuilder.Eq(bike => bike.Id, id);
        _bikesCollection.DeleteOne(filter);
    }

    public async Task DeleteBikeAsync(Guid id)
    {
        var filter = _bikesFilterBuilder.Eq(bike => bike.Id, id);
        await _bikesCollection.DeleteOneAsync(filter);
    }

    public async Task DeleteRiderAsync(Guid id)
    {
        var filter = _ridersFilterBuilder.Eq(rider => rider.Id, id);
        await _ridersCollection.DeleteOneAsync(filter);
    }

    public Bike? GetBike(Guid id)
    {
        var filter = _bikesFilterBuilder.Eq(bike => bike.Id, id);
        return _bikesCollection.Find(filter).SingleOrDefault();
    }

    public async Task<Bike> GetBikeAsync(Guid id)
    {
        var filter = _bikesFilterBuilder.Eq(bike => bike.Id, id);
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

    public async Task<Rider> GetRiderAsync(Guid id)
    {
        var filter = _ridersFilterBuilder.Eq(rider => rider.Id, id);
        return await _ridersCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Rider>> GetRidersAsync()
    {
        return await _ridersCollection.Find(new BsonDocument()).ToListAsync();
    }

    public void UpdateBike(Bike bike)
    {
        var filter = _bikesFilterBuilder.Eq(existingBike => existingBike.Id, bike.Id);
        _bikesCollection.ReplaceOne(filter, bike);
    }

    public async Task UpdateBikeAsync(Bike bike)
    {
        var filter = _bikesFilterBuilder.Eq(existingBike => existingBike.Id, bike.Id);
        await _bikesCollection.ReplaceOneAsync(filter, bike);
    }

    public async Task UpdateRiderAsync(Rider rider)
    {
        var filter = _ridersFilterBuilder.Eq(existingRider => existingRider.Id, rider.Id);
        await _ridersCollection.ReplaceOneAsync(filter, rider);
    }
}
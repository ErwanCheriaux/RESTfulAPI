using MongoDB.Bson;
using MongoDB.Driver;
using RESTfulAPI.Models;

namespace RESTfulAPI.DataAccess;

public class MongoDBGarage : IGarage
{
    private const string DatabaseName = "restfulapi";
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

    public void DeleteBike(Guid id)
    {
        var filter = _filterBuilder.Eq(bike => bike.Id, id);
        _bikesCollection.DeleteOne(filter);
    }

    public Bike? GetBike(Guid id)
    {
        var filter = _filterBuilder.Eq(bike => bike.Id, id);
        return _bikesCollection.Find(filter).SingleOrDefault();
    }

    public IEnumerable<Bike> GetBikes()
    {
        return _bikesCollection.Find(new BsonDocument()).ToList();
    }

    public void UpdateBike(Bike bike)
    {
        var filter = _filterBuilder.Eq(existingBike => existingBike.Id, bike.Id);
        _bikesCollection.ReplaceOne(filter, bike);
    }
}
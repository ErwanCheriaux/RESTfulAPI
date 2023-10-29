using MongoDB.Driver;
using RESTfulAPI.Models;

namespace RESTfulAPI.DataAccess;

public class MongoDBGarage : IGarage
{
    private const string DatabaseName = "restfulapi";
    private const string CollectionName = "bikes";
    private readonly IMongoCollection<Bike> _bikesCollection;

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
        throw new NotImplementedException();
    }

    public Bike? GetBike(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Bike> GetBikes()
    {
        throw new NotImplementedException();
    }

    public void UpdateBike(Bike bike)
    {
        throw new NotImplementedException();
    }
}
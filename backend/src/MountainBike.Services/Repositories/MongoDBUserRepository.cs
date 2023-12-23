using MongoDB.Bson;
using MongoDB.Driver;
using MountainBike.Services.Entities;

namespace MountainBike.Services.Repositories;

public class MongoDBUserRepository : IUserRepository
{
    private const string DatabaseName = "mountainbike";
    private const string UsersCollectionName = "users";
    private readonly IMongoCollection<UserEntity> _usersCollection;
    private readonly FilterDefinitionBuilder<UserEntity> _usersFilterBuilder = Builders<UserEntity>.Filter;

    public MongoDBUserRepository(IMongoClient mongoClient)
    {
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(DatabaseName);
        _usersCollection = mongoDatabase.GetCollection<UserEntity>(UsersCollectionName);
    }

    public async Task CreateUserAsync(UserEntity user)
    {
        await _usersCollection.InsertOneAsync(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var filter = _usersFilterBuilder.Eq(user => user.Id, id);
        await _usersCollection.DeleteOneAsync(filter);
    }

    public async Task<UserEntity> GetUserAsync(Guid id)
    {
        var filter = _usersFilterBuilder.Eq(user => user.Id, id);
        return await _usersCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<UserEntity> GetUserAsync(string email)
    {
        var filter = _usersFilterBuilder.Eq(user => user.Email, email);
        return await _usersCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<UserEntity>> GetUsersAsync()
    {
        return await _usersCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateUserAsync(UserEntity user)
    {
        var filter = _usersFilterBuilder.Eq(existingUser => existingUser.Id, user.Id);
        await _usersCollection.ReplaceOneAsync(filter, user);
    }
}
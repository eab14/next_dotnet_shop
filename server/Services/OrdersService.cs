using server.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace server.Services;

public class OrdersService
{
    private readonly IMongoCollection<Order> _ordersCollection;

    public OrdersService(
        IOptions<NextNetDatabaseSettings> nextNetDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            nextNetDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            nextNetDatabaseSettings.Value.DatabaseName);

        _ordersCollection = mongoDatabase.GetCollection<Order>(
            nextNetDatabaseSettings.Value.OrdersCollectionName);
    }

    public async Task<List<Order>> GetAsync() =>
        await _ordersCollection.Find(_ => true).ToListAsync();

    public async Task<Order?> GetAsync(string id) =>
        await _ordersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Order newOrder) =>
        await _ordersCollection.InsertOneAsync(newOrder);

    public async Task UpdateAsync(string id, Order updatedOrder) =>
        await _ordersCollection.ReplaceOneAsync(x => x.Id == id, updatedOrder);

    public async Task RemoveAsync(string id) =>
        await _ordersCollection.DeleteOneAsync(x => x.Id == id);
}
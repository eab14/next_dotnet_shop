using server.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace server.Services;

public class FeaturedProductsService
{
    private readonly IMongoCollection<FeaturedProduct> _featuredProductsCollection;

    public FeaturedProductsService(
        IOptions<NextNetDatabaseSettings> nextNetDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            nextNetDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            nextNetDatabaseSettings.Value.DatabaseName);

        _featuredProductsCollection = mongoDatabase.GetCollection<FeaturedProduct>(
            nextNetDatabaseSettings.Value.FeaturedProductsCollectionName);
    }

    public async Task<List<FeaturedProduct>> GetAsync() =>
        await _featuredProductsCollection.Find(_ => true).ToListAsync();

    public async Task<FeaturedProduct?> GetAsync(string id) =>
        await _featuredProductsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(FeaturedProduct newFeaturedProduct) =>
        await _featuredProductsCollection.InsertOneAsync(newFeaturedProduct);

    public async Task UpdateAsync(string id, FeaturedProduct updatedFeaturedProduct) =>
        await _featuredProductsCollection.ReplaceOneAsync(x => x.Id == id, updatedFeaturedProduct);

    public async Task RemoveAsync(string id) =>
        await _featuredProductsCollection.DeleteOneAsync(x => x.Id == id);
}
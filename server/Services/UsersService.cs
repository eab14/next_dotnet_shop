using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using server.Models;
using BCrypt.Net;

namespace server.Services;

public class UsersService
{
    private readonly IMongoCollection<User> _usersCollection;

    public UsersService(
        IOptions<NextNetDatabaseSettings> nextNetDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            nextNetDatabaseSettings.Value.ConnectionString);    

        var mongoDatabase = mongoClient.GetDatabase(
            nextNetDatabaseSettings.Value.DatabaseName);

        _usersCollection = mongoDatabase.GetCollection<User>(
            nextNetDatabaseSettings.Value.UsersCollectionName);
    }

    public async Task<List<User>> GetAsync() =>
        await _usersCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string id) =>
        await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<User> GetUserByEmailAsync(string email)  => 
        await _usersCollection.Find(user => user.Email == email).FirstOrDefaultAsync();
 

    public async Task CreateAsync(User newUser)
    {

        newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
        await _usersCollection.InsertOneAsync(newUser);

    }

    public async Task UpdateAsync(string id, User updatedUser) =>
        await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task RemoveAsync(string id) =>
        await _usersCollection.DeleteOneAsync(x => x.Id == id);
        
    public bool VerifyPassword(string password, string hashedPassword) => 
        BCrypt.Net.BCrypt.Verify(password, hashedPassword);

    public async Task SeedDatabase()
    {
        try
        {
            if (await _usersCollection.CountDocumentsAsync(_ => true) == 0)
            {
                var jsonString = await File.ReadAllTextAsync("./Data/users.json");
                var users = JsonSerializer.Deserialize<List<User>>(jsonString);

                if (users != null)
                {

                    foreach (var user in users)
                        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                    await _usersCollection.InsertManyAsync(users);
                    Console.WriteLine("\n[ SEEDING ] : Seeding successful...\n");

                }

            }
            else
            {
                Console.WriteLine("\n[ SEEDING ] : Database contains USER seeds already...\n");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[ SEEDING ] : Data seeding failed: {ex.Message}\n");
        }
    }
        
}

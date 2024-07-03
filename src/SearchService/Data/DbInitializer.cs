using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Data;

/// <summary>
/// Utility class for initializing the MongoDB database and seeding initial data if needed.
/// </summary>
public class DbInitializer
{
    /// <summary>
    /// Initializes the MongoDB database by establishing connection, creating text indexes, and seeding initial data if the database is empty.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance used to access configuration settings and file system.</param>
    public static async Task InitDb(WebApplication app)
    {
        // All functionalities provided by MongoDB entity are all static classes. Therefore object instantiation is not required.

        // Initialize the MongoDB database connection using the connection string from the configuration and the specified settings.
        await DB.InitAsync("SearchDb", MongoClientSettings
            .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

        // Create text indexes for the "Make", "Model", and "Color" fields in the "Item" collection to enable efficient text search.
        await DB.Index<Item>()
            .Key(x => x.Make, KeyType.Text)
            .Key(x => x.Model, KeyType.Text)
            .Key(x => x.Color, KeyType.Text)
            .CreateAsync();

        var count = await DB.CountAsync<Item>();

        if (count == 0)
        {
            Console.WriteLine("No data in the database, will attempt to seed");
            // Read the content of the "auctions.json" file asynchronously and store it in the variable "itemData".
            var itemData = await File.ReadAllTextAsync("Data/auctions.json");
            // Create a new instance of JsonSerializerOptions with PropertyNameCaseInsensitive set to true.
            // This option ensures that property names are case-insensitive during JSON serialization and deserialization.
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            // Deserialize the JSON string (itemData) into a list of Item objects using the specified JsonSerializerOptions.
            // The options variable ensures that the property names are treated as case-insensitive during deserialization.
            var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);
            // Save the list of items (items) asynchronously into the database.
            await DB.SaveAsync(items);
        }
    }
}
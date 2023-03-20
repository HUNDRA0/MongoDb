using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDb.Data;

public class DbManager
{
	private readonly IMongoClient _client;
	private readonly IMongoDatabase _database;
	private readonly IMongoCollection<BsonDocument> _collection;
	//private readonly IMongoCollection<Item> _collection;

	public DbManager(string connection, string dbName, string collectionName)
	{ 
		_client = new MongoClient(connection);
		_database = _client.GetDatabase(dbName);
        _collection = _database.GetCollection<BsonDocument>(collectionName);
        //_collection = _database.GetCollection<Item>(collectionName);
    }


	// (C)reate
	public async Task<bool> CreateOneAsync(Item item)
	{
		try
		{
			var bson = item.ToBsonDocument();
			await _collection.InsertOneAsync(bson);
			return true;
        }
		catch (Exception error)
		{
			Console.WriteLine("\tAn error occured while creating data:\n" +
				"\t" + error.Message);
			return false;
		}
	}

	// (R)ead
	public async Task<String> ReadOneAsync(string field, string name)
	{
		FilterDefinition<BsonDocument> filter;

		if (field == "_id")
		{
            var idDefiner = new ObjectId(name);
            filter = Builders<BsonDocument>.Filter.Eq("_id", idDefiner);
        }
		else
		{
            filter = Builders<BsonDocument>.Filter.Eq(field, name);
        }

        var rawResult = await _collection.Find(filter).FirstOrDefaultAsync();
        if (rawResult == null) { return null; }

        string result = rawResult.ToString();
        return result;
    }

	// (R)ead all
	public async Task<List<string>> GetAllAsync()
	{
		var entries = await _collection.FindAsync(new BsonDocument());
		var stringed = await entries.ToListAsync();

		return stringed.Select(e => e.ToString()).ToList();
	}

	// (U)pdate
	public async Task<bool> UpdateAsync(Item item, string id)
	{
		var itemId = new ObjectId(id);
        var filter = Builders<BsonDocument>.Filter.Eq("_id", itemId);
        //var filter = Builders<Item>.Filter.Eq("Id", id);
        var result = await _collection.FindAsync(filter);

		if (result == null)
		{
			Console.WriteLine("\tEntry not found.\n" +
				"\tHave you double checked the ID of the item you're looking to update?");
			return false;
		}
		var bson = item.ToBsonDocument();

		var updateResult = await _collection.ReplaceOneAsync(filter, bson);
		return updateResult.ModifiedCount == 1;
	}

	// (D)elete
	public async Task<bool> DeleteAsync(string id)
	{

		var itemId = new ObjectId(id);
        var filter = Builders<BsonDocument>.Filter.Eq("_id", itemId);
        //var filter = Builders<Item>.Filter.Eq("Id", id);
        var checkEntry = await _collection.FindAsync(filter);
		
		if (checkEntry == null)
		{
			Console.WriteLine("\tThe entry specified could not be found.\n" +
				"Please check the ID that you provided and try again!");
			return false;
		}

		var result = _collection.DeleteOneAsync(filter);
		return result.Result.DeletedCount == 1;
	}
}


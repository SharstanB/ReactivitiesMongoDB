using Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Persistence.Helpers;
using Persistence.Services.DBServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.MongoDbRepo
{
    public class MongoDbRepository<T> : IMongoDbRepository<T> where T : class, IBaseEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoDbRepository(IOptions<DatabaseSettings> DatabaseSettings)
        {
            var mongoClient = new MongoClient(
                DatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DatabaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<T>(typeof(T).Name);
        }
        public async Task<List<T>> GetAsync() =>
       await _collection.Find(_ => true).ToListAsync();
        public async Task<T?> GetByIdAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(T newEntity) =>
            await _collection.InsertOneAsync(newEntity);

        public async Task UpdateAsync(string id, T updatedEntity) =>
            await _collection.ReplaceOneAsync(x => x.Id == id, updatedEntity);

        public async Task RemoveAsync(string id) =>
            await _collection.DeleteOneAsync(x => x.Id == id);
    }
}

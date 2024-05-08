using MongoDB.Driver;
using OzPerksApi.Interfaces;

namespace OzPerksApi.Services
{
    public class RepositoryService<T> : IRepositoryService<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public RepositoryService(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task Create(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task Delete(string id)
        {
            await _collection.DeleteOneAsync(id);
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task Update(string id, T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            await _collection.ReplaceOneAsync(filter, entity);
        }
    }
}

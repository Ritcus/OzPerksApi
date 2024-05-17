using MongoDB.Driver;
using OzPerksApi.Interfaces;
using static OzPerksApi.Models.Enum.Enums;

namespace OzPerksApi.Services
{
    public class RepositoryService<T> : IRepositoryService<T> where T : IDocumentEntity
    {
        private readonly IMongoCollection<T> _collection;

        public RepositoryService(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        #region Generic Operations
        public async Task Create(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task Delete(string id)
        {
            var update = Builders<T>.Update.Set(x => x.IsDeleted, true);
            await _collection.FindOneAndUpdateAsync(a => a.Id == id, update);
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _collection.Find(_ => true && _.IsDeleted != true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Update(string id, T entity)
        {
            entity.Id = id;
            var filter = Builders<T>.Filter.Eq(a => a.Id, id);
            await _collection.FindOneAndReplaceAsync(filter,entity);
        }

        #endregion

        #region Post Operations
        public async Task<byte[]> ConveryImageToByteArray(IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
              using(var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            return null;
        }

        public async Task<IEnumerable<T>> GetPostsByType(PostType postType)
        {

            var filter = Builders<T>.Filter.And(
                Builders<T>.Filter.Eq("type", postType),
                Builders<T>.Filter.Eq("IsActive", true),
                Builders<T>.Filter.Eq("IsDeleted", false)

                );
            return await _collection.Find(filter).ToListAsync();
        }
        #endregion
    }
}

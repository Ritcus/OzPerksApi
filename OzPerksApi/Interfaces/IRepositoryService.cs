
using static OzPerksApi.Models.Enum.Enums;

namespace OzPerksApi.Interfaces
{
    public interface IRepositoryService<T> where T : IDocumentEntity
    {
        #region Generic Operations
        Task<IEnumerable<T>> Get();
        Task<T> GetByIdAsync(string id);
        Task Create(T entity);
        Task Update(string id, T entity);
        Task Delete(string id);
        #endregion

        #region Post Operations
        Task<byte[]> ConveryImageToByteArray(IFormFile file);
        Task<IEnumerable<T>> GetPostsByType(PostType postType);
        #endregion
    }
}

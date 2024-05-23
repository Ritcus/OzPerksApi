
using static OzPerksApi.Models.Enum.Enums;

namespace OzPerksApi.Interfaces
{
    public interface IRepositoryService<T> where T : IDocumentEntity
    {
        #region Generic Operations
        Task<IEnumerable<T>> Get();
        Task<T> GetByIdAsync(string id);
        Task Create(T entity);
        Task<T> Update(string id, T entity);
        Task<bool> Delete(string id);
        #endregion

        #region Post Operations
        Task<byte[]> ConveryImageToByteArray(IFormFile file);
        Task<IEnumerable<T>> GetPostsByType(PostType postType);
        #endregion
    }
}

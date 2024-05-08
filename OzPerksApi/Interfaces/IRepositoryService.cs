namespace OzPerksApi.Interfaces
{
    public interface IRepositoryService<T> where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetByIdAsync(string id);
        Task Create(T entity);
        Task Update(string id, T entity);
        Task Delete(string id);
    }
}

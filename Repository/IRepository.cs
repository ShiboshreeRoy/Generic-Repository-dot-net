namespace GenericRepository.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<T> GetByIdAsync(int id);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);
       
        Task DeleteAsync(T entity);

        // Task<T> UpdateAsync(T entity);
    }
}

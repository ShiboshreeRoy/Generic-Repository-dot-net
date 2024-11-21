
using GenericRepository.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbset;
        private readonly MyDbContext _dbcontext;

        public Repository(MyDbContext dbContext)
        {
            _dbset = dbContext.Set<T>();
            _dbcontext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbset.Remove(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbset.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbset.Attach(entity);
            _dbcontext.Entry(entity).State = EntityState.Modified;

             await _dbcontext.SaveChangesAsync();
            return entity;

        }
    }
}

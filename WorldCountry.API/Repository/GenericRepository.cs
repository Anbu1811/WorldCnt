using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WorldCountry.API.Data;
using WorldCountry.API.Repository.IRepository;

namespace WorldCountry.API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }





        public  async Task Create(T entity)
        {
           await _dbContext.AddAsync(entity);
           await Save();
        }

        public async Task Delete(T entity)
        {
            _dbContext.Remove(entity);
            await Save();
        }

        public async Task<T> Get(int id)
        {
            var result = await _dbContext.Set<T>().FindAsync(id);
            return result;
        }

        public async Task<List<T>> GetAll()
        {
            var result = await _dbContext.Set<T>().ToListAsync();
            return result;
        }

        public bool IsRecordExiste(Expression<Func<T, bool>> condition)
        {
            var result =  _dbContext.Set<T>().AsQueryable().Where(condition).Any();
            return result;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

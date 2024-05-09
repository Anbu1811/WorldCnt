using Microsoft.EntityFrameworkCore;
using WorldCountry.API.Data;
using WorldCountry.API.Model;
using WorldCountry.API.Repository.IRepository;

namespace WorldCountry.API.Repository
{
    public class StateRepository : IStateRepository
    {


        public readonly ApplicationDbContext _dbContext;

        public StateRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task Creat(States entity)
        {
           await _dbContext.AllStates.AddAsync(entity);
            await Save();
        }





        public async Task Delete(States entity)
        {
            _dbContext.AllStates.Remove(entity);
            await Save();

        }





        public async Task<States> Get(int id)
        {
            var get = await _dbContext.AllStates.FindAsync(id);
            return get;
        }





        public async Task<List<States>> GetAll()
        {
            var CountryList = await _dbContext.AllStates.ToListAsync();
            return CountryList;
        }





        public bool IsStateNameExist(string name)
        {
            var result = _dbContext.AllStates.AsQueryable().Where(x=>x.Name.ToLower().Trim() == name.ToLower().Trim()).Any();

            return result;
        }

        

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(States entity)
        {
            _dbContext.AllStates.Update(entity);
            await Save();
        }
    }
}

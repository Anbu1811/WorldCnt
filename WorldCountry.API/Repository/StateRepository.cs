using Microsoft.EntityFrameworkCore;
using WorldCountry.API.Data;
using WorldCountry.API.Model;
using WorldCountry.API.Repository.IRepository;

namespace WorldCountry.API.Repository
{
    public class StateRepository : GenericRepository<States> ,IStateRepository
    {


        public readonly ApplicationDbContext _dbContext;

        public StateRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }


        public async Task Update(States entity)
        {
            _dbContext.AllStates.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

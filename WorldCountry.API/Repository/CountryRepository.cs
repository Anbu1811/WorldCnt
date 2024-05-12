using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WorldCountry.API.Data;
using WorldCountry.API.Model;
using WorldCountry.API.Repository.IRepository;

namespace WorldCountry.API.Repository
{
    public class CountryRepository : GenericRepository<Country> ,ICountryRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public CountryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            
        }



        public async Task Update(Country entity)
        {
            _dbContext.AllCountries.Update(entity);
            await Save();
        }


    }
}

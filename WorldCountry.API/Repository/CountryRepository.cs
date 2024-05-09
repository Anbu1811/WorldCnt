using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WorldCountry.API.Data;
using WorldCountry.API.Model;
using WorldCountry.API.Repository.IRepository;

namespace WorldCountry.API.Repository
{
    public class CountryRepository : ICountryRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public CountryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }






        public async Task Create(Country entity)
        {
           await _dbContext.AllCountries.AddAsync(entity);
            await Save();
        }




        public async Task Update(Country entity)
        {
            _dbContext.AllCountries.Update(entity);
            await Save();
        }







        public async Task Delete(Country entity)
        {
            _dbContext.AllCountries.Remove(entity);
            await Save();
        }







        public async Task<List<Country>> GetAll()
        {


            return await _dbContext.AllCountries.ToListAsync();


        }






        public async Task<Country> GetById(int id)
        {
            var gedById = await _dbContext.AllCountries.FindAsync(id);
            return gedById;
        }







        public async Task Save()
        {
           await _dbContext.SaveChangesAsync();
        }

        public bool IsCountryExist(string name)
        {
            var result = _dbContext.AllCountries.AsQueryable().Where(x => x.Name.ToLower().Trim() == name.ToLower().Trim()).Any();

            return result;
        }
    }
}

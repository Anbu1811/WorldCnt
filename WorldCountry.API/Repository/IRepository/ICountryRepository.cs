using WorldCountry.API.Model;

namespace WorldCountry.API.Repository.IRepository
{
    public interface ICountryRepository
    {
        Task Create(Country entity);
        Task Update(Country entity);
        Task Delete(Country entity);
        Task<List<Country>> GetAll();
        Task<Country> GetById(int id);

        Task Save();

        bool IsCountryExist(string name);

        
    }
}

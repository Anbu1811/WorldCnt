using WorldCountry.API.Model;

namespace WorldCountry.API.Repository.IRepository
{
    public interface ICountryRepository: IGenericRepository<Country>
    {
        
        Task Update(Country entity);
        
        
    }
}

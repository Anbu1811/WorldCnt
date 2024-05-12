using WorldCountry.API.Model;

namespace WorldCountry.API.Repository.IRepository
{
    public interface IStateRepository : IGenericRepository<States>
    {
        
        Task Update(States entity);

        
    }
}

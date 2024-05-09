using WorldCountry.API.Model;

namespace WorldCountry.API.Repository.IRepository
{
    public interface IStateRepository
    {
        Task Creat(States entity);
        Task Delete(States entity);
        Task Update(States entity);

        Task Save();

        Task<List<States>> GetAll();

        Task<States> Get(int id);

        bool IsStateNameExist(string name);
    }
}

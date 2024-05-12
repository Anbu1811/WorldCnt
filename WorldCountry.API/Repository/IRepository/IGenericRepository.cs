using System.Linq.Expressions;

namespace WorldCountry.API.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task Create(T entity);
       
        Task Delete(T entity);

        Task<T> Get(int id);

        Task<List<T>> GetAll ();

        Task Save();

        bool IsRecordExiste(Expression<Func<T, bool>> condition);


    }
}

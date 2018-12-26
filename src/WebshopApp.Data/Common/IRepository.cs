using System.Linq;
using System.Threading.Tasks;

namespace WebshopApp.Data.Common
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All();

        Task AddAsync(TEntity entity);

        Task<int> Delete(TEntity entity);

        Task<int> SaveChangesAsync();

        Task<int> Update(TEntity entity);
    }
}

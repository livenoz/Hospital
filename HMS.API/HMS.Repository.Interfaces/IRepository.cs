using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Repo { get; }
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetAsync(int id);
        TEntity Add(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAsync(int id);
        void Update(TEntity entity);
        Task<int> SaveChangeAsync();
    }
}

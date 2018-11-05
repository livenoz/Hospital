using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Repo { get; }
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T Get(int id);
        Task<T> GetAsync(int id);
        T Add(T entity);
        void Delete(int id);
        Task DeleteAsync(int id);
        void Update(T entity);
        int SaveChange();
        Task<int> SaveChangeAsync();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using VismaUserCore.Entities;

namespace VismaUserCore.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T entity);
        void Update(T entity);
        Task Delete(int id);
    }
}

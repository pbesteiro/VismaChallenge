using System.Collections.Generic;
using System.Threading.Tasks;
using VismaUserCore.Entities;
namespace VismaUserCore.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Insert(T _entity);
        Task<bool> Update(T _entity);
        Task<bool> Delete(int id);
    }
}

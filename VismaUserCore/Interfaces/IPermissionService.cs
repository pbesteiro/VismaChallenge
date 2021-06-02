using System.Collections.Generic;
using System.Threading.Tasks;
using VismaUserCore.Entities;

namespace VismaUserCore.Interfaces
{
    public interface IPermissionService
    {
        Task<Permissions> Get(int id);
        Task<IEnumerable<Permissions>> GetAll();

    }
}

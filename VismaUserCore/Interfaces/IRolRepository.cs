using System.Collections.Generic;
using System.Threading.Tasks;
using VismaUserCore.Entities;

namespace VismaUserCore.Interfaces
{
    public interface IRolRepository: IRepository<Rols>
    {
        Task<ICollection<Permissions>> GetPermissionByRolId(int rolId);
        Task<bool> setPermissionToRol(int rolId, List<int> permissionsId);
    }
}

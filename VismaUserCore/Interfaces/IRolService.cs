using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VismaUserCore.Entities;
using VismaUserCore.Requests;

namespace VismaUserCore.Interfaces
{
    public interface IRolService
    {
        Task<bool> Delete(int id, int systemUserId);
        Task<Rols> Get(int id);
        Task<IEnumerable<Rols>> GetAll();
        Task<Rols> Insert(Rols _rol, int systemUserId);
        Task<bool> Update(Rols _rol, int systemUserId);
        Task<bool> setPermissionToRol(AddPermissionRoleRequest permissions, int systemUserId);
    }
}

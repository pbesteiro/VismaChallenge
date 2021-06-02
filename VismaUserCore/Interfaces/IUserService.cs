using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using VismaUserCore.Entities;
using VismaUserCore.Enumerators;
using VismaUserCore.Requests;

namespace VismaUserCore.Interfaces
{
    public interface IUserService
    {
        Task<Users> Get(int id);
        Task<IEnumerable<Users>> GetAll();
        Task<Users> Insert(Users _user, int systemUserId);
        Task<bool> Update(Users _userToEdit, int systemUserId);
        Task<bool> Delete(int id, int systemUserId);
        Task<bool> PathUserRols(AddRolsUserRequest rols, int systemUserId);
    }
}

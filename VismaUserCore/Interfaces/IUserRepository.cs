using System.Collections.Generic;
using System.Threading.Tasks;
using VismaUserCore.Entities;

namespace VismaUserCore.Interfaces
{
    public interface IUserRepository: IRepository<Users>
    {
        Task<IEnumerable<Users>> GetUsersByEmail(string email);
        Task<Users> GetLoginByEmail(string userName);
        Task<ICollection<Rols>> GetRolsByUserId(int userId);
        Task<bool> setRolsToUser(int userId, List<int> rolsId);
    }
}

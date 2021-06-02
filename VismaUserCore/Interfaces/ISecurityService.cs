using System.Threading.Tasks;
using VismaUserCore.DTOs;
using VismaUserCore.Entities;
using VismaUserCore.Requests;

namespace VismaUserCore.Interfaces
{
    public interface ISecurityService
    {
        Task<Users> GetLogin(UserLoginRequest login);
        bool Check(string encryptedPAssword, string password);
    }
}

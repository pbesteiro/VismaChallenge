using System;
using System.Threading.Tasks;
using VismaUserCore.Entities;

namespace VismaUserCore.Interfaces
{

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Departments> departmentRepository { get; }
        IUserRepository userRepository { get; }
        IRolRepository rolRepository { get; }
        IRepository<Permissions> permissionRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}

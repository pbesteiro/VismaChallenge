using System;
using System.Threading.Tasks;
using VismaUserCore.Entities;
using VismaUserCore.Interfaces;
using VismaUserInsfrestucture.Data;

namespace VismaUserInsfrestucture.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;
          private readonly VismaChallengeContext _context;
          private readonly IRepository<Departments> _departmentRepository;
          private readonly IUserRepository _userRepository;
          private readonly IRolRepository _rolRepository;
          private readonly IRepository<Permissions> _permissionRepository;

          public UnitOfWork(VismaChallengeContext context)
          {
              _context = context;
          }



          public IRepository<Departments> departmentRepository => _departmentRepository ?? new BaseRepository<Departments>(_context);
          public IUserRepository userRepository => _userRepository ?? new UserRepository(_context);
          public IRolRepository rolRepository => _rolRepository ?? new RolRepository(_context);
          public IRepository<Permissions> permissionRepository => _permissionRepository ?? new BaseRepository<Permissions>(_context);
        
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_context == null)
                        _context.Dispose();
                }

                // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // TODO: establecer los campos grandes como NULL
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

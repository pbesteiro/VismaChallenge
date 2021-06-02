using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VismaUserCore.Entities;
using VismaUserCore.Interfaces;
using VismaUserInsfrestucture.Data;

namespace VismaUserInsfrestucture.Repositories
{
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        VismaChallengeContext _context;
        public UserRepository(VismaChallengeContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Users> GetById(int id)
        {
            return await _context.Users.Include("RolsUser.Rol").FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Users>> GetUsersByEmail(string email)
        {
            return await _context.Users.Include("RolsUser.Rol").Where(x => x.Email == email).ToListAsync();
        }
        public async Task<Users> GetLoginByEmail(string email)
        {
            return await _context.Users.Include("RolsUser.Rol").FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
           
        }

        public async Task<ICollection<Rols>> GetRolsByUserId(int userId)
        {
            
            return await _context.RolsUser.Include(r=> r.Rol.RolPermissions).Where(x => x.UserId == userId).Select(r=> r.Rol).ToListAsync();
        }



        public async Task<bool> setRolsToUser(int userId, List<int> rolsId)
        {
            
            var currentRols = _context.RolsUser.Where(r => r.UserId == userId);

            _context.RolsUser.RemoveRange(currentRols);

            foreach (int rId in rolsId)
            {
                RolsUser rol = new RolsUser();
                rol.UserId = userId;
                rol.RolId = rId;

                _context.RolsUser.Add(rol);
            }
                           
            return true;
               

            
        }


    }
}

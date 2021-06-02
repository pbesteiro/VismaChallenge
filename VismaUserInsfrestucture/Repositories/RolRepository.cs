using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VismaUserCore.Entities;
using VismaUserCore.Interfaces;
using VismaUserInsfrestucture.Data;

namespace VismaUserInsfrestucture.Repositories
{
    public class RolRepository: BaseRepository<Rols>, IRolRepository
    {

        VismaChallengeContext _context;
        public RolRepository(VismaChallengeContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Rols> GetById(int id)
        {
            return await _context.Rols.Include("RolPermissions.Permission").FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task<ICollection<Permissions>> GetPermissionByRolId(int rolId)
        {
            return await _context.RolPermissions.Where(r => r.RolId == rolId).Select(r => r.Permission).ToListAsync();
        }




        public async Task<bool> setPermissionToRol(int rolId, List<int> permissionsId)
        {
            
            var currentPermissions = _context.RolPermissions.Where(r => r.RolId == rolId);

            _context.RolPermissions.RemoveRange(currentPermissions);

            foreach (int pId in permissionsId)
            {
                RolPermissions rolPermission = new RolPermissions();
                rolPermission.RolId = rolId;
                rolPermission.PermissionId = pId;

                _context.RolPermissions.Add(rolPermission);
            }


            return true;              

            
        }

        
    }
}

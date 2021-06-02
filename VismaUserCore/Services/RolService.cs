using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VismaUserCore.Entities;
using VismaUserCore.Enumerators;
using VismaUserCore.Exceptions;
using VismaUserCore.Interfaces;
using VismaUserCore.Requests;

namespace VismaUserCore.Services
{
    public class RolService: IRolService
    {

        private readonly IUnitOfWork _unitOfWork;

        public RolService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<bool> Delete(int id, int systemUserId)
        {            
            var rol = await _unitOfWork.rolRepository.GetById(id);
            if (rol == null)            
                throw new BussinessException("The Rol to delete Don't Exists");

            Users userSystem = await _unitOfWork.userRepository.GetById(systemUserId);
            if (userSystem == null)
                throw new BussinessException($"The user logged doesn´t exist");

            if (!await HasRol(systemUserId, TypeRol.ADMIN))
                throw new BussinessException($"Only admins users are allowed to delete rols");

            if (id == (int)TypeRol.ADMIN)
                throw new BussinessException("The  ADMIN Rol can´t be deleted");

            await _unitOfWork.rolRepository.Delete(rol.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Rols> Get(int id)
        {
            return await _unitOfWork.rolRepository.GetById(id);
        }

        public async Task<IEnumerable<Rols>> GetAll()
        {
            return await _unitOfWork.rolRepository.GetAll();
        }

        public async Task<Rols> Insert(Rols _rol, int systemUserId)
        {
            ValidateModel(_rol);

            Users userSystem = await _unitOfWork.userRepository.GetById(systemUserId);
            if (userSystem == null)
                throw new BussinessException($"The user logged doesn´t exist");

            if (!await HasRol(userSystem.Id, TypeRol.ADMIN))
                throw new BussinessException($"Only admins users are allowed to crete rols");

            await _unitOfWork.rolRepository.Add(_rol);
            await _unitOfWork.SaveChangesAsync();

            return _rol;
        }

        public async Task<bool> Update(Rols _rol, int systemUserId)
        {
            ValidateModel(_rol);

            Users userSystem = await _unitOfWork.userRepository.GetById(systemUserId);
            if (userSystem == null)
                throw new BussinessException($"The user logged doesn´t exist");

            var rol = await _unitOfWork.rolRepository.GetById(_rol.Id);
            if (rol == null)
                throw new BussinessException("The rol to update Don't Exists");

            if (!await HasRol(userSystem.Id, TypeRol.ADMIN))
                throw new BussinessException($"Only admins users are allowed to modify rols");

            if (_rol.Id == (int)TypeRol.ADMIN)
                throw new BussinessException("The  ADMIN Rol can´t be modifiy");

            rol.Description = _rol.Description;

            _unitOfWork.rolRepository.Update(rol);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> setPermissionToRol(AddPermissionRoleRequest permissions, int systemUserId)
        {
            Users userSystem = await _unitOfWork.userRepository.GetById(systemUserId);
            if (userSystem == null)
                throw new BussinessException($"The user logged doesn´t exist");

            if (!await HasRol(userSystem.Id, TypeRol.ADMIN))
                throw new BussinessException($"Only admins users are allowed to set permissions rols");

            if (!permissions.RolId.HasValue)
                throw new BussinessException("The id rol can't be null");

            var rol = await _unitOfWork.rolRepository.GetById(permissions.RolId.Value);
            if (rol == null)
                throw new BussinessException("The rol to update Don't Exists");

            if (rol.Id==(int) TypeRol.ADMIN)
                throw new BussinessException("The permissions from ADMIN Rol can´t be modifiy");
            

            var permissionsAllows = await  _unitOfWork.permissionRepository.GetAll();

            var permissionsNotExistent = permissions.PermissionsId.Except(permissionsAllows.Select(r => r.Id));

            if (permissionsNotExistent.Count() > 0)
                throw new BussinessException($"The permission whit id {permissionsNotExistent.First().ToString()} not exists");


            var result = await _unitOfWork.rolRepository.setPermissionToRol(permissions.RolId.Value, permissions.PermissionsId);


            if (!result)
                throw new BussinessException("The rols couldn´t be modified");


            await _unitOfWork.SaveChangesAsync();

            return true;

        }

        private void ValidateModel(Rols _rol)
        {

            //validate user email           
            if (string.IsNullOrWhiteSpace(_rol.Description))
                throw new BussinessException("The description must have a lenght greathen than 0");

        }

        private async Task<bool> HasRol(int _userId, TypeRol rolToCheck)
        {
            var userRols = await _unitOfWork.userRepository.GetRolsByUserId(_userId);

            if (userRols.Count == 0)
                throw new BussinessException("The user has not an assigned rol");

            var rol = userRols.FirstOrDefault(p => p.Id == (int)rolToCheck);

            if (rol == null)
                return false;

            return true;

        }

    }
}

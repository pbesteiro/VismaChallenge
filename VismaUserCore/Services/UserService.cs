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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryptorService _encryptorService;

        public  UserService(IUnitOfWork unitOfWork, IEncryptorService encriptorServices)
        {
            _unitOfWork = unitOfWork;
            _encryptorService = encriptorServices;
        }
        public async Task<bool> Delete(int id, int systemUserId)
        {
            var user = await _unitOfWork.userRepository.GetById(id);
            if (user == null)
            {
                throw new BussinessException("The User to delete Don't Exists");
            }

            //validatte that the user logged exist
            Users userSystem = await _unitOfWork.userRepository.GetById(systemUserId);
            if (userSystem == null)
                throw new BussinessException($"The user logged doesn´t exist");

            if (!await HasPermission(userSystem.Id, TypePermission.DELETE) && !await HasRol(userSystem.Id, TypeRol.ADMIN))
                throw new BussinessException($"The user logged isnt´t allowed to delete users");


            await _unitOfWork.userRepository.Delete(user.Id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Users> Get(int id)
        {
            return await _unitOfWork.userRepository.GetById(id);     
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await _unitOfWork.userRepository.GetAll();             
        }

        public async Task<Users> Insert(Users _user, int systemUserId)
        {
            //validate the model
            await ValidateModel(_user);

            //validate that not exist another user with the same email
            var usersWithSameEmail = await _unitOfWork.userRepository.GetUsersByEmail(_user.Email);
            if (usersWithSameEmail.Count() > 0)
            {
                throw new BussinessException($"An user whith email {_user.Email} already exists!");
            }


            //validatte that the user logged exist
            Users userSystem = await _unitOfWork.userRepository.GetById(systemUserId);
            if (userSystem == null)
                throw new BussinessException($"The user logged doesn´t exist");

            if(! await HasPermission(userSystem.Id,TypePermission.WRITE) && !await HasRol(userSystem.Id, TypeRol.ADMIN))
                throw new BussinessException($"The user logged isnt´t allowed to create users");


            //ecript the password
            var salt = _encryptorService.CreateSalt();
            var encryptedPass = _encryptorService.Encrypt(_user.Password + salt);
            _user.Password = encryptedPass;
            _user.Salt = salt;

            //add the user
            await _unitOfWork.userRepository.Add(_user);
            await _unitOfWork.SaveChangesAsync();

            //add Employee Roll for the new user
            AddRolsUserRequest employeeRol = new AddRolsUserRequest();
            employeeRol.RolsId.Add((int)TypeRol.EMPLOYEE);
            employeeRol.UserId = _user.Id;
            bool result=await PathUserRols(employeeRol, systemUserId);
            if (!result)
                throw new BussinessException("Can´t assign Employ rol to user");

            return _user;
        }

        public async Task<bool> Update(Users _userToEdit, int systemUserId)
        {
            await ValidateModel(_userToEdit);

            //validate exist user whit the id
            var user = await _unitOfWork.userRepository.GetById(_userToEdit.Id);
            if (user == null)
                throw new BussinessException("The User to update Don't Exists");

            //validatte that the user logged exist
            Users userSystem = await _unitOfWork.userRepository.GetById(systemUserId);
            if (userSystem == null)
                throw new BussinessException($"The user logged doesn´t exist");

            //Chekk if try to update a ADMIN user, a user that isn´s ADMIN
            var userToUpateIsAdmin = await HasRol(userSystem.Id, TypeRol.ADMIN);
            var systemUserIsAdmin = await HasRol(userSystem.Id, TypeRol.ADMIN);

            if (userToUpateIsAdmin && !systemUserIsAdmin)
                throw new BussinessException($"Admin users only can by updated by a ADMIN user");

            if (!await HasPermission(userSystem.Id, TypePermission.UPDATE) && !systemUserIsAdmin)
                throw new BussinessException($"The user logged isnt´t allowed to update users");

            user.DepartmentId = _userToEdit.DepartmentId;
            user.Email = _userToEdit.Email;
            user.Name = _userToEdit.Name;

            //ecript the password
            var salt = _encryptorService.CreateSalt();
            var encryptedPass = _encryptorService.Encrypt(_userToEdit.Password + salt);
            user.Password = encryptedPass;
            user.Salt = salt;


            _unitOfWork.userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PathUserRols(AddRolsUserRequest rols, int systemUserId)
        {

            if (!rols.UserId.HasValue)
                throw new BussinessException("The user id can´t by null");

            if (rols.UserId.Value == 0)
                throw new BussinessException("The user id can´t by 0");

            //validatte that the user To asign Rols exist
            Users userToEdit = await _unitOfWork.userRepository.GetById(rols.UserId.Value);
            if (userToEdit == null)
                throw new BussinessException($"The user with id {rols.UserId.Value} doesn´t exist");


            //validatte that the user logged exist
            Users userSystem = await _unitOfWork.userRepository.GetById(systemUserId);
            if (userSystem == null)
                throw new BussinessException($"The user logged doesn´t exist");


            //validate that the rol to asign exists
            var rolsAllows = await _unitOfWork.rolRepository.GetAll();
            var rolsNotExistent = rols.RolsId.Except(rolsAllows.Select(r => r.Id));
            if (rolsNotExistent.Count() > 0)
                throw new BussinessException($"The rol whit id {rolsNotExistent.First().ToString()} not exists");

            //Chekk if try to asign a ADMIN rol a user that isn´s ADMIN
            var hasAdminRolToAsing = rols.RolsId.Contains((int)TypeRol.ADMIN);
            var systemUserIsAdmin = await HasRol(userSystem.Id, TypeRol.ADMIN);

            if (!await HasPermission(userSystem.Id, TypePermission.UPDATE) && !await HasPermission(userSystem.Id, TypePermission.WRITE) && !systemUserIsAdmin)
                throw new BussinessException($"The user logged isnt´t allowed to set rols");

            if (hasAdminRolToAsing && !systemUserIsAdmin)
                throw new BussinessException($"Admin Rol only can by assigned by a ADMIN user");

            //asign the rols to the user
            var result = await _unitOfWork.userRepository.setRolsToUser(rols.UserId.Value, rols.RolsId);

            if (!result)
                throw new BussinessException("The rols couldn´t be modified");

            await _unitOfWork.SaveChangesAsync();

            return true;

        }

        private async Task<bool> HasPermission(int _userId, TypePermission permissionToCheck)
        {
            var userRols = await _unitOfWork.userRepository.GetRolsByUserId(_userId);

            if (userRols.Count == 0)
                throw new BussinessException("The user has not an assigned rol");


            var userPermissions = new List<Permissions>();
            foreach (Rols r in userRols)
            {
                var p = await _unitOfWork.rolRepository.GetPermissionByRolId(r.Id);
                userPermissions.AddRange(p);
            }
                       
            var permission = userPermissions.FirstOrDefault(p => p.Id == (int)permissionToCheck);

            if (permission == null)
                return false;

            return true;

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


        private async Task ValidateModel(Users _user)
        {

            //validate user email           
            if (string.IsNullOrWhiteSpace(_user.Email))
                throw new BussinessException("The email must have a lenght greathen than 0");

            if (!_user.Email.Contains("@"))
                throw new BussinessException("The email must contains @");

            //validate user department
            if (_user.DepartmentId == null)            
                throw new BussinessException($"Departmenidtcan´t be null");            

            if (_user.DepartmentId.Value == 0)            
                throw new BussinessException($"Departmenidtcan´t be 0");            

            var existDepartment = await _unitOfWork.departmentRepository.GetById(_user.DepartmentId.Value);

            if (existDepartment == null)
                throw new BussinessException($"Don´t exists a departmet with Id {_user.DepartmentId.Value}");


            
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VismaUserCore.DTOs;
using VismaUserCore.Entities;
using VismaUserCore.Interfaces;
using VismaUserCore.Requests;

namespace VismaUserCore.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryptorService _encryptorService;
        public SecurityService(IUnitOfWork unitOfWork, IEncryptorService encryptorService)
        {
            _unitOfWork = unitOfWork;
            _encryptorService = encryptorService;
        }

        public async Task<Users> GetLogin(UserLoginRequest login)
        {
            return await _unitOfWork.userRepository.GetLoginByEmail(login.Email);
        }

        public bool Check(string encryptedPAssword, string password)
        {

            return _encryptorService.Verify(encryptedPAssword, password);   

        }

    }
}

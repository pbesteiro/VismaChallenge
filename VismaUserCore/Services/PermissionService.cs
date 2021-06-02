using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaUserCore.Entities;
using VismaUserCore.Exceptions;
using VismaUserCore.Interfaces;
using VismaUserCore.Requests;

namespace VismaUserCore.Services
{
    public class PermissionService :IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }               

        public async Task<Permissions> Get(int id)
        {
            return await _unitOfWork.permissionRepository.GetById(id);
        }

        public Task<IEnumerable<Permissions>> GetAll()
        {
            return _unitOfWork.permissionRepository.GetAll();
        }               
       
    }
}

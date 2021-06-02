using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using VismaUserCore.DTOs;
using VismaUserCore.Entities;
using VismaUserCore.Interfaces;


namespace VismaUserApi.Controllers
{
    [Authorize]
    [Produces("Application/json")] 
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : Controller
    {
        public readonly IPermissionService _permissionService;
        public readonly IMapper _mapper;
        public PermissionController(IPermissionService permissionService, IMapper mapper)
        {
            _permissionService = permissionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all the Permission 
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetPermissions))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = (typeof(IEnumerable<PermissionDTO>)))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await _permissionService.GetAll();
            var permissionsDto = _mapper.Map<IEnumerable<PermissionDTO>>(permissions);
            return Ok(permissionsDto); //return status 200
        }

        /// <summary>
        /// Returns a specific permission by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = (typeof(PermissionDTO)))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPermission(int id)
        {
            var permission = await _permissionService.Get(id);
            if (permission == null)
                return NotFound();

            var permissionDto = _mapper.Map<PermissionDTO>(permission);
            return Ok(permissionDto); //return status 200
        }

    }
}

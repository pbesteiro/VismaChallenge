using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using VismaUserCore.DTOs;
using VismaUserCore.Entities;
using VismaUserCore.Enumerators;
using VismaUserCore.Exceptions;
using VismaUserCore.Interfaces;
using VismaUserCore.Requests;
using VismaUserCore.Responses;
using VismaUserCore.Services;

namespace VismaUserApi.Controllers
{
    [Authorize]
    [Produces("Application/json")] //speceific that the media type of the api is json
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {

        public readonly IRolService _rolService;
        public readonly IUserService _userService;
        public readonly IMapper _mapper;
        public RolController(IRolService rolService, IMapper mapper, IUserService userService)
        {
            _rolService = rolService;
            _mapper = mapper;
            _userService = userService;
        }

        /// <summary>
        /// Returns all the rols
        /// </summary>
        /// <returns>A list of Rols</returns>
        [HttpGet(Name = nameof(GetRols))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = (typeof(IEnumerable<RolDTO>)))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRols()
        {           
            var rols = await _rolService.GetAll();                        
            var rolsDto = _mapper.Map<IEnumerable<RolDTO>>(rols);
            return Ok(rolsDto); //return status 200
           
        }

        /// <summary>
        /// Return a specific Rol by ID
        /// </summary>
        /// <param name="id">id of the rol</param>
        /// <returns>return the data of the rolls whit the perissions that it has asigned</returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = (typeof(RolDTO)))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRol(int id)
        {
            var rol = await _rolService.Get(id);
            if (rol == null)
                return NotFound();

            var rolDto = _mapper.Map<RolDTO>(rol);
            return Ok(rolDto); //return status 200
        }


        /// <summary>
        /// Create a new Rol. Only the Admin users can create Rols
        /// </summary>
        /// <param name="rol">description of the rol</param>
        /// <returns>return the data of the rol</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = (typeof(CreateRolRequest)))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(CreateRolRequest rol)
        {
            var systemUserId = getUserId();
            var _rol = _mapper.Map<Rols>(rol);
            var newRol = await _rolService.Insert(_rol, systemUserId);

            var rolDTO = _mapper.Map<RolDTO>(newRol);

            return Ok(rolDTO); //return status 200
        }

        /// <summary>
        /// Modify the data of the rol, but not change the permission.
        /// Only Admin users can modify rols
        /// </summary>
        /// <param name="rol">data of the rol</param>
        /// <returns>return true if everithing was sucessfull</returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = (typeof(bool)))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(UpdateRolRequest rol)
        {
            var systemUserId = getUserId();
            var _rol = _mapper.Map<Rols>(rol);
            var result = await _rolService.Update(_rol, systemUserId);
            return Ok(result); //return status 200
        }

        /// <summary>
        /// Delete a Rol
        /// </summary>
        /// <param name="id">The id of the rol to delete</param>
        /// <returns>return true if everithing was sucessfull</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = (typeof(bool)))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var systemUserId = getUserId();
            var result = await _rolService.Delete(id, systemUserId);
            return Ok(result); //return status 200
        }

        /// <summary>
        /// Set the permission to a Rol
        /// </summary>
        /// <param name="permissions">The id of the rol to patch, and the id's list of the permissions to asign</param>
        /// <returns>return true if everithing was sucessfull</returns>
        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = (typeof(bool)))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Patch(AddPermissionRoleRequest permissions)
        {
            var systemUserId = getUserId();

            var result = await _rolService.setPermissionToRol(permissions, systemUserId);
            return Ok(result); //return status 200
        }




        private int getUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                return int.Parse(identity.FindFirst("userId").Value);
            }

            throw new BussinessException("Logges User Id couldn´t be found");
        }

    }
}

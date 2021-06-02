using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using VismaUserCore.Requests;
using VismaUserCore.DTOs;
using VismaUserCore.Entities;
using VismaUserCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using VismaUserCore.Enumerators;
using System.Security.Claims;
using VismaUserCore.Exceptions;
using VismaUserCore.Responses;

namespace VismaUserApi.Controllers
{
    [Authorize]
    [Produces("Application/json")] //speceific that the media type of the api is json
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        public readonly IUserService _userService;
        public readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// This Endpoint retrieve all the users 
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetUsers))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = (typeof(IEnumerable<UserResponse>)))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await  _userService.GetAll();
            var userResponse = _mapper.Map<IEnumerable<UserResponse>>(users);  
            return Ok(userResponse); //return status 200
        }

        /// <summary>
        /// Return the data of the specific user 
        /// </summary>
        /// <param name="id">the id of the user to get</param>
        /// <returns>retunr the data of the rol and the list of the asigned rols</returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = (typeof(UserResponse)))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.Get(id);
            if (user == null)
                return NotFound();

            var userResponse = _mapper.Map<UserResponse>(user);
            return Ok(userResponse); //return status 200
        }

        /// <summary>
        /// Create a new User
        /// </summary>
        /// <param name="user">the data of the new user</param>
        /// <returns>return the data of the new user and his new id</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = (typeof(UserResponse)))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(AddUserRequest user)
        {
            var systemUserId = getUserId();

            var _user = _mapper.Map<Users>(user);
            
            var newPost = await _userService.Insert(_user, systemUserId);

            var userResponse = _mapper.Map<UserResponse>(newPost);
            
            return Ok(userResponse); //return status 200
        }


        /// <summary>
        /// Change the data of a user but not chenge his rols
        /// </summary>
        /// <param name="user">the data to save in the user</param>
        /// <returns>return true if everithing was sucessfull</returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = (typeof(bool)))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(UpdateUserRequest user)
        {
            var systemUserId = getUserId();
            var _user = _mapper.Map<Users>(user);
            var result = await _userService.Update(_user, systemUserId);            
            return Ok(result); //return status 200
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id">The id of the user to delete</param>
        /// <returns>return true if everithing was sucessfull</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = (typeof(bool)))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var systemUserId = getUserId();
            var result = await _userService.Delete(id, systemUserId);         
            return Ok(result); //return status 200
        }

        /// <summary>
        /// Set the rols to a user
        /// </summary>
        /// <param name="rols">The id of the user to patch, and the id's list of the rols to asign</param>
        /// <returns>return true if everithing was sucessfull</returns>
        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = (typeof(bool)))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Patch(AddRolsUserRequest rols)
        {

            var systemUserId = getUserId();            

            var result = await _userService.PathUserRols(rols, systemUserId);
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

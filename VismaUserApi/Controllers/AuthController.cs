using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using VismaUserCore.Requests;
using VismaUserCore.Entities;
using VismaUserCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using VismaUserCore.DTOs;

namespace VismaUserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IUserService _userService;
        public readonly ISecurityService _securityService;
        public readonly IConfiguration _configuration;
        public readonly IMapper _mapper;
        public AuthController(IUserService userService, ISecurityService securityService, IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _securityService = securityService;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// Login the user to the system (TRY WITH: email: "pablo@email.com" and pass:"123456" for login as admin)
        /// </summary>
        /// <param name="login">emial and password</param>
        /// <returns>JWT Access</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = (typeof(UserLoginRequest)))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Authentication(UserLoginRequest login)
        {
            //todo: add validation of valid user

            var validation = await IsValidUser(login);
            if (validation.Item1)
            {
                var token = GenerateToken(validation.Item2);
                return Ok(new { Token = token });
            }

            return NotFound();

        }

        private async Task<(bool, UserDTO)> IsValidUser(UserLoginRequest login)
        {
            var user = await _securityService.GetLogin(login);
                       
            if (user == null)
                return (false, null);

            var userDTO = _mapper.Map<UserDTO>(user);
                

            var isValid = _securityService.Check(user.Password, login.Password+user.Salt);

            return (isValid, userDTO);
        }

        private string GenerateToken(UserDTO user)
        {
            //header
            var _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var _signingCredential = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(_signingCredential);


            //claims
            var claims = new[]{
                new Claim("userId",user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email)
                //,new Claim(ClaimTypes.Role,JsonConvert.SerializeObject( user.Rols).ToString())
            };


            //payload
            var payload = new JwtPayload(
                _configuration["Authentication:Issuing"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(1)
                );

            var token = new JwtSecurityToken(header, payload);

            //return serializated token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

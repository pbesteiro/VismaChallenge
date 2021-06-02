using System;
using Xunit;
using VismaUserCore.Interfaces;
using VismaUserCore.Services;
using Moq;
using System.Threading.Tasks;
using VismaUserCore.Entities;
using VismaUserApi.Controllers;
using AutoMapper;
using System.Web.Http.Results;
using VismaUserCore.Requests;

namespace VimaTest
{
   
    public class RolServiceTest
    {
        private readonly Mock<IRolService> _mockRolAppService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUserService> _userService;
        public RolServiceTest()
        {
            _mockRolAppService = new Mock<IRolService>();
            _mockMapper = new Mock<IMapper>();
            _userService= new Mock<IUserService>();

        }

        /// <summary>
        /// Get User Rol when Rol not exist
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetRol_Should_Return_NotFound()
        {
            // Arrange
            var id = 90000;

            _mockRolAppService.Setup(_ => _.Get(It.IsAny<int>()))
                .Returns(Task.FromResult((Rols)null));

            var controller = new RolController(_mockRolAppService.Object, _mockMapper.Object, _userService.Object);

            // Act
            var actionResult = await controller.GetRol(id);

            // Assert
            var result = Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundResult>(actionResult);
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundResult>(result);

        }


        [Fact]
        public async Task GetRol_Should_Return_Rol_Information()
        {
            // Arrange           
            _mockRolAppService.Setup(_ => _.Get(It.IsAny<int>()))
                .Returns(Task.FromResult((Rols)null));

            var controller = new RolController(_mockRolAppService.Object, _mockMapper.Object, _userService.Object);

            // Act
            var actionResult = await controller.GetRol(1);

            // Assert
            var result = Assert.IsType<Rols>(actionResult);
            Assert.Equal("1", result.Id.ToString());

        }


        [Fact]
        public async Task GetRol_Should_Return_Exception()
        {
            var error = "The description must have a lenght greathen than 0";


            _mockRolAppService.Setup(_ => _.Get(It.IsAny<int>()))
                .Returns(Task.FromResult((Rols)null));

            var controller = new RolController(_mockRolAppService.Object, _mockMapper.Object, _userService.Object);

            CreateRolRequest rol = new CreateRolRequest();

            // Act
            var actionResult = await controller.Post(rol);

            // Assert
            var result = Assert.IsType<BadRequestErrorMessageResult>(actionResult);
            Assert.Equal(error,result.Message);

        }

        
    }
}

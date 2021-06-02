using Microsoft.VisualStudio.TestTools.UnitTesting;
using VismaUserCore.Entities;
using VismaUserCore.Interfaces;
using VismaUserCore.Services;
using VismaUserInsfrestucture.Data;
using VismaUserInsfrestucture.Repositories;

namespace TestApi
{
    [TestClass]
    public class RolServiceTest
    {
        [TestMethod]
        public async void Create_ShouldReturnNewRol()
        {
            VismaChallengeContext context = new VismaChallengeContext();

            IUnitOfWork uow = new UnitOfWork(context);
            RolService service = new RolService(uow);

            Rols rol = new Rols();
            rol.Description = "New Rol Test";

            Rols newRol = await service.Insert(rol, 5);

            Assert.AreEqual(newRol.Description, rol.Description);

        }
    }
}

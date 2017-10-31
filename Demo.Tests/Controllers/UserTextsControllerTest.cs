using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Demo.Controllers;

namespace Demo.Tests.Controllers
{
    [TestClass]
    public class UserTextsControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            UserTextsController controller = new UserTextsController();

            // Act
            //ViewResult result = controller.Index().Result as ViewResult;

            // Assert
            //Assert.IsNotNull(result);
        }
    }
}

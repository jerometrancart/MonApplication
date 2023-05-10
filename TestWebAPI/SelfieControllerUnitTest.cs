using Microsoft.AspNetCore.Mvc;
using SelfieAWookie.API.UI.Controllers;

namespace TestWebAPI
{
    public class SelfieControllerUnitTest
    {
        #region Public methods
        [Fact]
        public void Should()
        {
            // ARRANGE
            var controller = new SelfiesController(null);
            // ACT
            var result = controller.TestAMoi();
            // ASSERT

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult okResult = result as OkObjectResult;
            Assert.NotNull(okResult.Value);
        }
        #endregion
    }
}
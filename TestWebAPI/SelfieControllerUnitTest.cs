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
            var controller = new SelfiesController();
            // ACT
            var result = controller.TestAMoi();
            // ASSERT

            Assert.NotNull(result);
            Assert.True(result.GetEnumerator().MoveNext());
        }
        #endregion
    }
}
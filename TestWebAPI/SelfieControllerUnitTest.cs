using Microsoft.AspNetCore.Mvc;
using Moq;
using SelfieAWookie.API.UI;
using SelfieAWookie.API.UI.Application.DTOs;
using SelfieAWookie.API.UI.Controllers;
using SelfieAWookies.Core.Selfies.Domain;
using SelfiesAWookie.Core.Framework;

namespace TestWebAPI
{
    public class SelfieControllerUnitTest
    {
        #region Public methods
        [Fact]
        public void ShouldAddOneSelfie()
        {
            // ARRANGE
            SelfieDto selfie = new SelfieDto();
            var repositoryMock = new Mock<ISelfieRepository>();
            var unit = new Mock<IUnitOfWork>().Object;
            //WHEN YOU USE MOCK YOU HAVE TO MOCK EVERYTHING
            repositoryMock.Setup(item => item.UnitOfWork).Returns(new Mock<IUnitOfWork>().Object);
            repositoryMock.Setup(item => item.AddOne(It.IsAny<Selfie>())).Returns(new Selfie() { Id = 4 });
            // ACT
            var controller = new SelfiesController(repositoryMock.Object);
            var result = controller.AddOne(selfie);
            // ASSERT
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var addedSelfie = (result as OkObjectResult).Value as SelfieDto;
            Assert.NotNull(addedSelfie);
            Assert.True(addedSelfie.Id > 0);
        }
        [Fact]
        public void ShouldReturnListOfSelfies()
        {
            // ARRANGE

            var expectedList = new List<Selfie>(){
                new Selfie() { Wookie = new Wookie() },
                new Selfie() { Wookie = new Wookie() }
            };
            //simulate 
            var repositoryMock = new Mock<ISelfieRepository>();

            repositoryMock.Setup(item => item.GetAll(It.IsAny<int>())).Returns(expectedList);

            var controller = new SelfiesController(repositoryMock.Object);

            // ACT
            var result = controller.GetAll();

            // ASSERT
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult? okResult = result as OkObjectResult;

            Assert.IsType<List<SelfieResumeDto>>(okResult?.Value);
            Assert.NotNull(okResult?.Value);

            List<SelfieResumeDto>? list = okResult.Value as List<SelfieResumeDto>;

            Assert.True(list?.Count == expectedList.Count);
        }
        #endregion
    }
}
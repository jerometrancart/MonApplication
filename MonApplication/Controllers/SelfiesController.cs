using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SelfieAWookie.API.UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SelfiesController : ControllerBase
    {
        #region Public methods
        //[HttpGet]
        //public IEnumerable<Selfie> TestAMoi()
        //{
        //    return Enumerable.Range(1, 10).Select(item => new Selfie() { Id = item });
        //}

        [HttpGet]
        public IActionResult TestAMoi()
        {
            // modele = what i want as data is return
            var model = Enumerable.Range(1, 10).Select(item => new Selfie() { Id = item });

            // return a status code that can be interpreted by front

            // return this.StatusCode(StatusCodes.Status200OK);

            return this.Ok(model);
        }
        #endregion
    }
}

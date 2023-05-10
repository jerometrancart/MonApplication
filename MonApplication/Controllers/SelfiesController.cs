using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookies.Core.Selfies.Infrastructure.Data;
using SelfieAWookie.API.UI;

namespace SelfieAWookie.API.UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SelfiesController : ControllerBase
    {
        #region Fields
        private readonly SelfiesContext? _context = null;
        #endregion
        #region Constructor
        public SelfiesController(SelfiesContext context)
        {
            this._context = context;
        }
        #endregion
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
            // var model = Enumerable.Range(1, 10).Select(item => new Selfie() { Id = item });

            // return a status code that can be interpreted by front
            // return this.StatusCode(StatusCodes.Status200OK);

            var model = this._context.Selfies.Include(item => item.Wookie).Select(item => new { Title = item.Title, WookieId = item.Wookie.Id, NbSelfiesFromWookie = item.Wookie.Selfies.Count() }).ToList();
            

            return this.Ok(model);

           
        }
        #endregion
    }
}

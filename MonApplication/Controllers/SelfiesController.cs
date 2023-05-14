using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;
using SelfieAWookie.API.UI;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookie.API.UI.Application.DTOs;

namespace SelfieAWookie.API.UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SelfiesController : ControllerBase
    {
        #region Fields
        private readonly ISelfieRepository? _repository = null;
        #endregion
        #region Constructor
        public SelfiesController(ISelfieRepository repository)
        {
            this._repository = repository;
        }
        #endregion
        #region Public methods
        //[HttpGet]
        //public IEnumerable<Selfie> TestAMoi()
        //{
        //    return Enumerable.Range(1, 10).Select(item => new Selfie() { Id = item });
        //}

        [HttpGet]
        public IActionResult GetAll([FromQuery] int wookieId = 0)
        {
            // modele = what i want as data is return
            // var model = Enumerable.Range(1, 10).Select(item => new Selfie() { Id = item });

            // return a status code that can be interpreted by front
            // return this.StatusCode(StatusCodes.Status200OK);
            var param = this.Request.Query["wookieId"];
            
            var selfiesList = this._repository?.GetAll(wookieId);

            var model = selfiesList?.Select(item => new SelfieResumeDto { Title = item.Title, WookieId = item.Wookie?.Id, NbSelfiesFromWookie = (item.Wookie?.Selfies?.Count).GetValueOrDefault(0) }).ToList();
            

            return this.Ok(model);
           
        }

        [HttpPost]
        public IActionResult AddOne(SelfieDto dto)
        {
            //INIT THE VALUE FOR RESULT
            IActionResult result = this.BadRequest();

            Selfie addSelfie = this._repository.AddOne(new Selfie()
            {
                ImagePath = dto.ImagePath,
                Title = dto.Title,
            });
            this._repository.UnitOfWork.SaveChanges();

            if(addSelfie != null)
            {
                dto.Id = addSelfie.Id;
                result = this.Ok(dto);

            }

            return result;
        }
        #endregion
    }
}

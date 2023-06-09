﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookies.Core.Selfies.Infrastructure.Data;
using SelfieAWookie.API.UI;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookie.API.UI.Application.DTOs;
using Microsoft.AspNetCore.Cors;
using SelfieAWookie.API.UI.ExtensionsMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MediatR;
using SelfieAWookie.API.UI.Application.Queries;
using SelfieAWookie.API.UI.Application.Commands;

namespace SelfieAWookie.API.UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SelfiesController : ControllerBase
    {
        #region Fields
        private readonly ISelfieRepository? _repository = null;
        private readonly IWebHostEnvironment? _webHostEnvironment = null;
        private readonly IMediator? _mediator = null;
        #endregion

        #region Constructor
        public SelfiesController(IMediator mediator, ISelfieRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            this._repository = repository;
            this._webHostEnvironment = webHostEnvironment;
            this._mediator = mediator;
        }
        #endregion

        #region Public methods
        //[HttpGet]
        //public IEnumerable<Selfie> TestAMoi()
        //{
        //    return Enumerable.Range(1, 10).Select(item => new Selfie() { Id = item });
        //}

        [HttpGet]
        //[EnableCors(SecurityMethods.DEFAULT_POLICY_3)]
        public IActionResult GetAll([FromQuery] int wookieId = 0)
        {
            // modele = what i want as data is return
            // var model = Enumerable.Range(1, 10).Select(item => new Selfie() { Id = item });

            // return a status code that can be interpreted by front
            // return this.StatusCode(StatusCodes.Status200OK);
            var param = this.Request.Query["wookieId"];

            var model = this._mediator.Send(new SelectAllSelfiesQuery() { WookieId = wookieId });

            return this.Ok(model);
           
        }

        // adding a new picture
        //[Route("photos")]
        //[HttpPost]
        ////use async to avoid overloading the thread
        //public async Task<IActionResult> AddPicture()
        //{
        //    //launch a stream that will end when this body ends
        //    using var stream = new StreamReader(this.Request.Body);

        //    var content = await stream.ReadToEndAsync();

        //    return this.Ok();
        //}
        [Route("photos")]
        [HttpPost]
        //use async to avoid overloading the thread
        public async Task<IActionResult> AddPicture(IFormFile picture)
        {
            string filePath = Path.Combine(this._webHostEnvironment.ContentRootPath, @"images\selfies");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = Path.Combine(filePath, picture.FileName);

            using var stream = new FileStream(filePath, FileMode.OpenOrCreate);
            await picture.CopyToAsync(stream);

            try
            {
                this._repository.UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return this.Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddOne(SelfieDto dto)
        {
            //INIT THE VALUE FOR RESULT
            IActionResult result = this.BadRequest();

           

            var item = await this._mediator.Send(new AddSelfieCommand() { Item = dto });
            if (item != null)
            {
                result = this.Ok(item);
            }

            return result;
        }
        #endregion
    }
}

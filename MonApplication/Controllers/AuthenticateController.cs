﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SelfieAWookie.API.UI.Application.DTOs;
using SelfieAWookies.Core.Selfies.Infrastructure.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SelfieAWookie.API.UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        #region Fields
        private readonly SecurityOption? _option = null;
        private readonly UserManager<IdentityUser>? _userManager = null;
        private readonly IConfiguration? _configuration = null;
        private readonly ILogger<AuthenticateController>? _logger = null;
        #endregion

        #region Constructors
        public AuthenticateController(ILogger<AuthenticateController> logger, UserManager<IdentityUser>? userManager, IConfiguration configuration, IOptions<SecurityOption> options) 
        {
            this._option = options.Value;
            this._userManager = userManager;
            this._configuration = configuration;
            this._logger = logger;

            this._logger.LogError("Test log");
        }
        #endregion

        #region Public methods
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] AuthenticateUserDto dtoUser)
        {
            IActionResult result = this.BadRequest();

            var user = new IdentityUser(dtoUser.Login);
            user.Email = dtoUser.Login;
            user.UserName = dtoUser.Name;
            
            var success = await this._userManager.CreateAsync(user, dtoUser.Password);
            
            if(success.Succeeded) 
            { 
                dtoUser.Token = this.GenerateJwtToken(user);
                result = this.Ok(dtoUser);
            }

            return result;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserDto dtoUser)
        {
            
            IActionResult result = this.BadRequest();
            try
            {
                
                var user = await this._userManager.FindByEmailAsync(dtoUser.Login);
                if (user != null)
                {
                    var verif = await this._userManager.CheckPasswordAsync(user, dtoUser.Password);
                    if (verif)
                    {
                        result = this.Ok(new AuthenticateUserDto()
                        {
                            Login = user.Email,
                            Name = user.UserName,
                            Token = this.GenerateJwtToken(user)
                        });
                    }
                }
                
            }

            catch (Exception ex)
            {
                this._logger.LogError("login", ex, dtoUser);
                result = this.Problem("Cannot log");
                
            }
            return result;
        }
        #endregion

        #region Internal methods
        private string GenerateJwtToken(IdentityUser user)
        {
            // Now its ime to define the jwt token which will be responsible of creating our tokens
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            byte[]? key = null;

            // We get our secret from the appsettings
            if(this._option.Key is not null) { 
            key = Encoding.UTF8.GetBytes(this._option.Key);
            }
            // we define our token descriptor
            // We need to utilise claims which are properties in our token which gives information about the token
            // which belong to the specific user who it belongs to
            // so it could contain their id, name, email the good part is that these information
            // are generated by our server and identity framework which is valid and trusted
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                // the JTI is used for our refresh token which we will be convering in the next video
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                // the life span of the token needs to be shorter and utilise refresh token to keep the user signedin
                // but since this is a demo app we can extend it to fit our current need
                Expires = DateTime.UtcNow.AddHours(6),
                // here we are adding the encryption alogorithim information which will be used to decrypt our token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
        #endregion
    }
}

﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SelfieAWookie.API.UI.Application.DTOs;
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
        private UserManager<IdentityUser>? _userManager = null;
        private IConfiguration? _configuration = null;
        #endregion

        #region Constructors
        public AuthenticateController(UserManager<IdentityUser>? userManager, IConfiguration configuration) 
        {
            this._userManager = userManager;
            this._configuration = configuration;
        }
        #endregion

        #region Public methods
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] AuthenticateUserDto dtoUser)
        {
            IActionResult result = this.BadRequest();

            var user = new IdentityUser(dtoUser.Login);
            user.Email = dtoUser.Login;
            user.UserName = dtoUser.Name;
            var success = await this._userManager.CreateAsync(user);
            
            if(success.Succeeded) 
            { 
                dtoUser.Token = this.GenerateJwtToken(user);
                result = this.Ok(dtoUser);
            }

            return result;
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserDto dtoUser)
        {
            IActionResult result = this.BadRequest();

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
            return result;
        }
        #endregion

        #region Internal methods
        private string GenerateJwtToken(IdentityUser user)
        {
            // Now its ime to define the jwt token which will be responsible of creating our tokens
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // We get our secret from the appsettings
            var key = Encoding.UTF8.GetBytes(this._configuration["Jwt:Key"]);

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
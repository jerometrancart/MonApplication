﻿namespace SelfieAWookie.API.UI.Application.DTOs
{
    public class AuthenticateUserDto
    {
        #region Properties
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set;}

        public string? Token { get; set; }
        #endregion
    }
}

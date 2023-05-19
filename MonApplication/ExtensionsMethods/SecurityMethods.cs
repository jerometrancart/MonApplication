using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SelfieAWookies.Core.Selfies.Infrastructure.Configurations;

namespace SelfieAWookie.API.UI.ExtensionsMethods
{
    /// <summary>
    /// About security (cors, jwt)
    /// </summary>
    public static class SecurityMethods
    {
        #region Constants
        public const string DEFAULT_POLICY = "DEFAULT_POLICY";
        public const string DEFAULT_POLICY_2 = "DEFAULT_POLICY 2";
        public const string DEFAULT_POLICY_3 = "DEFAULT_POLICY 3";
        #endregion

        #region Public methods
        public static void AddcustomSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomCors(configuration);
            services.AddCustomAuthentication(configuration);
        }

        public static void AddCustomAuthentication (this IServiceCollection services, IConfiguration configuration)
        {
            SecurityOption securityOption = new SecurityOption();
            configuration.GetSection("Jwt").Bind(securityOption);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                string? maClef = securityOption.Key;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(maClef)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateActor = false,
                    ValidateLifetime = true
                };
            }); 
        }
        public static void AddCustomCors (this IServiceCollection services, IConfiguration configuration)
        {
            CorsOption  corsOption = new CorsOption();
            configuration.GetSection("Cors").Bind(corsOption);
            services.AddCors(options =>
            {

                options.AddPolicy(DEFAULT_POLICY, builder =>
                {
                    builder.WithOrigins(corsOption.Origin)
                    //builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });

                //options.AddPolicy(DEFAULT_POLICY_2, builder =>
                //{
                //    builder.WithOrigins("https://127.0.0.1:5500")
                //    //builder.AllowAnyOrigin()
                //           .AllowAnyHeader()
                //           .AllowAnyMethod();
                //});

                //options.AddPolicy(DEFAULT_POLICY_3, builder =>
                //{
                //    builder.WithOrigins("https://127.0.0.1:5502")
                //    //builder.AllowAnyOrigin()
                //           .AllowAnyHeader()
                //           .AllowAnyMethod();
                //});
            });
        }
        #endregion
    }
}

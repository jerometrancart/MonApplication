using Microsoft.Extensions.DependencyInjection;

namespace SelfieAWookie.API.UI.ExtensionsMethods
{
    /// <summary>
    /// About security (cors, jwt)
    /// </summary>
    public static class SecurityMethods
    {
        #region Constants
        public const string DEFAULT_POLICY = "DEFAULT_POLICY";
        public const string DEFAULT_POLICY_2= "DEFAULT_POLICY 2";
        public const string DEFAULT_POLICY_3 = "DEFAULT_POLICY 3";
        #endregion

        #region Public methods
        public static void AddcustomSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(DEFAULT_POLICY, builder =>
                {
                    builder.WithOrigins(configuration["Cors:Origin"])
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

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
        #endregion

        #region Public methods
        public static void AddcustomSecurity(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(DEFAULT_POLICY, builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }
        #endregion
    }
}

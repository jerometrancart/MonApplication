using SelfieAWookies.Core.Selfies.Infrastructure.Configurations;

namespace SelfieAWookie.API.UI.ExtensionsMethods
{
    /// <summary>
    /// Add custom options from config (json)
    /// </summary>
    public static class OptionsMethod
    {
        #region Public methods
        public static void AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SecurityOption>(configuration.GetSection("Jwt"));
        }
        #endregion
    }
}

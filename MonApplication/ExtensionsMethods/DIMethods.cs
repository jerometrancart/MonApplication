
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructure.Repositories;

namespace SelfieAWookie.API.UI.ExtensionsMethods
{
    public static class DIMethods
    {
        #region Public methods
        public static IServiceCollection AddInjections(this IServiceCollection services)
        {
            services.AddScoped<ISelfieRepository, DefaultSelfieRepository>();
            services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());

            return services;
        }
        #endregion
    }
}

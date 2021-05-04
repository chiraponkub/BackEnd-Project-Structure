using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VShow_BackEnd.Entity;
using VShow_BackEnd.Services.Abstracts;
using VShow_BackEnd.Services.Security;

namespace VShow_CoreLibs
{

    public static class DependencyResolver
    {
        public static void ConfigureLibraries(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBConnect>();
            services.AddTransient<IHashSerucityService, HashSerucityService>();
            services.AddTransient<IJwtSecurityService, JwtSecurityService>();

        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VShow_BackEnd.Entity;
using VShow_BackEnd.Services.Abstracts;
using VShow_BackEnd.Services.Security;
using VShow_CoreLibs.Abstarct;
using VShow_CoreLibs.Abstarct.Mail;
using VShow_CoreLibs.Concrete;
using VShow_CoreLibs.Concrete.Mail;

namespace VShow_CoreLibs
{

    public static class DependencyResolver
    {
        public static void ConfigureLibraries(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBConnect>();
            services.AddTransient<IHashSerucityService, HashSerucityService>();
            services.AddTransient<IJwtSecurityService, JwtSecurityService>();
            services.AddTransient<IAccount, EFAccount>();
            services.AddTransient<IMailService, EFMailService>();
        }
    }
}

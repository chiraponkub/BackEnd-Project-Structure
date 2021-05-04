using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using VShow_BackEnd.Services.Security;

namespace VShow_BackEnd.Configs
{
    /// <summary>
    /// ส่วนของการตั้งค่า ยืนยันตัวตน Authorization
    /// </summary>
    public static class AuthenticationConfig
    {
        /// <summary>
        /// ส่วนของการตั้งค่า ยืนยันตัวตน
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthenticationHelper(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtSecurityService.ISSUER,
                    ValidAudience = JwtSecurityService.AUDIENCE,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecurityService.SECRET_KEY)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(authorization => PolicyConfig.RegisterPolicies(authorization));
        }
    }
}

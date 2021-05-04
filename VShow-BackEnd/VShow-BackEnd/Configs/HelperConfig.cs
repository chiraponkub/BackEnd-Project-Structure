using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace VShow_BackEnd.Configs
{
    /// <summary>
    /// สำหรับทำส่วนของ Document และอื่นๆเพื่อช่วยในการเชื่อมต่อกับ API
    /// </summary>
    public static class HelperConfig
    {
        /// <summary>
        /// ตรวจสอบ Proxy path
        /// </summary>
        public static string ProxyPath => Environment.GetEnvironmentVariable("ASPNETCORE_PROXY_PATH");

        /// <summary>
        /// ตรวจสอบ Environment
        /// </summary>
        public static string EnvironmentName => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        /// <summary>
        /// ส่วนของการตั้งค่าของ Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        public static void AddSwaggerGenHelper(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"{Configuration.GetValue<string>("AppName")} - {EnvironmentName}"
                });

                // Set Generate Document
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    In = ParameterLocation.Header
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        /// <summary>
        /// ส่วนของ Generage Swagger json และ config server สำหรับทดสอบ swagger
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerHelper(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((apiDoc, httpReq) =>
                {
                    apiDoc.Servers.Add(new Microsoft.OpenApi.Models.OpenApiServer
                    {
                        Url = !string.IsNullOrEmpty(ProxyPath) ? $"/{ProxyPath}" : ""
                    });
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "help";
                c.SwaggerEndpoint($"{(!string.IsNullOrEmpty(ProxyPath) ? $"/{ProxyPath}" : "")}/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}

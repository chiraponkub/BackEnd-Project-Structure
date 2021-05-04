using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using VShow_BackEnd.Configs;

namespace VShow_BackEnd.Middlewares
{
    /// <summary>
    /// ดักจับ Error ทั้งหมดของระบบ ก่อนจะ Response ออกไป
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IConfiguration _configuration;

        public ExceptionMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            try
            {
                await _next(httpContext);
                switch ((HttpStatusCode)httpContext.Response.StatusCode)
                {
                    // หากหาหน้าไม่เจอแล้วอยู่ใน Development Mode จะ Redirect ไปหน้า Swagger help
                    case HttpStatusCode.NotFound:
                        if (IsDevelopment())
                        {
                            if (!httpContext.Request.Path.HasValue) return;
                            var Path = httpContext.Request.Path;
                            var ProxyPath = !string.IsNullOrEmpty(HelperConfig.ProxyPath) ? $"/{HelperConfig.ProxyPath}" : "/";
                            if (Path.Value.StartsWith(ProxyPath) || Path.Value.EndsWith("/"))
                            {
                                ProxyPath = ProxyPath.Last().Equals('/') ? ProxyPath : ProxyPath + "/";
                                httpContext.Response.Redirect($"{ProxyPath}help/index.html");
                            }
                            else
                            {
                                httpStatusCode = HttpStatusCode.NotFound;
                                throw new Exception("NotFound");
                            }
                        }
                        break;
                    case HttpStatusCode.Forbidden:
                        httpStatusCode = HttpStatusCode.Forbidden;
                        throw new Exception("Forbidden");
                    case HttpStatusCode.Unauthorized:
                        httpStatusCode = HttpStatusCode.Unauthorized;
                        throw new Exception("Unauthorized");
                }
            }
            // หากเกิดข้อผิดพลาดอื่นๆ Response Message ออกมาหากเป็น Development mode จะมีรายละเอียดเพิ่มเติมมาด้วย
            catch (Exception ex)
            {
                string responseMessage = JsonSerializer.Serialize(new
                {
                    message = ex.ErrorMessage(),
                    exception = IsDevelopment() ? new { ex.Source, ex.StackTrace, } : null
                });
                httpContext.Response.StatusCode = (int)httpStatusCode;
                httpContext.Response.ContentType = "application/json";
                if (ex.Message.Equals("Unauthorized")) httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                if (ex.Message.Equals("Forbidden")) httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                if (ex.Message.Equals("NotFound")) httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await httpContext.Response.WriteAsync(responseMessage);
            }
        }

        /// <summary>
        /// ตรวจสอบ Environment ว่าเป็น Dev หรือไม่
        /// </summary>
        private bool IsDevelopment()
        {
            return _configuration.GetValue("Environment", "Development").Equals("Development");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using VShow_BackEnd.Services.Abstracts;
using VShow_CoreLibs.Services.Upload;

namespace VShow_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : VShowControllerBase
    {
        private readonly IHashSerucityService hashSerucityService;
        public ExampleController(IHashSerucityService hash)
        {
            hashSerucityService = hash;
        }

        /// <summary>
        /// เข้าสู่ระบบ
        /// </summary>
        /// <param name="username">bell</param>
        /// <param name="password">123456</param>
        /// <returns></returns>
        [HttpPost("signin/{username}/{password}")]
        public IActionResult Login(string username, string password)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                string userRole = "Global";
                string guid = "305ebf93-c9a8-40ed-b12d-c4085486138a";
                Guid userID = new Guid(guid);
                string user = "bell";
                string pass = "123456";
                if (user == username && pass == password)
                {
                    return OkAuthentication("SignIn Successfully", userID.ToString(), userRole);
                }
                return BadRequest("Sorry, the email or password is incorrect. Please check your email or password. And try again");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpPost("Upload")]
        public IActionResult Upload(IFormFile file,string SetPath)
        {
            try
            {

                bool Ex = UploadFile.Upload(file, SetPath);
                if (Ex)
                {
                    return Ok("Upload Successfully");
                }
                return Ok("Upload UnSuccessfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        ////[Authorize]
        //[HttpGet("/hash")]
        //public IActionResult Test(string pass)
        //{
        //    try
        //    {
        //        var ss = hashSerucityService.PasswordHash(pass);
        //        return Ok(ss);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpGet("/Den")]
        //public IActionResult Test(string pass, string passhash)
        //{
        //    try
        //    {
        //        var ss = hashSerucityService.PasswordVerify(pass, passhash);
        //        return Ok(ss);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}

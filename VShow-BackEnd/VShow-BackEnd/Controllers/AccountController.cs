using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VShow_BackEnd.Entity;
using VShow_BackEnd.Models.Account;
using VShow_CoreLibs.Abstarct;
using VShow_CoreLibs.Abstarct.Mail;
using VShow_CoreLibs.Entity.Tables;
using VShow_CoreLibs.Services.Upload;

namespace VShow_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : VShowControllerBase
    {
        private readonly IAccount Account;
        private readonly DBConnect db;
        private readonly IMailService mailService;
        public AccountController(DBConnect _db, IAccount account, IMailService mailService)
        {
            Account = account;
            db = _db;
            this.mailService = mailService;
        }


        [HttpPost("SMTP")]
        public async Task<IActionResult> SMTP([FromForm] MailRequest req)
        {
            try
            {
                await mailService.SendEmailAsync(req);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("Forgotpassword ")]
        public IActionResult Forgotpassword(m_Account_Controller_Required_FotgotPassword req)
        {
            try
            { 
                return Ok(Account.ForgotPassword(req));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Information")]
        public IActionResult Information()
        {
            try
            {
                UserProfile thisUserAccount = Account.Information(long.Parse(UserLoginId));
                return Ok(thisUserAccount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
         
        [Authorize]
        [HttpPut("UpdateProfile")]
        public IActionResult UpdateProfile([FromForm] m_Account_Controller_Required_UpdateProfile req)
        {
            try
            {
                var image = UploadFile.Upload(req.file, req.SetPath);
                Account.UpdateProfile(image, req.userAccountId, req);

                return Ok(Account.UpdateProfile(image, req.userAccountId, req)); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SignIn")]
        public IActionResult SignUp(m_Account_Controller_Required_SingIn req)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.ToList());
                var thisUserAccount = Account.SingIn(req);
                return OkAuthentication("SignIn Successfully", thisUserAccount.UserId, thisUserAccount.UserRold);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SignUp")]
        public IActionResult SignUp(m_Account_Controller_Required req)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(s => s.Errors).ToList());
                return Ok(Account.SingUp(req));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("ChangePassword/{UserAccountId}")]
        public IActionResult ChangePassword(long UserAccountId, m_Account_Controller_Required_ChangePassword req)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values.Select(s => s.Errors).ToList());
                return Ok(Account.ChangePassword(UserAccountId, req));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VShow_BackEnd.Models.Account
{
    public class m_Account_Controller_Required
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class m_Account_Controller_Required_SingIn
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class m_Account_Controller_Required_ChangePassword
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }

    public class m_Account_Controller_Required_UpdateProfile
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public long userAccountId { get; set; }
        public string SetPath { get; set; }
        public IFormFile file { get; set; }
    }


    public class m_Account_Controller_Required_FotgotPassword 
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string VerifyCode { get; set; }

    }

    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public class MailRequest 
    {
        public string Subject { get; set; }
        public string ToEmail { get; set; }
        public string Body { get; set; }
        [Required]
        public string Url { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }

}


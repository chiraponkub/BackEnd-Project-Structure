using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VShow_BackEnd.Entity;
using VShow_BackEnd.Models.Account;
using VShow_BackEnd.Services.Abstracts;
using VShow_CoreLibs.Abstarct;
using VShow_CoreLibs.Entity.Tables;

namespace VShow_CoreLibs.Concrete
{
    public class EFAccount : IAccount
    {

        private readonly DBConnect db;
        private readonly IHashSerucityService hashSerucityService;

        public EFAccount(DBConnect _db, IHashSerucityService _hashSerucityService)
        {
            db = _db;
            hashSerucityService = _hashSerucityService;
        }

        public UserProfile Information(long userAccountId)
        {
            UserProfile thisUserAccount = db.UserProfile.Find(userAccountId);
            if (thisUserAccount == null)
                throw new Exception("The specified Member ID is invalid.");
            return thisUserAccount;
        }
        public UserProfile UpdateProfile(string image,long userAccountId, m_Account_Controller_Required_UpdateProfile req)
        {
            UserProfile find = db.UserProfile.FirstOrDefault(f => f.UserId == userAccountId);
            find.FirstName = req.FirstName;
            find.LastName = req.LastName;
            find.Image = image;
            find.UpdateDate = DateTime.UtcNow;
            db.SaveChanges();
            return find;
        }
        public m_Account_Controller_Response SingIn(m_Account_Controller_Required_SingIn req)
        {
            m_Account_Controller_Response models = new m_Account_Controller_Response();
            var find = db.AccountProvider.Where(w => w.Username == req.EmailAddress).FirstOrDefault();
            string text = "Sorry, the EmailAddress or password is incorrect. Please check your EmailAddress  or password. And try again";
            if (find != null)
            {
                var hash = hashSerucityService.PasswordVerify(req.Password, find.HashPassword);
                if (hash)
                {
                    var ss = db.UserProfile.FirstOrDefault(f => f.UserId == find.UserId);
                    models.EmailAddress = ss.Email;
                    models.FirstName = ss.FirstName;
                    models.LastName = ss.LastName;
                    models.UserRold = ss.UserRole;
                    models.UserId = ss.UserId.ToString();
                }
                if (find == null || !hash)
                    throw new Exception(text);
            }
            else
            {
                throw new Exception(text);
            }

            return models;
        }
        public string SingUp(m_Account_Controller_Required req)
        {
            using (var Transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var find = db.AccountProvider.Where(w => w.Username == req.EmailAddress).FirstOrDefault();
                    if (find != null)
                        throw new Exception("This email is already in use.");
                    UserProfile User = new UserProfile
                    {
                        FirstName = req.FirstName,
                        LastName = req.LastName,
                        Email = req.EmailAddress,
                        CreateDate = DateTime.UtcNow,
                        UpdateDate = null
                    };
                    db.UserProfile.Add(User);
                    db.SaveChanges();

                    var hash = hashSerucityService.PasswordHash(req.Password);
                    AccountProvider Account = new AccountProvider
                    {
                        Username = req.EmailAddress,
                        HashPassword = hash,
                        CreateDate = DateTime.UtcNow,
                        UpdateDate = null,
                        AccountVerify = false,
                        ProviderKey = null,
                        ProviderType = "Member",
                        UserId = User.UserId
                    };
                    db.AccountProvider.Add(Account);
                    db.SaveChanges();
                    Transaction.Commit();
                    return "Successfully Registered";
                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }




        }
        public string ChangePassword(long UserAccountId, m_Account_Controller_Required_ChangePassword req)
        {
            var Account = db.AccountProvider.FirstOrDefault(f => f.UserId == UserAccountId);
            if (Account == null)
                throw new Exception("Id Not Found");
            if (hashSerucityService.PasswordVerify(req.OldPassword, Account.HashPassword) && req.NewPassword == req.ConfirmPassword)
            {
                Account.HashPassword = hashSerucityService.PasswordHash(req.NewPassword);
                db.SaveChanges();
            }
            else
            {
                return "Password is incorrect";
            }
            return "Password changed successfully.";
        }
        public string ForgotPassword(m_Account_Controller_Required_FotgotPassword req) 
        {
            var find = db.AccountProvider.FirstOrDefault(f => f.Username == req.EmailAddress);
            var VerifyCode = db.UserProfile.FirstOrDefault(f => f.VerifyCode == req.VerifyCode);
            if (VerifyCode == null || find == null)
                throw new Exception("VerifyCode is invalid or Emailaddress not found.");
            find.HashPassword = hashSerucityService.PasswordHash(req.Password);
            VerifyCode.VerifyCode = null;
            db.SaveChanges();
            return "Password changed successfully.";
        }
    }
}

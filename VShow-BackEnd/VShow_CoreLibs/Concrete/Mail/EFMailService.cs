using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VShow_BackEnd.Entity;
using VShow_BackEnd.Models.Account;
using VShow_CoreLibs.Abstarct.Mail;

namespace VShow_CoreLibs.Concrete.Mail
{
    public class EFMailService : IMailService
    {
        private readonly DBConnect _db;
        private readonly MailSettings _mailSettings;
        public EFMailService(DBConnect db, IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
            _db = db;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = "Hi " + mailRequest.ToEmail;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            RandomNumberGenerator generator = new RandomNumberGenerator();
            string VerifyCode = generator.RandomPassword();
            string Title = "FotgotPassword";
            //string ActionUrl = "https://www.figma.com/file/c2C7oihcqJ9pKDVL22EgJP/Vshow?node-id=7%3A2";
            string ActionUrl = "https://www." + mailRequest.Url + "/VerifyCode=" + VerifyCode + "/Email=" + mailRequest.ToEmail;
            string ss = $@"<div style='background-color: #eef1f3; padding-bottom: 100px; height: 100%;font-family: Sarabun, Google Sans,Roboto,RobotoDraft,Helvetica,Arial,sans-serif;'>
                       <div style='margin-left:auto; margin-right:auto; background-color:white;width: 700px; color:#21262c;height: fit-content; display: flex;; margin-bottom: 2px'>
                            <h2 style='margin: 14px 24px; width: 100%;font-size: 24px;'> {Title} </h2>
                       </div>
                       <div style='margin-left:auto; margin-right:auto; background-color:white;width: 700px; color:#21262c;height: fit-content; display: flex; height: 20px;'></div>
                        <div style='margin-left:auto; margin-right:auto; background-color:white;width: 700px; color:#21262c;height: fit-content; display: flex;'>
                            <a href='{ActionUrl}'
                            style='background-color: #1A1E2B; color:white; width: 200px; height: 31px;margin-left: 20px; border: unset; margin-top: 25px; margin-top: 25px; text-align: center; padding-top: 14px; text-decoration: none;'>
                                UrlLink
                            </a>
                        </div>
                        <div style='margin-left:auto; margin-right:auto; background-color:white;width: 700px; color:#21262c;height: fit-content; display: flex;'>
                            <p style='margin-left: 20px;margin-bottom: 0px;margin-top: 70px;'> Thank you, </p>
                        </div>
                        <div style='margin-left:auto; margin-right:auto; background-color:white;width: 700px; color:#21262c;height: fit-content; display: flex;'>
                            <p style='margin-left: 20px;margin-bottom: 5px;margin-top: 3px;margin-bottom: 50px;'> VShow </p>
                        </div>
                    </div>";
            builder.HtmlBody = ss;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            var find = _db.AccountProvider.FirstOrDefault(f => f.Username == mailRequest.ToEmail);
            var user = _db.UserProfile.FirstOrDefault(f => f.UserId == find.UserId);
            user.VerifyCode = VerifyCode;
            _db.SaveChanges();
            smtp.Disconnect(true);
        }
    }


    public class RandomNumberGenerator
    {
        // Generate a random number between two numbers    
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public string RandomPassword(int size = 0)
        {
            //RandomNumberGenerator generator = new RandomNumberGenerator();
            //int rand = generator.RandomNumber(53, 100);
            //int rand1 = generator.RandomNumber(6, 100);
            //int rand2 = generator.RandomNumber(9, 100);
            //int rand3 = generator.RandomNumber(20, 100);
            //string lower = generator.RandomString(3, true);
            //string lower1 = generator.RandomString(3, true);
            //string lower2 = generator.RandomString(3, true);
            //string Upper = generator.RandomString(2, false);
            //string Upper1 = generator.RandomString(2, false);
            //string Upper2 = generator.RandomString(2, false);
            //string VerifyCode = Upper + rand1 + lower+ "-" + rand2 + lower2 + Upper1+ lower1+"-" + rand;
            //return VerifyCode;


            StringBuilder builder = new StringBuilder();
            //builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1, 999999));
            //builder.Append(RandomString(2, false));
            return builder.ToString();
        }
    }
}

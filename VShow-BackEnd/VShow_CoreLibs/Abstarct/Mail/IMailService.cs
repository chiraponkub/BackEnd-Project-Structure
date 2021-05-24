using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VShow_BackEnd.Models.Account;

namespace VShow_CoreLibs.Abstarct.Mail
{
   public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}

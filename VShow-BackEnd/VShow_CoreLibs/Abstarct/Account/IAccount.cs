using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VShow_BackEnd.Models.Account;
using VShow_CoreLibs.Entity.Tables;

namespace VShow_CoreLibs.Abstarct
{
    public interface IAccount
    {
        UserProfile Information(long userAccountId);
        UserProfile UpdateProfile(string image, long userAccountId, m_Account_Controller_Required_UpdateProfile req);
        m_Account_Controller_Response SingIn(m_Account_Controller_Required_SingIn req);
        string SingUp(m_Account_Controller_Required req);
        string ChangePassword(long UserAccountId, m_Account_Controller_Required_ChangePassword req);
        string ForgotPassword(m_Account_Controller_Required_FotgotPassword req);
    }
}

using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Repository.Repository.EmailSMS
{
    public interface IEmailSMSRepository
    {
        List<EmailSMSCommon> GetSMSList(string User);
        List<EmailSMSCommon> GetSMS(string User, int Id);
        List<EmailSMSCommon> GetEmailList(string User);
        List<EmailSMSCommon> GetEmail(string User, int Id);

        DbResponse UpdateEmailQueue(string User, int id);
        DbResponse UpdateSMSQueue(string User, int id);
    }
}

using MVCERP.Repository.Repository.EmailSMS;
using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Business.Business.EmailSMS
{
    public class EmailSMSBusiness : IEmailSMSBusiness
    {
         IEmailSMSRepository repo;
         public EmailSMSBusiness(EmailSMSRepository _repo)
        {
            repo = _repo;
        }
         public List<EmailSMSCommon> GetSMSList(string User)
         {
             return repo.GetSMSList(User);
         }
         public List<EmailSMSCommon> GetSMS(string User, int id)
         {
             return repo.GetSMS(User, id);
         }
         public List<EmailSMSCommon> GetEmailList(string User)
         {
             return repo.GetEmailList(User);
         }
         public List<EmailSMSCommon> GetEmail(string User, int id)
         {
             return repo.GetEmail(User, id);
         }
         public DbResponse UpdateSMSQueue(string User, int id)
         {
             return repo.UpdateSMSQueue(User, id);
         }
         public DbResponse UpdateEmailQueue(string User, int id)
         {
             return repo.UpdateEmailQueue(User, id);
         }
    }
}

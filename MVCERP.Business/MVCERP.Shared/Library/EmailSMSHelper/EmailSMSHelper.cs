using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Configuration;
using System.Net.Mail;
using MVCERP.Shared.Common;

namespace MVCERP.Shared.Library
{
    public class EmailSMSHelper
    {
        static string smtp = ReadWebConfig("smtp");
        static string mailFrom = ReadWebConfig("mailFrom");
        static string pwd = ReadWebConfig("mailPwd");
        static bool enableSSL = ReadWebConfig("enableSSL").ToLower().Equals("true");
        static int port = Convert.ToInt16(ReadWebConfig("port"));
        static string supportEmail = ReadWebConfig("supportEmail");

        public static void SendNotification()
        {
            try
            {
                SendSMS();
            }
            catch (Exception ex) { }

            try
            {
                SendEmail();
            }
            catch (Exception ex) { }
        }
        public static string SendSMS()
        {
            var sql = "EXEC proc_SMSQueue @flag='s'";
            var db = new EmailSMSDao();
            var dt = db.ExecuteDataTable(sql);
                       
            if (dt == null || dt.Rows.Count < 1)
            {               
                return "No Records";
            }

            var totalRows = dt.Rows.Count;
            var currentRowIndex = 0;
            foreach (DataRow row in dt.Rows)
            {
                var tmpRow = row;
                currentRowIndex++;

                
                var dr = ProcessSMS(ref tmpRow);

                if (dr.ErrorCode.ToString().Trim().Equals("0"))
                {
                    sql = "EXEC proc_SMSQueue @flag='u', @rowId=" + db.FilterString(dr.Id);
                    dr = db.ParseDbResponse(sql);

                    
                }
            }
            return "Success";
        }
        public static string  SendEmail()
        {
            var sql = "EXEC proc_EmailQueue @flag='s'";
            var db = new EmailSMSDao();
            var dt = db.ExecuteDataTable(sql);

            if (dt == null || dt.Rows.Count < 1)
            {                
                return "No Records";
            }

            var totalRows = dt.Rows.Count;
            var currentRowIndex = 0;
            foreach (DataRow row in dt.Rows)
            {
                var tmpRow = row;
                currentRowIndex++;


                var dr = ProcessEmail(ref tmpRow);

                if (dr.ErrorCode.ToString().Trim().Equals("0"))
                {
                    sql = "EXEC proc_EmailQueue @flag='u', @rowId=" + db.FilterString(dr.Id);
                    dr = db.ParseDbResponse(sql);
                }
            }
            return "Success";
        }
        private static DbResponse ProcessSMS(ref DataRow row)
        {
            string mobileNo = "";
            var dr = new DbResponse();
            dr.SetError(1, "Invalid Data to Process", "0");
            try
            {
                var rowId = GetDataRowData(ref row, "rowId");
                mobileNo = GetDataRowData(ref row, "mobileNo");

                var msg = GetDataRowData(ref row, "msg");

                if (!string.IsNullOrWhiteSpace(mobileNo))
                {
                    dr = SendSMS(mobileNo, msg);
                }

                dr.Id = rowId;
                return dr;
            }
            catch (Exception ex)
            {
                Log(ex, mobileNo);
                dr.SetError(1, ex.Message, "");
                return dr;
            }
        }

        private static DbResponse ProcessEmail(ref DataRow row)
        {
            string emailId = "";
            var dr = new DbResponse();
            dr.SetError(1, "Invalid Data to Process", "0");
            try
            {
                var rowId = GetDataRowData(ref row, "rowId");

                emailId = GetDataRowData(ref row, "email");
                var subject = GetDataRowData(ref row, "subject");
                var msg = GetDataRowData(ref row, "msg");
                var cc = GetDataRowData(ref row, "cc");
                var bcc = GetDataRowData(ref row, "bcc");

                if (!string.IsNullOrWhiteSpace(emailId))
                {
                    dr = SendEmail(ref emailId, "", cc, bcc, subject, msg, "", "0");
                }


                dr.Id = rowId;
                return dr;
            }
            catch (Exception ex)
            {
                Log(ex, emailId);
                dr.SetError(1, ex.Message, "");
                return dr;
            }
        }
        public static void Log(Exception error, string to)
        {
            try
            {               
                Log(error.Message, to);
            }
            catch (Exception ex)
            {

            }
        }
        public static void Log(string msg, string to)
        {
            var db = new EmailSMSDao();
            var sql = @"INSERT INTO tblLogs (errorPage, errorMsg, createdBy, createdDate)
                        SELECT 'SMS/Email Notification (" + to + @")','Technical Error : " + db.FilterQuote(msg) + @"','Admin', GETDATE()";

            db.ExecuteDataset(sql);

            //var rootPath = GetAppPath() + "\\tmp";
            //if (!Directory.Exists(rootPath))
            //{
            //    Directory.CreateDirectory(rootPath);
            //}
            //var filePath = rootPath + "\\log" + DateTime.Now.ToString("ddMMyyyy");
            //using (var sw = new StreamWriter(filePath, true))
            //{
            //    sw.Write(DateTime.Now.ToString());
            //    sw.Write(" " + to + " ");
            //    sw.WriteLine(msg);
            //}
        }

       
        #region SMS
        public static DbResponse SendSMS(string mobileNo, string msg, bool sendInBulk)
        {
            var dr = new DbResponse();
            try
            {
                var apiToken = ReadWebConfig("apitoken");
                var smsFrom = ReadWebConfig("smsFrom");


                var client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR1.0.3705;)");
                client.QueryString.Add("token", apiToken);
                client.QueryString.Add("from", smsFrom);
                client.QueryString.Add("to", mobileNo);
                client.QueryString.Add("text", msg);
                
                var baseurl = sendInBulk ? ReadWebConfig("smsBulkURL") : ReadWebConfig("smsURL");
                var data = client.OpenRead(baseurl);

                var reader = new StreamReader(data);
                var s = reader.ReadToEnd();
                data.Close();
                reader.Close();
                dr.SetError(0, s, mobileNo);
                return dr;
            }
            catch (Exception ex)
            {
                dr.SetError(1, ex.Message, mobileNo);
                return dr;
            }
        }

        public static string ReadWebConfig(string key)
        {
            return ReadWebConfig(key, "");
        }
        public static string ReadWebConfig(string key, string defValue)
        {
           return ConfigurationSettings.AppSettings[key] ?? defValue;           
        }
        public static DbResponse SendSMS(string mobileNo, string msg)
        {
            
            var isBulk = mobileNo.IndexOf(",") > -1;

            var res = SendSMS(mobileNo, msg, isBulk);
            return res;

        }


        #endregion


        #region XML To DataTable Helper
        public static DataTable ParseXMLToDataTable(string xml, bool isFile)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return null;
            }
            DataSet ds = ParseXMLToDataSet(xml, isFile);
            if (ds == null || ds.Tables.Count == 0)
                return null;

            return ds.Tables[0];
        }
        public static DataSet ParseXMLToDataSet(string xml, bool isFile)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return null;
            }

            DataSet ds = new DataSet();
            if (isFile)
            {
                ds.ReadXml(xml);
            }
            else
            {
                using (StringReader r = new StringReader(xml))
                {
                    ds.ReadXml(r);
                }
            }
            return ds;
        }
        public static DataRow ParseXMLToDataRow(string xml, bool isFile)
        {
            DataTable dt = ParseXMLToDataTable(xml, isFile);
            if (dt == null || dt.Rows.Count == 0)
                return null;

            return dt.Rows[0];
        }


        public static string GetDataRowData(ref DataRow dr, string colName, string defVal)
        {
            if (ColumnExists(ref dr, colName))
            {
                return (dr[colName] ?? defVal).ToString();
            }
            return "";
        }
        public static string GetDataRowData(ref DataRow dr, string colName)
        {
            return GetDataRowData(ref dr, colName, "");
        }
        public static bool ColumnExists(ref DataRow dr, string colName)
        {
            foreach (DataColumn dc in dr.Table.Columns)
            {
                if (dc.ColumnName.ToUpper().Equals(colName.ToUpper()))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion


        #region Email
        private static bool IsValidEmailAdd(string emailaddress)
        {
            try
            {
                var m = new MailAddress(emailaddress);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static bool IsValidEmailAdd(string emailaddress, ref  List<MailAddress> addList)
        {
            try
            {
                emailaddress = emailaddress.Replace(",", ";");
                foreach (var itm in emailaddress.Split(';'))
                {
                    if (IsValidEmailAdd(itm))
                        addList.Add(new MailAddress(itm));
                }
                return (addList.Count > 0);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static DbResponse SendSupportEmail(string emailMsg)
        {

            return SendEmail(ref supportEmail, "", "", "", "Error on Application", emailMsg, "", "0");
        }
        public static DbResponse SendEmail(ref string mailTo, string replyToAddress, string cc, string bcc, string subject, string bodyOfMail, string attachment, string importance)
        {
            DbResponse dr = new DbResponse();
            try
            {
                bool boolTo = false;
                bool boolCC = false;
                bool boolBCC = false;
                List<MailAddress> MailToList = new List<MailAddress>();
                List<MailAddress> CCList = new List<MailAddress>();
                List<MailAddress> BCCList = new List<MailAddress>();
                boolTo = IsValidEmailAdd(mailTo, ref MailToList);
                boolCC = IsValidEmailAdd(cc, ref CCList);
                boolBCC = IsValidEmailAdd(bcc, ref BCCList);

                if (!boolTo & !boolCC & !boolCC)
                {
                    dr.ErrorCode = 1;
                    dr.Message = "Invalid email addresses";
                    return dr;
                }

                if (!boolTo)
                {
                    mailTo = cc;
                    MailToList = CCList;
                    boolTo = boolCC;
                    boolCC = false;
                }

                if (!boolTo)
                {
                    mailTo = bcc;
                    MailToList = BCCList;
                    boolTo = boolBCC;
                    boolBCC = false;
                }

                MailMessage oMail = new MailMessage();
                oMail.From = new MailAddress("MVCERP<" + mailFrom + ">");
                oMail.Body = bodyOfMail;
                oMail.Subject = subject;

                if (boolTo)
                {
                    foreach (MailAddress itm in MailToList)
                    {
                        oMail.To.Add(itm);
                    }
                }


                SmtpClient oClient = new SmtpClient(smtp);
                oMail.IsBodyHtml = true;

                if (boolCC)
                {
                    foreach (MailAddress itm in CCList)
                    {
                        oMail.CC.Add(itm);
                    }
                }
                if (boolBCC)
                {
                    foreach (MailAddress itm in BCCList)
                    {
                        oMail.Bcc.Add(itm);
                    }
                }
                if (!string.IsNullOrEmpty(attachment))
                {
                    oMail.Attachments.Add(new Attachment(attachment));
                }


                if (IsValidEmailAdd(replyToAddress))
                {
                    oMail.ReplyTo = new MailAddress(replyToAddress);
                }

                oClient.Port = port;
                // //you could use port 21.. but alot of spam gets send trough here, so it's blocked by many ISPs
                oClient.Credentials = new System.Net.NetworkCredential(mailFrom, pwd);
                oClient.EnableSsl = enableSSL;
                oClient.SendAsync(oMail, null);
                dr.ErrorCode = 0;
                dr.Message = "Email Sent Successfully";
                return dr;
            }
            catch (Exception ex)
            {
                dr.Message = ex.Message;
                if (dr.Message.ToLower().StartsWith("mailbox unavailable"))
                {
                    dr.ErrorCode = 10;
                }
                else
                {
                    dr.ErrorCode = 1;
                }
                return dr;
            }
        }

        #endregion
    }
}

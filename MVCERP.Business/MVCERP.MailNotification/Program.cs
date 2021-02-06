using System;
using System.IO;

using System.Data;
using MVCERP.Shared.Common;
using MVCERP.Repository.Repository;

namespace MVCERP.MailNotification
{
    class Program : Helper
    {

        static void Main(string[] args)
        {
            var st = DateTime.Now;

            try
            {
              
                Console.WriteLine("Sending SMS/Email Started");
                PrintLine();
                Console.WriteLine(DateTime.Now.ToString());

                PrintLine();
                StartSendingSMSMain();
                PrintLine();

                Console.WriteLine("Sending SMS/Email Completed");
                Console.WriteLine(DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                Log(ex, "Main()");

                Helper.SendSupportEmail(ex.Message);
            }
                        
        }

        static void StartSendingSMSMain()
        {
            //var sql = "EXEC proc_SMSQueue @flag='s'";
            //var db = new RepositoryDao();
            //var dt = db.ExecuteDataTable(sql);

            var dt = new DataTable();
            dt.Columns.Add("rowId");
            dt.Columns.Add("mobileNo");
            dt.Columns.Add("msg");
            dt.Columns.Add("email");
            dt.Columns.Add("subject");
            dt.Columns.Add("cc");
            dt.Columns.Add("bcc");

            var drow = dt.NewRow();

            drow[0] = "1";
            drow[1] = "9851209525";
            drow[2] = "Hi, Do you know MVCERP is now available in the market.";
            drow[3] = "";
            drow[4] = "Auto Email";
            drow[5] = "";
            drow[6] = "";
            dt.Rows.Add(drow);

            drow = dt.NewRow();

            drow[0] = "1";
            drow[1] = "9851223620";
            drow[2] = "Hi, Do you know MVCERP is now available in the market.";
            drow[3] = "";
            drow[4] = "Auto Email";
            drow[5] = "";
            drow[6] = "";
            dt.Rows.Add(drow);

            drow = dt.NewRow();

            drow[0] = "1";
            drow[1] = "9851111488";
            drow[2] = "Hi, Do you know MVCERP is now available in the market.";
            drow[3] = "";
            drow[4] = "Auto Email";
            drow[5] = "";
            drow[6] = "";
            dt.Rows.Add(drow);

            drow = dt.NewRow();

            drow[0] = "1";
            drow[1] = "9851205397";
            drow[2] = "Hi, Do you know MVCERP is now available in the market.";
            drow[3] = "";
            drow[4] = "Auto Email";
            drow[5] = "";
            drow[6] = "";
            dt.Rows.Add(drow);

            drow = dt.NewRow();
            drow[0] = "1";
            drow[1] = "";
            drow[2] = "Hi, How are you?";
            drow[3] = "sanjayatripathi@gmail.com";
            drow[4] = "Auto Email";
            drow[5] = "";
            drow[6] = "";

            dt.Rows.Add(drow);


            if (dt == null || dt.Rows.Count < 1)
            {
                Write("No SMS to send.");
                return;
            }

            var totalRows = dt.Rows.Count;
            var currentRowIndex = 0;
            foreach (DataRow row in dt.Rows)
            {
                var tmpRow = row;
                currentRowIndex++;

                Write("Processing . . . " + currentRowIndex.ToString() + " of " + totalRows.ToString());
                var dr = ProcessSMS(ref tmpRow);

                if (dr.ErrorCode.Equals("0"))
                {
                    //sql = "EXEC proc_SMSQueue @flag='u', @rowId=" + db.FilterString(dr.Id);
                    //dr = db.ParseDbResponse(sql);
                    //Write(dr.Message);
                    Write("Success");
                }
            }
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
                    dr = Helper.SendSMS(mobileNo, msg);
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
                    dr = Helper.SendEmail(ref emailId, "", cc, bcc, subject, msg, "", "0");
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
        #region Helper

        public static void Write(string data, bool newLine)
        {
            Console.WriteLine(data);
        }
        public static void Write(string data)
        {
            Write(data, true);
        }
        public static void PrintLine()
        {
            Write("----------------------------------------------------------------------------");
        }
        public static void Log(Exception error, string to)
        {
            try
            {
                Write(error.Message);
                Log(error.Message, to);
            }
            catch (Exception ex)
            {

            }
        }
        public static void Log(string msg, string to)
        {
            var db = new RepositoryDao();
            var sql = @"INSERT INTO tblLogs (errorPage, errorMsg, createdBy, createdDate)
                        SELECT 'SMS/Email Notification (" + to + @")','Technical Error : " + db.FilterQuote(msg) + @"','Admin', GETDATE()";

            db.ExecuteDataset(sql);

            var rootPath = GetAppPath() + "\\tmp";
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }
            var filePath = rootPath + "\\log" + DateTime.Now.ToString("ddMMyyyy");
            using (var sw = new StreamWriter(filePath, true))
            {
                sw.Write(DateTime.Now.ToString());
                sw.Write(" " + to + " ");
                sw.WriteLine(msg);
            }
        }
        #endregion
    }
}

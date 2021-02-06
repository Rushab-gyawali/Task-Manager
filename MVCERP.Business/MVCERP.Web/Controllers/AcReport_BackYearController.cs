using iSolutionLife.Business.Business.AccountReport;
using iSolutionLife.Shared.Common;
using iSolutionLife.Shared.Library;
using iSolutionLife.Web.Filters;
using iSolutionLife.Web.Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace iSolutionLife.Web.Controllers
{
    [SessionExpiryFilter]
    public class AcReport_BackYearController : Controller
    {
        //
        // GET: /Report/
        bool MergeColumnHead = false;
        string TotalInColumn = "";
        bool IsPreparedForExcel = false;
        public AcReport_BackYearController(IAccountReport_BackYear _buss)
        {
            buss = _buss;
        }
        string[] query;
        public ActionResult Index()
        {
            var reportName = StaticData.ReadQueryString("reportName", "");
            if (reportName.ToLower() !="daybook")
            {
                query = StaticData.GetIdFromQuery().ToString().Split('&');
                reportName = query[query.Length-1].ToLower();
            }
            
            PrintReport(reportName.ToLower());
            if (IsPreparedForExcel)
            {
                string filePath = ConfigurationManager.AppSettings["filePath"];
                string fileName = filePath + @"\Reports.xlsx";
                return File(fileName, "application/vnd.ms-excel", "ReportInExcel.xlsx");
            }         
            return View();
        }
        public ActionResult AgentCommissionUpload_BackYear()
        {
            //StaticData.CheckFunctionId(ControllerName, "112125");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgentCommissionUpload_BackYear(HttpPostedFileBase doc, string TranDate, string Narration)
        {
            DbResponse res = new DbResponse();
            //StaticData.CheckFunctionId("112125");
            var docPath = StaticData.GetFilePath();
            TranDate = StaticData.FrontToDBDate(TranDate);
            if (!Directory.Exists(docPath))
                Directory.CreateDirectory(docPath);

            string docOrgName = doc.FileName;
            var docExt = Path.GetExtension(docOrgName);

            if (docExt.ToLower() == ".csv")
            {
                var docName = docPath + DateTime.Now.Ticks + docExt;
                if (!string.IsNullOrEmpty(docName))
                {
                    doc.SaveAs(docName);

                    string[] defaultHeader = new string[4];
                    defaultHeader[0] = "ACCOUNTNO";
                    defaultHeader[1] = "DRAMT";
                    defaultHeader[2] = "CRAMT";
                    defaultHeader[3] = "NARRATION";
                    var xml = CSV_XML.GetCSVToXML(docName, true, defaultHeader);
                    if (xml.Length < 20)
                    {
                        ModelState.AddModelError("1", "Uploaded field Mismatched!");
                        return View();
                    }
                    System.IO.File.Delete(docName);

                    var response = buss.UploadAgentCommission(StaticData.GetUser(), xml, TranDate, Narration);
                    StaticData.SetMessageInSession(response);
                    ModelState.AddModelError("", response.Message);
                    return View();
                }
            }
            else
            {
                res.ErrorCode = 1;
                ModelState.AddModelError("1", "");
                res.Message = "Please Upload CSV File.";
                StaticData.SetMessageInSession(res);
                return View();
            }
            return View();
        }

        private void PrintReport(string reportName)
        {
            var report = new Reports();
            //string reportName = StaticData.ReadQueryString("reportName", "sample").ToLower();
            var reportResult = PrepareReport(reportName);
            if (reportResult.ResultSet.Rows.Count >= 1000)
            {
                ExcelDownload excelDownload = new ExcelDownload();
                excelDownload.GenerateExcel(reportResult.Sql, "", reportResult);
                
                IsPreparedForExcel = true;
            }
            ViewData["head"] = reportResult.ReportHead;
            ViewData["filters"] = reportResult.Filters;
            if (reportResult.ErrorCode !=0)
            {
                ViewData["ReportBody"] =  "<div>" + reportResult.Message + "</div>";
            }

            string reportText = GenerateReport(reportResult.ResultSet);
            if (reportResult.Result.Tables.Count==5)
            {
                reportText += GenerateReport(reportResult.Result.Tables[1]);
            }

            ViewData["ReportBody"] =  reportText;
        }
        private string GenerateReport(DataTable dt)
        {
            if (null == dt) return "";

            if (dt.Rows.Count == 0)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder("<table width=\"100%\" style=\"border-collapse: collapse; margin-top: 15px;\">");
            sb.Append("<thead>");
            if (MergeColumnHead)
            {
                var columns = new Dictionary<string, string>();

                foreach (DataColumn col in dt.Columns)
                {
                    var splitPos = col.ColumnName.IndexOf('_');
                    if (splitPos == -1)
                    {
                        columns.Add(col.ColumnName, col.ColumnName);
                    }
                    else
                    {
                        var key = col.ColumnName.Substring(0, splitPos);
                        var value = col.ColumnName.Substring(splitPos + 1, col.ColumnName.Length - splitPos - 1);
                        if (!columns.ContainsKey(key))
                        {
                            columns.Add(key, value);
                        }
                        else
                        {
                            columns[key] = columns[key] + "|" + value;
                        }
                    }
                }

                var row1 = "";
                var row2 = "";

                foreach (var kvp in columns)
                {
                    string[] values = kvp.Value.Split('|');

                    if (values.Length == 1)
                    {
                        row1 = row1 + "<th rowspan=\"2\" style=\"padding: 5px; border: 1px solid #ccc;text-align:center\">" + kvp.Key + "</th>";
                    }
                    else
                    {
                        row1 = row1 + "<th  style=\"padding: 5px; border: 1px solid #ccc;text-align:center\" colspan=\"" + values.Length + "\">" + kvp.Key + "</th>";

                        foreach (string value in values)
                        {
                            row2 = row2 + "<th style=\"padding: 5px; border: 1px solid #ccc;text-align:center\" >" + value + "</th>";
                        }
                    }
                }

                sb.Append("<tr><th rowspan='2'  style=\"padding: 5px; border: 1px solid #ccc;text-align:center\">S.N.</td> " + row1 + "</tr>");
                sb.Append("<tr>" + row2 + "</tr>");

            }
            else
            {
                sb.Append("<th  style=\"padding: 5px; border: 1px solid #ccc;text-align:center\">S.N.</th>");
                foreach (var item in dt.Columns)
                {
                    sb.Append("<th  style=\"padding: 5px; border: 1px solid #ccc;text-align:center\">" + item + "</th>");
                }
            }

            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");
            int CN = 0;

            string[] totalFieldsArray = new string[TotalInColumn.Replace(" ", "").Split('|').Length];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int arrayCnt = 0;
                sb.Append("<tr>");
                CN++;
                sb.Append("<td style=\"padding: 5px; vertical-align: top; border: 1px solid #ccc\">" + CN + "</td>");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    bool isTotal = false;
                    foreach (var item in TotalInColumn.Replace(" ", "").Split('|'))
                    {
                        if (item == j.ToString())
                        {
                            isTotal = true;
                        }
                    }
                    if (isTotal)
                    {
                        double arrayVal = (totalFieldsArray[arrayCnt] == null ? 0 : StaticData.ParseDouble(totalFieldsArray[arrayCnt].ToString()));
                        var dbVal = dt.Rows[i][j].ToString();
                        totalFieldsArray[arrayCnt] = (arrayVal + StaticData.ParseDouble(dbVal)).ToString();
                        sb.Append("<td style=\"padding: 5px; vertical-align: top; border: 1px solid #ccc ;text-align:right \">" + StaticData.ParseMinusValue(dbVal) + "</td>");
                        arrayCnt++;
                    }
                    else
                    {
                        string[] isLink = dt.Rows[i][j].ToString().Split('#');
                        if (isLink.Length > 1)
                        {
                            sb.Append("<td style=\"padding: 5px; vertical-align: top; border: 1px solid #ccc;text-align:center \">" +
                                "<a href='AcReport_BackYear?q=" + StaticData.Base64Encode_URL(isLink[0]) + "'>" + isLink[1] + "</a>"
                                + "</td>");
                        }
                        else
                        {
                            sb.Append("<td style=\"padding: 5px; vertical-align: top; border: 1px solid #ccc;text-align:center \">" + dt.Rows[i][j] + "</td>");
                        }

                    }
                }
                sb.Append("</tr>");
            }

            if (totalFieldsArray.Length > 0)
            {
                sb.Append("<tr>");
                sb.Append("<td style=\"padding: 5px; vertical-align: top; border: 1px solid #ccc\"><strong>Total</strong></td>");
                CN = 0;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    bool isTotal = false;
                    foreach (var item in TotalInColumn.Replace(" ", "").Split('|'))
                    {
                        if (item == j.ToString())
                        {
                            isTotal = true;
                        }
                    }
                    if (isTotal)
                    {
                        sb.Append("<td style=\"padding: 5px; vertical-align: top; border: 1px solid #ccc ;text-align:right \"><strong>" + StaticData.ParseMinusValue(totalFieldsArray[CN]) + "</strong></td>");
                        CN++;
                    }
                    else
                    {
                        sb.Append("<td style=\"padding: 5px; vertical-align: top; border: 1px solid #ccc;text-align:center \">&nbsp;</td>");
                    }
                }
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            return sb.ToString();
        }
        private ReportResultCommon PrepareReport(string reportName)
        {
            ReportResultCommon rpt=new ReportResultCommon();
            if (reportName == "sample")
                rpt = PrepareTranReport();
            else if (reportName == "trialbalance")
                rpt = PrepareTrialBalance();
            else if (reportName == "gldetail")
                rpt = PrepareTrialDetail();
            else if (reportName == "daybook")
                rpt = PrepareDayBook();
            else if (reportName == "daybookacwise")
                rpt = PrepareDayBookAcWise();
            
            return rpt;
        }
        private ReportResultCommon PrepareDayBookAcWise()
        {
            string FromDate = StaticData.FrontToDBDate(query[0]);
            string ToDate = StaticData.FrontToDBDate(query[1]);
            string AccountNo = query[2];
            string FiscalYear = query[3];
            string User = StaticData.GetUser();
            TotalInColumn = "5|6";
            return buss.PrepareDayBookAcWise(FromDate, ToDate, AccountNo, User, FiscalYear);
        }
        private ReportResultCommon PrepareDayBook()
        {
            string FromDate = StaticData.FrontToDBDate(query[0]);
            string ToDate = StaticData.FrontToDBDate(query[1]);
            string vType = query[2];
            string UserId = query[3];
            string Branch = query[4];
            string ShowLedger = query[5];
            string FiscalYear = query[6];
            string AccountNo = StaticData.ReadQueryString("a", "");
            string User = StaticData.GetUser();
            TotalInColumn = "3|4";
            if (ShowLedger.ToLower()=="true")
            {
                TotalInColumn = "4|5";
            }
            return buss.PrepareDayBook(FromDate, ToDate, vType, AccountNo, User, UserId, Branch, ShowLedger, FiscalYear);
        }
        private ReportResultCommon PrepareTranReport()
        {
            string reportType = StaticData.ReadQueryString("reportType", "");
            string pageNumber = StaticData.ReadQueryString("pageNumber", "1");

            string fromDate = StaticData.ReadQueryString("fromDate", "");
            string toDate = StaticData.ReadQueryString("toDate", "");
            string user = StaticData.GetUser();

            return buss.GetSampleReport();
        }
        private ReportResultCommon PrepareTrialBalance()
        {
            string FromDate = StaticData.FrontToDBDate(query[0]);
            string ToDate = StaticData.FrontToDBDate(query[1]);
            string SearchFor = query[2];
            string Branch = query[3];
            string FiscalYear = query[4];
            string user = StaticData.GetUser();
            MergeColumnHead = true;
            TotalInColumn = "3|4|5|6|7|8";
            return buss.PrepareTrialBalance(FromDate, ToDate, SearchFor,user,Branch, FiscalYear);
        }
        private ReportResultCommon PrepareTrialDetail()
        {
            string FromDate = query[0];
            string ToDate = query[1];
            string GL = query[2];
            string FiscalYear = query[4];
            string User = StaticData.GetUser();
            MergeColumnHead = true;
            TotalInColumn = "2|3|4|5|6|7";
            return buss.PrepareTrialDetail(FromDate, ToDate, GL, User, FiscalYear);
        }
    }
}

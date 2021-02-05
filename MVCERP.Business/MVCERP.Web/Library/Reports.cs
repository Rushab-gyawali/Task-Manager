using MVCERP.Shared.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace MVCERP.Web.Library
{
    public class Reports
    {
        private Boolean AllowDrillDown;
        private string cssClass = "table table-responsive table-bordered table-striped";
        private string fieldAlignment = "";
        private string fieldFormat = "";
        private string fieldWrap = "";
        private string tblCaption = "";
        private Boolean mergeColumnHead;
        private ReportResultCommon reportResult = new ReportResultCommon();
        private string excludeColumns = "";
        private bool includeSerialNo = false;
        private bool useDBRowColorCode = false;
        private int subTotalBy = -1;
        private int totalTextCol = -1;
        private int subTotalTextCol = -1;
        private string subTotalFields = "";
        private string subTotalText = "";
        private string totalFields = "";
        private string totalText = "";
        private int totalPage = 0;
        private int pageNo = 0;
        private double grandTotal = 0.00;
        private double grandTotalUsd = 0.00;
        private double grandTotal_1 = 0.00;
        private string reportName = "";
        private int extraCol = 0;
        private string flag = "";
        protected string Url = "";
        private string isExportFull = "";

        protected string GetURL()
        {
            return HttpContext.Current.Request.Url.AbsoluteUri.Replace("&pageNumber=" + GetPageNumber(), "");
        }

        
        private void ShowPaging(DataTable dt)
        {
            DataTable dtPaging = dt;
            int totalRecords = Convert.ToInt32(dtPaging.Rows[0]["TXNCOUNT"].ToString());
            int PageSizes = Convert.ToInt32(dtPaging.Rows[0]["PAGESIZE"].ToString());
            int PageNumber = Convert.ToInt32(dtPaging.Rows[0]["PAGENUMBER"].ToString());
            if (dtPaging.Columns.Count > 3)
                grandTotal = Convert.ToDouble(dtPaging.Rows[0]["GRANDTOTAL"].ToString());

            if (dtPaging.Columns.Count > 4)
                grandTotalUsd = Convert.ToDouble(dtPaging.Rows[0]["GRANDTOTAL_USD"].ToString());

            string cssLink = "pagingLink";

            totalPage = totalRecords / PageSizes;
            if ((totalPage * PageSizes) < totalRecords)
                totalPage++;

            var sbPaging = new StringBuilder("<table class=\"table table-responsive table-striped table-bordered\"><tr><td nowrap='nowrap'>");
            sbPaging.AppendLine("<div  class='reportFilters' >");
            sbPaging.AppendLine("<span style='float:left; width:auto; margin-top:5px;'>Results:&nbsp; " + totalRecords + " records &nbsp; </span>");

            int currPage = GetPageNumber();
            int startPage = (currPage - 5 <= 0 ? 1 : currPage - 5);
            int offSet = (startPage == 1 ? ((currPage - 5) * -1 + 1) : 0);
            int endPage = currPage + 4 + offSet;

            endPage = currPage == 0 ? 10 : endPage;
            endPage = (endPage > totalPage ? totalPage : endPage);

            if (currPage > 10 && (endPage - startPage) + 1 != 10)
            {
                startPage = startPage - (10 - (endPage - startPage + 1));
            }

            if (totalRecords > PageSizes)// Convert.ToInt32(GetStatic.GetReportPagesize()))
            {
                string url = GetURL();
                sbPaging.AppendLine("<img onclick='GotoPage(1);' src='../../Images/paging_Icons/first_page.png' alt='First Page' style='margin-top:5px;float:left;border:none;cursor:pointer' />");
                sbPaging.AppendLine("<img " + (GetPageNumber() != 1 ? " onclick='GotoPage(" + (GetPageNumber() - 1) + ");'" : "") + " src='../../Images/paging_Icons/" + (GetPageNumber() == 1 ? "previous_page_dis" : "previous_page") + ".png' style='margin-top:5px;float:left;border:none;cursor:pointer' alt='Previous Page' /></a>");

                for (int i = startPage; i < endPage + 1; i++)
                {
                    cssLink = PageNumber == i ? "pagingLinkSelected" : "pagingLink";
                    sbPaging.AppendLine("<span onclick ='GotoPage(" + i + ");' class='" + cssLink + "'>" + i + "</span>");
                }

                sbPaging.AppendLine("<img " + (GetPageNumber() != totalPage ? "onclick=GotoPage(" + (GetPageNumber() + 1) + ");" : "") + " src='../../Images/paging_Icons/" + (GetPageNumber() == totalPage ? "next_page_dis" : "next_page") + ".png' style='margin-top:5px;border:none;cursor:pointer' alt='Next Page' /></a>");
                sbPaging.AppendLine("<img  onclick=GotoPage(" + totalPage + "); src='../../Images/paging_Icons/last_page.png' style='margin-top:5px;border:none;cursor:pointer' />");

            }
            sbPaging.AppendLine("</div></td><td nowrap='nowrap' width='135' align=\"right\">");

            if (totalRecords > PageSizes) //Convert.ToInt32(GetStatic.GetReportPagesize()))
                sbPaging.AppendLine("Goto Page:  " + GotoList(totalPage));
            sbPaging.AppendLine("</td></tr></table>");
            //paging.InnerHtml = sbPaging.ToString();
        }

        private string GotoList(int totalPage)
        {
            StringBuilder sb = new StringBuilder("");
            sb.AppendLine("<select id='gotoLabel' onchange=GotoPage(this.value); style='min-width:50px'>");
            for (int i = 0; i < totalPage; i++)
            {
                sb.AppendLine("<option value='" + (i + 1) + "' " + (GetPageNumber() == (i + 1) ? "Selected=Selected" : "") + " >" + (i + 1) + "</option>");
            }
            sb.AppendLine("</select>");
            return sb.ToString();
        }

       
        private int GetPageNumber()
        {
            return
                Convert.ToInt32(StaticData.ReadNumericDataFromQueryString("pageNumber") == 0
                                    ? 1
                                    : StaticData.ReadNumericDataFromQueryString("pageNumber"));
        }

        #region Generate Report

        public String GenerateReport(ref DataTable dt)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
            var ExcludeFieldList = new ArrayList();
            foreach (var col in excludeColumns.Split('|'))
            {
                ExcludeFieldList.Add(col.ToLower());
            }
            ExcludeFieldList.Add("rowcolor");

            var html = new StringBuilder("");
            html.AppendLine("<div>");

            html.AppendLine("<table style='table' class=\"" + cssClass +
                            "\" ");
            if (tblCaption != "")
                html.AppendLine("<tr><td style='td' colspan=\"" + (dt.Columns.Count + extraCol).ToString() +
                                "\"><strong>" + tblCaption + "</strong></td></tr>");
            html.AppendLine(CreateReportHead(ref dt, mergeColumnHead, ref ExcludeFieldList));
            html.AppendLine(CreateReportBody(ref dt, subTotalFields, totalFields, ref ExcludeFieldList, totalTextCol,
                                             subTotalTextCol));

            html.AppendLine("<tr><td style='td' colspan=\"" + (dt.Columns.Count + extraCol) + "\" align=\"center\">");

            if (totalPage == 0)
                totalPage = 1;

            html.AppendLine("<strong>Page " + (GetPageNumber() == 0 ? 1 : GetPageNumber()) + " of " + totalPage +
                            "</strong>");
            html.AppendLine("</td></tr>");
            html.AppendLine("</table>");
            html.AppendLine("</div>");
            return html.ToString();
        }

        private int SerialNo = 0;

        private String CreateReportBody(ref DataTable dt, string subTotalFieldList, string totalFieldList,
                                        ref ArrayList ExcludeFieldList, int totalTextCol, int subTotalTextCol)
        {
            int cnt = 0;
            var body = new StringBuilder("");
            var SerialNoColumnValue = "";

            bool doSubTotal = subTotalBy > -1 ? true : false;
            bool doTotal = totalFieldList != "" ? true : false;

            string[] totalFieldsArray = totalFieldList.Replace(" ", "").Split('|');
            var totalValues = new double[totalFieldsArray.Length];

            string[] subTotalFieldsArray = subTotalFieldList.Replace(" ", "").Split('|');
            var subTotalValues = new double[subTotalFieldsArray.Length];

            string[] fieldFormatList = fieldFormat.Replace(" ", "").Split('|');


            string tmpSubTotalText = "||";

            var hasRowColorCol = dt.Columns.Contains("rowColor");

            foreach (DataRow row in dt.Rows)
            {
                if (includeSerialNo)
                {
                    SerialNo++;
                    SerialNoColumnValue = "<td style='td' align=\"right\">" + SerialNo.ToString() + "</td>";
                }
                else
                {
                    SerialNoColumnValue = "";
                }

                if (doSubTotal)
                {
                    if (tmpSubTotalText == "||")
                        tmpSubTotalText = row[subTotalBy].ToString();

                    if (tmpSubTotalText != row[subTotalBy].ToString())
                    {
                        body.AppendLine(CreateTotalRow(ref dt, subTotalText, subTotalBy, subTotalFieldsArray,
                                                       subTotalValues, fieldFormatList, fieldAlignment, fieldWrap,
                                                       ref ExcludeFieldList, subTotalTextCol, includeSerialNo));
                        tmpSubTotalText = row[subTotalBy].ToString();

                        for (int i = 0; i < subTotalValues.Length; i++)
                        {
                            subTotalValues[i] = 0;
                        }
                    }
                }
                if (useDBRowColorCode && hasRowColorCol)
                {
                    body.AppendLine("<tr style=\"background:" + row["rowColor"].ToString() + ";\">");
                }
                else
                {
                    body.AppendLine(++cnt % 2 == 1 ? "<tr>" : "<tr style=\"background: #F0F0F0;\">");
                }

                body.AppendLine(SerialNoColumnValue);

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (ExcludeFieldList.IndexOf(dt.Columns[i].ColumnName.ToLower()) > -1)
                    {
                        continue;
                    }
                    string format = GetFormat(fieldFormatList, i);

                    string data = row[i].ToString();
                    if (format != "")
                    {
                        double dataParse;
                        double.TryParse(row[i].ToString(), out dataParse);
                        data = dataParse < 0 ? StaticData.ParseMinusValue(dataParse) : dataParse.ToString(format);
                        //Parse Minus Value
                    }
                    if (AllowDrillDown)
                    {
                        data = CreateLink(data);
                    }
                    string alignment = GetAlignment(fieldAlignment, i);
                    string noWrapProperty = GetNoWrapping(fieldWrap, i);
                    body.AppendLine("<td style='td' " + alignment + noWrapProperty + ">" + data + "</td>");

                    var data2 = row[i].ToString();
                    if (doTotal)
                    {
                        int pos = Array.IndexOf(totalFieldsArray, i.ToString());

                        if (pos >= 0)
                        {

                            double value;
                            double.TryParse(data2, out value);

                            totalValues[pos] = totalValues[pos] + value;
                        }
                    }

                    if (doSubTotal)
                    {
                        int pos = Array.IndexOf(subTotalFieldsArray, i.ToString());

                        if (pos >= 0)
                        {
                            
                            double value;
                            double.TryParse(data2, out value);
                            subTotalValues[pos] = subTotalValues[pos] + value;
                        }
                    }
                }

                body.AppendLine("</tr>");
            }

            if (doSubTotal)
            {
                body.AppendLine(CreateTotalRow(ref dt, subTotalText, subTotalBy, subTotalFieldsArray, subTotalValues,
                                               fieldFormatList, fieldAlignment, fieldWrap, ref ExcludeFieldList,
                                               totalTextCol, includeSerialNo));
            }

            if (doTotal)
            {
                if (grandTotal != 0.00)
                {
                    if (totalPage == GetPageNumber())
                        body.AppendLine(CreatGrandTotalRow(ref dt, totalText, 0, totalFieldsArray, grandTotal,
                                                           fieldFormatList,
                                                           fieldAlignment, fieldWrap, grandTotal, grandTotalUsd,
                                                           grandTotal_1, ref ExcludeFieldList, includeSerialNo));
                }
                else
                {
                    body.AppendLine(CreateTotalRow(ref dt, totalText, 0, totalFieldsArray, totalValues, fieldFormatList,
                                                   fieldAlignment, fieldWrap, ref ExcludeFieldList, totalTextCol, includeSerialNo));
                }
            }

            return body.ToString();
        }

        private static String CreateTotalRow(ref DataTable dt, string totalText, int totalFieldIndex,
                                             string[] totalFields, Double[] totalValues, string[] fieldFormatList,
                                             string fieldAlignmentList, string fieldWrapList,
                                             ref ArrayList ExcludeFieldList, int totalTextCol, bool includeSerialNo)
        {
            var rowText = new StringBuilder("");

            rowText.AppendLine("<tr>");

            if (includeSerialNo)
            {
                if (totalText.IndexOf("<td>") == -1)
                    rowText.AppendLine("<td></td>");

            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (ExcludeFieldList.IndexOf(dt.Columns[i].ColumnName.ToLower()) > -1)
                {
                    continue;
                }
                int pos = Array.IndexOf(totalFields, i.ToString());
                string data = "";
                string alignment = "";
                string nowrapProperty = "";
                if (pos >= 0)
                {
                    string format = GetFormat(fieldFormatList, i);
                    data = totalValues[pos] < 0
                               ? StaticData.ParseMinusValue(totalValues[pos])
                               : totalValues[pos].ToString(format.ToUpper());
                    alignment = GetAlignment(fieldAlignmentList, i);
                    nowrapProperty = GetNoWrapping(fieldWrapList, i);
                }
                if (totalTextCol > -1)
                {
                    totalFieldIndex = totalTextCol;
                }

                if (i == totalFieldIndex) data = totalText;
                rowText.AppendLine("<td style='td' " + alignment + nowrapProperty + "><b>" + data + "</b></td>");
            }

            rowText.AppendLine("</tr>");
            return rowText.ToString();
        }

        private static String CreatGrandTotalRow(ref DataTable dt, string totalText, int totalFieldIndex,
                                                 string[] totalFields, Double totalValues, string[] fieldFormatList,
                                                 string fieldAlignmentList, string fieldWrapList, Double grandTotal,
                                                 Double grandTotalUsd, Double grandTotal_1,
                                                 ref ArrayList ExcludeFieldList, bool includeSerialNo)
        {
            var rowText = new StringBuilder("");

            rowText.AppendLine("<tr>");
            if (includeSerialNo)
            {
                if (totalText.IndexOf("<td>") == -1)
                    rowText.AppendLine("<td></td>");
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (ExcludeFieldList.IndexOf(dt.Columns[i].ColumnName.ToLower()) > -1)
                {
                    continue;
                }

                int pos = Array.IndexOf(totalFields, i.ToString());
                string data = "";
                string alignment = "";
                string nowrapProperty = "";
                if (pos >= 0)
                {
                    data = StaticData.ParseMinusValue(grandTotal.ToString());
                    alignment = GetAlignment(fieldAlignmentList, i);
                    nowrapProperty = GetNoWrapping(fieldWrapList, i);
                }

                if (i == totalFieldIndex)
                    data = totalText;

                if (i == 9 && grandTotal != 0.00)
                {
                    rowText.AppendLine("<td style='td' align=\"right\"><b>" + StaticData.ParseMinusValue(grandTotal) +
                                       "</b></td>");
                }
                else if (i == 11 && grandTotalUsd != 0.00)
                {
                    rowText.AppendLine("<td style='td' align=\"right\"><b>" + StaticData.ParseMinusValue(grandTotalUsd) +
                                       "</b></td>");
                }
                else if (i == 13 && grandTotal_1 != 0.00)
                {
                    rowText.AppendLine("<td style='td' align=\"right\"><b>" + StaticData.ParseMinusValue(grandTotal_1) +
                                       "</b></td>");
                }
                else
                {
                    rowText.AppendLine("<td style='td' " + alignment + nowrapProperty + "><b>" + data + "</b></td>");
                }
            }

            rowText.AppendLine("</tr>");
            return rowText.ToString();
        }

        private static string GetFormat(string[] fieldFormatList, int currFieldIndex)
        {
            return fieldFormatList.Length > currFieldIndex ? fieldFormatList[currFieldIndex] : "";
            //return ( pos == -1 ? "": fieldFormatList[pos]);
        }

        private static string GetNoWrapping(string fieldWrapList, int currFieldIndex)
        {
            if (fieldWrapList == "")
                return "";

            string[] wrapListArray = fieldWrapList.Split('|');
            string isWrap = wrapListArray.Length > currFieldIndex ? wrapListArray[currFieldIndex] : "";
            string noWrapValue = "";
            if (isWrap == "Y")
                noWrapValue = " nowrap = \"nowrap\"";
            return noWrapValue;
        }

        private static string GetAlignment(string fieldAlignmentList, int currFieldIndex)
        {
            if (fieldAlignmentList == "")
                return "";

            string[] alignListArray = fieldAlignmentList.Split('|');
            string alignName = alignListArray.Length > currFieldIndex ? alignListArray[currFieldIndex] : "";
            string align = "";
            switch (alignName.ToUpper())
            {
                case "R":
                    align = " align=\"right\"";
                    break;
                case "L":
                    align = " align=\"left\"";
                    break;
                case "C":
                    align = " align=\"center\"";
                    break;
                default:
                    break;
            }
            return align;
        }

        private static string CreateLink(string data)
        {
            return "";
        }

        private String CreateReportHead(ref DataTable dt, Boolean merge, ref ArrayList ExcludeFieldList)
        {
            var head = new StringBuilder("");

            var SerialNoColumnHead = "";
            if (includeSerialNo)
            {
                SerialNoColumnHead = "<th style='th'>SN.</th>";
                extraCol = 1;
            }

            if (!merge)
            {
                head.AppendLine("<tr>");

                head.AppendLine(SerialNoColumnHead);
                foreach (DataColumn col in dt.Columns)
                {
                    if (ExcludeFieldList.IndexOf(col.ColumnName.ToLower()) > -1)
                    {
                        extraCol--;
                        continue;
                    }
                    head.AppendLine("<th style='th'>" + col.ColumnName + "</th>");

                }
                head.AppendLine("</tr>");
            }
            else
            {
                var columns = new Dictionary<string, string>();

                foreach (DataColumn col in dt.Columns)
                {
                    if (ExcludeFieldList.IndexOf(col.ColumnName.ToLower()) > -1)
                    {
                        extraCol--;
                        continue;
                    }
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
                        row1 = row1 + "<th style='th' rowspan=\"2\">" + kvp.Key + "</th>";
                    }
                    else
                    {
                        row1 = row1 + "<th style='th' align=\"center\" colspan=\"" + values.Length + "\">" + kvp.Key +
                               "</th>";

                        foreach (string value in values)
                        {
                            row2 = row2 + "<th style='th'>" + value + "</th>";
                        }
                    }
                }

                if (includeSerialNo)
                {
                    SerialNoColumnHead = "<th style='th' rowspan=\"2\">SN.</th>";
                }

                head.AppendLine("<tr>" + SerialNoColumnHead + row1 + "</tr>");
                head.AppendLine("<tr>" + row2 + "</tr>");
            }

            return head.ToString();
        }

        #endregion

        private ColDefinatoin GetColumnNameToIndex(DataTable dt, string totalFieldNameList, string subTotalFieldNameList)
        {
            var r = new ColDefinatoin();
            var fList = new ArrayList();

            r.Alignment = "";
            r.Format = " ";
            r.SubTotalFields = " ";
            r.TotalFields = " ";

            for (var i = 0; i < dt.Columns.Count; i++)
            {
                var cp = new ColProperties(i);
                fList.Add(cp);
            }

            var cList = totalFieldNameList.Split('|');
            foreach (var colName in cList)
            {
                var pos = dt.Columns.IndexOf(colName);
                if (pos >= 0)
                {
                    var cp = (ColProperties)fList[pos];
                    cp.IsTotal = true;
                    fList[pos] = cp;
                }
            }
            foreach (var colName in cList)
            {
                var pos = dt.Columns.IndexOf(colName);
                pos = pos - 1;
                if (pos >= 0)
                {
                    var cp = (ColProperties)fList[pos];
                    cp.IsNumeric = true;
                    fList[pos] = cp;
                }
            }

            cList = subTotalFieldNameList.Split('|');
            foreach (var colName in cList)
            {
                var pos = dt.Columns.IndexOf(colName);
                if (pos >= 0)
                {
                    var cp = (ColProperties)fList[pos];
                    cp.IsSubTotal = true;
                    fList[pos] = cp;
                }
            }


            foreach (ColProperties itm in fList)
            {
                r.Alignment = r.Alignment + (r.Alignment.Length > 0 ? "|" : "") +
                              ((itm.IsSubTotal || itm.IsTotal) ? "R" : "L");
                r.Format = r.Format + (r.Format.Length > 0 ? "|" : "") + ((itm.IsNumeric) ? "N" : "");
                r.SubTotalFields = r.SubTotalFields + (r.SubTotalFields.Length > 0 ? "|" : "") +
                                   (itm.IsSubTotal ? itm.Index.ToString() : "");
                r.TotalFields = r.TotalFields + (r.TotalFields.Length > 0 ? "|" : "") +
                                (itm.IsTotal ? itm.Index.ToString() : "");
            }

            return r;
        }

        public class ColDefinatoin
        {
            public string TotalFields { get; set; }
            public string SubTotalFields { get; set; }
            public string Alignment { get; set; }
            public string Format { get; set; }
        }

        public class ColProperties
        {
            public int Index { get; set; }
            public bool IsTotal { get; set; }
            public bool IsNumeric { get; set; }
            public bool IsSubTotal { get; set; }

            public ColProperties()
            {
            }

            public ColProperties(int index)
            {
                Index = index;
                IsTotal = false;
                IsSubTotal = false;
                IsNumeric = false;
            }

        }
    }
}

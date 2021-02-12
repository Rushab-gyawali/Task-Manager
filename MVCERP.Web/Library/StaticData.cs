using ClosedXML.Excel;
using MVCERP.Shared.Common;
using MVCERP.Shared.Common.ReportComponent;
using MVCERP.Web.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVCERP.Web.Library
{
    [SessionExpiryFilter]
    public class StaticData
    {
        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Accept date from form in dd/MM/yyyy</param>
        /// <returns>returns DB accepted format Date : MM/dd/yyyy</returns>
        public static string FrontToDBDate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            try
            {
                string[] date = null;
                if (value.Length == 9)
                {
                    date = value.Split('/');
                }
                else
                {
                    date = value.Substring(0, 10).Split('/');
                }
                return date[1] + "/" + date[0] + "/" + date[2];
            }
            catch (Exception e)
            {
                return "";
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Accept date from DB in MM/dd/yyyy</param>
        /// <returns>returns Date in: dd/MM/yyyy</returns>
        public static string DBToFrontDate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            try
            {
                string sysFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                if (sysFormat.ToLower().Contains("dd/mm/yyyy"))
                {
                    return value.Substring(0, 10);
                }

                var validDate = value.Split(' ');
                string[] date = validDate[0].Split('/');
                return (date[1].Length == 1 ? "0" + date[1] : date[1]) + "/" + (date[0].Length == 1 ? "0" + date[0] : date[0]) + "/" + date[2];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EngDate">english date in MM/dd/yyyy</param>
        /// <param name="format">Nepali date format if 1 : yyyy-MM-dd, 2 : dd/MM/yyyy, default 0 : MM/dd/yyyy</param>
        /// <returns>returns Nepali Date</returns>
        public static string ConvertEng_NepDate(string EngDate, int format = 1, string Seperator = "/")
        {
            if (String.IsNullOrWhiteSpace(EngDate))
            {
                return "";
            }
            MVCERP.Shared.Library.NepaliCalender.IConvertToNepali nep = new MVCERP.Shared.Library.NepaliCalender.ConvertToNepali();
            var dt = nep.GetNepaliDate(DateTime.Parse(EngDate));
            var returnDt = dt.npDate;
            if (format == 0)
            {
                returnDt = String.Format("{0}" + Seperator + "{1}" + Seperator + "{2}", dt.npMonth, dt.npDay, dt.npYear);
            }
            else if (format == 1)
            {
                returnDt = String.Format("{0}-{1}-{2}", dt.npYear, dt.npMonth, dt.npDay);
            }
            else if (format == 2)
            {
                returnDt = String.Format("{0}" + Seperator + "{1}" + Seperator + "{2}", dt.npDay, dt.npMonth, dt.npYear);
            }
            else if (format == 3)
            {
                returnDt = String.Format("{0}" + Seperator + "{1}" + Seperator + "{2}", dt.npYear, dt.npMonth, dt.npDay);
            }
            return returnDt;
        }

        /// <summary>
        /// current Date
        /// </summary>
        /// <returns>returns todays date</returns>
        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString("dd/MM/yyyy");
        }
        public static string GetCurrent_NepDate()
        {
            return ConvertEng_NepDate(DateTime.Now.ToString("MM/dd/yyyy"), 1);
        }
        #endregion

        public static string NumberToText(string n)
        {
            var str = n.Split('.');
            int number = Convert.ToInt32(str[0]);
            int dec = Convert.ToInt32(str[1]);

            if (number == 0) return "Zero";

            if (number == -2147483648)
                return
                    "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight Rupees Fifty Paisa";

            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }

            string[] words0 = {
                                  "", "One ", "Two ", "Three ", "Four ",
                                  "Five ", "Six ", "Seven ", "Eight ", "Nine "
                              };

            string[] words1 = {
                                  "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
                                  "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen "
                              };

            string[] words2 = {
                                  "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
                                  "Seventy ", "Eighty ", "Ninety "
                              };

            string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            num[0] = number % 1000;               // units 
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2];       // thousands 
            num[3] = number / 10000000;           // crores 
            num[2] = num[2] - 100 * num[3];       // lakhs 

            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }


            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;

                u = num[i] % 10;  // ones 
                t = num[i] / 10;
                h = num[i] / 100; // hundreds 
                t = t - 10 * h;   // tens 

                if (h > 0) sb.Append(words0[h] + "Hundred ");

                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");

                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }

                if (i != 0) sb.Append(words3[i - 1]);

            }

            sb.Append(" Rupees ");

            int d1 = dec / 10;
            int d2 = dec % 10;
            if (d1 == 0)
                sb.Append(words0[d1]);
            else if (d1 == 1)
                sb.Append(words1[d2]);
            else
                sb.Append(words2[d1 - 2] + words0[d2]);

            if (dec > 0)
                sb.Append(" Paisa");
            return sb.ToString().TrimEnd() + " only";
        }
        
        public static DbResponse GetSessionMessage()
        {
            var resp = HttpContext.Current.Session["Msg"] as MVCERP.Shared.Common.DbResponse;
            HttpContext.Current.Session.Remove("Msg");
            return resp;
        }
        public static void SetMessageInSession(DbResponse resp)
        {
            HttpContext.Current.Session["Msg"] = resp;
        }
        public static void SetMessageInSession(int code, string Msg)
        {
            var resp = new DbResponse()
            {
                ErrorCode = code,
                Message = Msg
            };
            SetMessageInSession(resp);
        }
        public static string ReadWebConfig(string key)
        {
            return ReadWebConfig(key, "");
        }


        public static string ReadWebConfig(string key, string defValue)
        {
            return ConfigurationManager.AppSettings[key] ?? defValue;
        }
        public static string GetUser()
        {
            var user = ReadSession("UserName", "");
            if (null == user)
            {
                HttpContext.Current.Response.Redirect("/Home");
            }

            if (ReadSession("ForcePwdChange", "").ToUpper() == "Y")
            {
                HttpContext.Current.Response.Redirect("/ChangeUserPassword");
            }
            return user;
        }
        public static int GetPageSize()
        {
            return ParseInt(ReadQueryString("Pagesize", "100"));
        }
        public static List<SelectListItem> SetDDLValue(Dictionary<string, string> dictionary, string selectedVal, string defLabel = "", bool isTextAsValue = false)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            if (!string.IsNullOrWhiteSpace(defLabel))
            {
                items.Add(new SelectListItem { Text = defLabel, Value = "" });
            }
            if (dictionary.Count > 0)
            {
                foreach (var item in dictionary)
                {
                    string Value = item.Key;
                    string Name = item.Value;
                    if (isTextAsValue)
                        Value = Name;

                    if (Value.ToUpper() == selectedVal)
                    {
                        items.Add(new SelectListItem { Text = Name, Value = Value, Selected = true });
                    }
                    else
                    {
                        items.Add(new SelectListItem { Text = Name, Value = Value });
                    }
                }
            }
            return items;
        }
        /// <summary>
        /// Static is active ddl
        /// </summary>
        /// <param name="selectedVal"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetIsActiveDdl(string selectedVal = "1", bool activeText = false)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = (activeText ? "Active" : "Yes"), Value = "1", Selected = (selectedVal == "1" ? true : false) });
            items.Add(new SelectListItem { Text = (activeText ? "Passive" : "No"), Value = "0", Selected = (selectedVal == "0" ? true : false) });

            return items;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"> upload postfile</param>
        /// <param name="path"> folder Name </param>
        /// <param name="CheckContentType">CheckContentType boolean value</param>
        /// <returns></returns>
        public static DbResponse UploadDoc(HttpPostedFileBase file, string path, bool CheckContentType = true)
        {
            var response = new DbResponse();
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    if (CheckContentType)
                    {
                        var fileType = file.ContentType;
                        if (fileType == "image/jpeg" || fileType == "image/png")
                        {
                            var docPath = StaticData.GetFilePath();

                            if (!Directory.Exists(docPath))
                                Directory.CreateDirectory(docPath);

                            if (!Directory.Exists(docPath + "\\" + path))
                                Directory.CreateDirectory(docPath + "\\" + path);

                            string docOrgName = file.FileName;
                            var docExt = Path.GetExtension(docOrgName);

                        docName:
                            var docName = DateTime.Now.Ticks + docExt;
                            docName = @"\" + path + "\\" + docName;
                            var docToCreate = docPath + "\\" + docName;

                            if (System.IO.File.Exists(docToCreate))
                                goto docName;

                            if (!string.IsNullOrEmpty(docName))
                            {
                                file.SaveAs(docToCreate);
                                response.ErrorCode = 0;
                                response.Message = docName;
                            }
                        }
                    }
                    else
                    {
                        response.ErrorCode = 1;
                        response.Message = "Invalid file format";
                        return response;
                    }
                }
                catch (Exception e)
                {
                    response.ErrorCode = 1;
                    response.Message = e.Message;
                }
            }
            return response;
        }

        public static string GetActions(string Control, Int64 Id, string ExtraId = "", string AddEdit = "")
        {
            var link = "";
            if (HasRight(Control, AddEdit))
            {
                var enc = Base64Encode_URL(ExtraId.ToString());
                if (Control.ToLower() == "taskmanager")
                {

                    link += "<a href='/" + Control + "/" +AddEdit +"?id=" + enc + "' class='btn-action' title='Edit'><i class='mdi mdi-pencil'></i></a>";
                }
                else if (Control.ToLower() == "member")
                {

                    link += "<div style='display:flex;justify-content:space-around;'><a href='/" + Control + "/" + AddEdit + "?id=" + enc + "' class='btn-action' title='Edit'><i class='mdi mdi-pencil'></i></a>";

                    link += "<a href='/" + Control + "/DeleteUser" +"?id=" + enc + "' class='btn-action' title='Delete'><i class='mdi mdi-delete'></i></a></div>";
                }

            }

            return link;
        }
        public static string GetSubActions(string Control, int Id, string ParentId = "")
        {
            var link = "";

            if (HasRight(Control, "d"))
            {
                //link += "<a href='/" + Control + "/SubDelete/" + Id + "' onclick=' return ConfirmDelete();'  class='btn-action'><i class='mdi mdi-remove'></i></a>";
            }
            if (HasRight(Control, "a"))
            {
                link += "<a href='/" + Control + "/SubManage/?id=" + ParentId + "&rowId=" + Id + "' class='btn-action' title='Edit'><i class='mdi mdi-pencil'></i></a>";
            }
            if (Control.ToLower() == "branch" && !string.IsNullOrWhiteSpace(ParentId))
            {
                link += "<a href='/" + Control + "/SubList/" + Id + "'  class='btn-action' title='Sub List'><i class='mdi mdi-settings'></i></a>";
            }
            else if (Control.ToLower() == "branch")
            {
                link += "<a href='/" + Control + "/BM/" + Id + "'  class='btn-action' title='BM Setting'><i class='mdi mdi-account'></i></a>";
            }
            else if (Control.ToLower() == "staticdata")
            {
                link += "<a href='/" + Control + "/Manage/?id" + Id + "&code='" + ParentId + "  class='btn-action' title='Edit'><i class='mdi mdi-pencil'></i></a>";
            }
            return link;
        }
        public static bool HasRight(string ControllerName, string Action)
        {
            if (GetUser() == "admin")
            {
                return true;
            }
            if (string.IsNullOrWhiteSpace(ControllerName))
                return false;

            var user = ReadSession("UserName", "");
            return MVCERP.Web.Library.UserMonitor.GetInstance().HasRight(user, ControllerName, Action);
        }
        public static bool CheckFunctionId(string ControllerName, string Action = "V")
        {
            CheckSession();

            long number1 = 0;
            bool canConvert = long.TryParse(ControllerName, out number1);
            if (canConvert)
            {
                Action = ControllerName;
            }
            //return true;
            if (!HasRight(ControllerName, Action))
            {
                var log = new MVCERP.Shared.Common.ErrorLogsCommon()
                {
                    Action = "UnAuthorization",
                    ErrorPage = ControllerName,
                    ErrorMsg = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"],
                    ErrorDetail = HttpContext.Current.Request.Browser.Browser + " Version :" + HttpContext.Current.Request.Browser.Version,
                    User = GetUser()
                };
                var buss = new Business.Business.Logs.LogsBusiness();
                var response = buss.UnAuthorizedLog(log);

                //IF INVALID ATTEMPT COUNT EXCEED THE LIMIT AUTO SEND MAIL TO HO AND BLOCK THE USER
                if (response.ErrorCode.Equals(1))
                {
                    if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                    {
                        HttpContext.Current.Response.Redirect("/Home?log=" + response.Id);
                    }
                }
                else
                {
                    if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                    {
                        HttpContext.Current.Response.Redirect("/UnAuthorized");
                    }
                }
                // return "/UnAuthorized";
            }
            return true;
        }
        public static string ReadSession(string key, string defVal)
        {
            try
            {
                var User = HttpContext.Current.Session[key] == null ? defVal : HttpContext.Current.Session[key].ToString();
                return User;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void WriteSession(string key, string value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public static void RemoveSession(string key)
        {
            if (HttpContext.Current.Session[key] == null)
            {
                return;
            }
            HttpContext.Current.Session.Remove(key);
        }

        public static void DeleteCookie(string key)
        {
            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                var aCookie = new HttpCookie(key);
                aCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(aCookie);
            }
        }

        internal static void LogError(HttpException Apperr, string page)
        {
            Exception err = Apperr;
            if (Apperr.InnerException != null)
                err = Apperr.InnerException;

            var errPage = FilterString(page);
            var errMsg = FilterString(err.Message);
            var errDetails = FilterString(Apperr.GetHtmlErrorMessage());

            var log = new MVCERP.Shared.Common.ErrorLogsCommon()
            {
                ErrorPage = errPage,
                ErrorMsg = errMsg,
                ErrorDetail = errDetails,
                User = GetUser()
            };

            var buss = new Business.Business.StaticData.StaticDataBusiness();
            var response = buss.InsertErrorLog(log);

            // Send internal mail to developer
            if (response.ErrorCode.Equals(1))
            {
                if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                {
                    HttpContext.Current.Response.Redirect("/Home?log=" + response.Id);
                }
            }
            else
            {
                if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                {
                    HttpContext.Current.Response.Redirect("/Error?id=" + response.Id);
                }
            }
        }

        internal static void LogError(string errorMessage, string page)
        {
            var errPage = FilterString(page);
            var errMsg = FilterString(errorMessage);
            var errDetails = FilterString(errorMessage);

            var log = new MVCERP.Shared.Common.ErrorLogsCommon()
            {
                ErrorPage = errPage,
                ErrorMsg = errMsg,
                ErrorDetail = errDetails,
                User = GetUser()
            };

            var buss = new Business.Business.StaticData.StaticDataBusiness();
            var response = buss.InsertErrorLog(log);

            // Send internal mail to developer
            if (response.ErrorCode.Equals(1))
            {
                if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                {
                    HttpContext.Current.Response.Redirect("/Home?log=" + response.Id);
                }
            }
            else
            {
                if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                {
                    HttpContext.Current.Response.Redirect("/Error?id=" + response.Id);
                }
            }
        }

        public static string ReadQueryString(string key, string defVal)
        {
            return HttpContext.Current.Request.QueryString[key] ?? defVal;
        }
        public static String FilterString(string strVal)
        {
            var str = FilterQuote(strVal);

            if (str.ToLower() != "null")
                str = "'" + str + "'";

            return str.TrimEnd().TrimStart();
        }

        public static String FilterQuote(string strVal)
        {
            if (string.IsNullOrEmpty(strVal))
            {
                strVal = "";
            }
            var str = strVal.Trim();

            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace(";", "");
                //str = str.Replace(",", "");
                str = str.Replace("--", "");
                str = str.Replace("'", "");

                str = str.Replace("/*", "");
                str = str.Replace("*/", "");

                str = str.Replace(" select ", "");
                str = str.Replace(" insert ", "");
                str = str.Replace(" update ", "");
                str = str.Replace(" delete ", "");

                str = str.Replace(" drop ", "");
                str = str.Replace(" truncate ", "");
                str = str.Replace(" create ", "");

                str = str.Replace(" begin ", "");
                str = str.Replace(" end ", "");
                str = str.Replace(" char(", "");

                str = str.Replace(" exec ", "");
                str = str.Replace(" xp_cmd ", "");


                str = str.Replace("<script", "");

            }
            else
            {
                str = "null";
            }
            return str;
        }


        internal static object GetReportPagesize()
        {
            return "100";
        }

        internal static object GetSessionId()
        {
            return HttpContext.Current.Session.SessionID;
        }

        public static long ReadNumericDataFromQueryString(string key)
        {
            var tmpId = ReadQueryString(key, "0");
            long tmpIdLong;
            long.TryParse(tmpId, out tmpIdLong);
            return tmpIdLong;
        }
        public static string ParseMinusValue(double data)
        {
            var retVal = Math.Abs(data).ToString("N");
            if (data < 0)
            {
                return "(" + retVal + ")";
            }
            return retVal;

        }

        public static string ParseMinusValue(string data)
        {
            var m = ParseDouble(data);

            return ParseMinusValue(m);
        }
        public static double ParseDouble(string value)
        {
            double tmp;
            double.TryParse(value, out tmp);
            return tmp;
        }
        public static int ParseInt(string value)
        {
            int tmp;
            int.TryParse(value, out tmp);
            return tmp;
        }
        public static String ShowDecimalWithMinus(String strVal)
        {
            if (strVal != "" && strVal != null)
                strVal = String.Format("{0:0,0.00}", double.Parse(strVal));

            return ParseMinusValue(strVal);
        }
        public static String ShowDecimal(String strVal)
        {
            if (strVal != "" && strVal != null)
                return String.Format("{0:0,0.00}", double.Parse(strVal));
            else
                return strVal;
        }

        public static String ShowDecimal2(String strVal)
        {
            if (strVal != "" && strVal != null)
                return String.Format("{0:F}", double.Parse(strVal));
            else
                return strVal;
        }

        public static String ShowAbsDecimal(String strVal)
        {
            if (strVal != "" && strVal != null)
            {
                strVal = Math.Abs(ParseDouble(strVal)).ToString();
                return String.Format("{0:0,0.00}", double.Parse(strVal));
            }
            else
                return strVal;
        }
        public static List<SelectListItem> SetDDLByTable(DataTable dt, string selectedVal, string defLabel = "", bool IsTextAsValue = false)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            if (!string.IsNullOrWhiteSpace(defLabel))
            {
                items.Add(new SelectListItem { Text = defLabel, Value = "" });
            }
            if (dt.Rows.Count > 0)
            {
                int val = 0;
                if (IsTextAsValue)
                    val = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][val].ToString() == selectedVal)
                    {
                        items.Add(new SelectListItem { Text = dt.Rows[i][1].ToString(), Value = dt.Rows[i][val].ToString(), Selected = true });
                    }
                    else
                    {
                        items.Add(new SelectListItem { Text = dt.Rows[i][1].ToString(), Value = dt.Rows[i][val].ToString() });
                    }
                }
            }
            return items;
        }

        private static string SaltKey = "23472@asd";
        public static string Base64Encode(string plainText)
        {
            plainText = plainText + SaltKey;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                base64EncodedData = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                base64EncodedData = base64EncodedData.Replace(SaltKey, "");
            }
            catch (Exception e)
            {
                base64EncodedData = "";
            }

            return base64EncodedData;
        }
        static string salt1 = "&%$%#@#";
        static string salt2 = "@$^#%^";
        public static string Base64Encode_URL(string plainText)
        {
            var enc = "";
            try
            {
                plainText = salt1 + plainText + salt2;
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                enc = System.Convert.ToBase64String(plainTextBytes);
                enc = enc.Replace("=", "000");
            }
            catch (Exception)
            {
                enc = "";
            }

            return enc;
        }
        public static string Base64Decode_URL(string base64EncodedData)
        {
            if (base64EncodedData == "Index" || string.IsNullOrWhiteSpace(base64EncodedData))
            {
                return "";
            }
            try
            {
                base64EncodedData = base64EncodedData.Replace("000", "=");
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                base64EncodedData = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                base64EncodedData = base64EncodedData.Replace(salt1, "");
                base64EncodedData = base64EncodedData.Replace(salt2, "");
            }
            catch (Exception e)
            {
                base64EncodedData = "";
            }

            return base64EncodedData;
        }
        public static string GetIdFromQuery()
        {
            string getEnc = "";
            if (HttpContext.Current.Request.QueryString.Count > 0)
            {
                getEnc = HttpContext.Current.Request.QueryString[0];
            }

            return StaticData.Base64Decode_URL(getEnc);
        }
        public static string GetFilePath()
        {
            return ReadWebConfig("filePath", "");
        }
        public static string OnlineGetFilePath()
        {
            return ReadWebConfig("OnlinefilePath", "");
        }
        public static string GetUrlRoot()
        {
            return ReadWebConfig("urlRoot", "");
        }

        public static string GetPosition(string value)
        {
            value = value.Replace("1", "First");
            value = value.Replace("2", "Second");
            value = value.Replace("3", "Third");
            value = value.Replace("4", "Fourth");
            value = value.Replace("5", "Fifth");
            value = value.Replace("6", "Sixth");
            value = value.Replace("7", "Seventh");
            value = value.Replace("8", "Eighth");
            value = value.Replace("9", "Ninth");
            value = value.Replace("10", "Tenth");

            return value;
        }
        public static string NumberInNepali(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            var Num = "";
            for (int i = 0; i < value.Length; i++)
            {
                var result = value.Substring(i, 1);
                result = result.Replace("0", "०");
                result = result.Replace("1", "१");
                result = result.Replace("2", "२");
                result = result.Replace("3", "३");
                result = result.Replace("4", "४");
                result = result.Replace("5", "५");
                result = result.Replace("6", "६");
                result = result.Replace("7", "७");
                result = result.Replace("8", "८");
                result = result.Replace("9", "९");
                Num += result;
            }

            return Num;
        }
        public static bool CheckSession()
        {
            string user = GetUser();
            if (string.IsNullOrEmpty(user))
            {
                HttpContext.Current.Response.Redirect("/Home?log=SessionExpired");
                return false;
            }
            return true;
        }
        public static string MakeXmlByComa(string value)
        {
            string val = "<root>";
            foreach (var item in value.Split(','))
            {
                val += @"<row id=""" + item + "\" />";
            }
            val += "</root>";
            return val;
        }

        public static string GetNepaliToADDate(string BSDate)
        {
            var conversionfailed = BSDate.Split('/')[2];
            if(conversionfailed.ToInt()>32)
            {
                return "";
            }

            var adDate = DipeshNepaliDateConversion.DipeshNepaliDateConverter.convertToAD(BSDate).ToShortDateString();
   
            return adDate;
        }
        public static String NepaliNumberSystem(String strVal)
        {
            if (strVal != "" && strVal != null)
            {
                CultureInfo hindi = new CultureInfo("hi-IN");
                string num = string.Format(hindi, "{0:c}", double.Parse(strVal));
                num = num.Replace("₹", "");
                return num;
                //return String.Format("{0:0,0.00}", double.Parse(strVal));
            }
            else
                return strVal;
        }
        #region company Header
        public static string GetCompanyHeader()
        {
            string header = ReadSession("CompanyName", "");
            return header;
        }
        public static string CompanyAddress()
        {
            string header = ReadSession("CompanyAddress", "");
            return header;
        }
        public static string GetCompanyHeaderNep()
        {
            string header = ReadSession("CompanyNameNep", "");
            return header;
        }
        public static string CompanyAddressNep()
        {
            string header = ReadSession("CompanyAddressNep", "");
            return header;
        }
        #endregion

        #region FUNCTION IN NEPALI
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Accept date from DB in MM/dd/yyyy</param>
        /// <returns>returns Date in: dd/MM/yyyy</returns>
        public static string DBToFrontDate_Nep(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            try
            {
                string sysFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                if (sysFormat.ToLower().Contains("dd/mm/yyyy"))
                {
                    return value.Substring(0, 10);
                }

                var validDate = value.Split(' ');
                string[] date = validDate[0].Split('/');
                var dt = (date[1].Length == 1 ? "0" + date[1] : date[1]) + "/" + (date[0].Length == 1 ? "0" + date[0] : date[0]) + "/" + date[2];
                return NumberInNepali(dt);
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static string DBToFrontDateTime_Nep(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            try
            {
                string dateOnly = DBToFrontDate_Nep(value);
                string[] list = value.Split(' ');
                var time = "";
                var total = "";
                var size = list.Length;
                if (size > 1)
                {
                    total = dateOnly + " " + NumberInNepali(list[1]) + " " + list[2];

                }



                return total;
            }
            catch (Exception e)
            {
                return "";
            }

        }

        private static string[] HundredHindiDigitArray = {"", "एक", "दुई", "तीन", "चार", "पाँच", "छ", "सात", "आठ", "नौ", "दस",
    "एघार", "बाह्र", "तेह्र", "चौध", "पन्ध्र", "सोह्र", "सत्र", "अठार", "उन्नाइस", "बीस",
    "एक्काइस", "बाईस", "तेईस", "चौबीस", "पच्चीस", "छब्बीस", "सत्ताईस", "अठ्ठाईस", "उनन्तिस", "तीस", 
    "इकतीस", "बत्तीस", "तैंतीस", "चौंतीस", "पैंतीस", "छत्तीस", "सैंतीस", "अड़तीस", "उनन्चालीस", "चालीस", 
    "एकचालीस", "बयालीस", "त्रियालीस", "चवालीस", "पैँतालीस", "छयालीस", "सच्चालीस", "अठचालीस", "उनन्चास", "पचास", 
    "एकाउन्न", "बाउन्न", "त्रिपन्न", "चउन्न", "पचपन्न", "छपन्न", "सन्ताउन्न", "अन्ठाउन्न", "उनन्साठी", "साठी", 
    "एकसट्ठी", "बयसट्ठी", "त्रिसट्ठी", "चौंसट्ठी", "पैंसट्ठी", "छयसट्ठी", "सतसट्ठी", "अठसट्ठी", "उनन्सत्तरी", "सत्तरी", 
    "एकहत्तर", "बहत्तर", "त्रिहत्तर", "चौहत्तर", "पचहत्तर", "छयहत्तर", "सतहत्तर", "अठहत्तर", "उनासी", "असी", 
    "एकासी", "बयासी", "त्रियासी", "चौरासी", "पचासी", "छयासी", "सतासी", "अठासी", "उनान्नब्बे", "नब्बे", 
    "एकान्नब्बे", "बयानब्बे", "त्रियान्नब्बे", "चौरान्नब्बे", "पन्चानब्बे", "छयान्नब्बे", "सन्तान्नब्बे", "अन्ठान्नब्बे", "उनान्सय"};

        private static string[] HigherDigitHindiNumberArray = { "", "", "सय", "हजार", "लाख", "करोड़", "अरब", "खरब", "नील" };
        private static string[] HigherDigitSouthAsianStringArray = { "", "", "Hundred", "Thousand", "Lakh", "Karod", "Arab", "Kharab", "Neel" };

        private static string[] SouthAsianCodeArray = { "1", "22", "3", "4", "42", "5", "52", "6", "62", "7", "72", "8", "82", "9", "92" };
        private static string[] EnglishCodeArray = { "1", "22", "3" };

        private static string[] SingleDigitStringArray = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" };
        private static string[] DoubleDigitsStringArray = { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        private static string[] TenthDigitStringArray = { "Ten", "Eleven", "Tweleve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

        public  static string NumberInNepaliWords(string AmtInFigure)
        {
            if (string.IsNullOrWhiteSpace(AmtInFigure))
                AmtInFigure = "0";

            AmtInFigure = AmtInFigure.Replace(".00", "");
            int Amount = 0;
            try
            {
                Amount = int.Parse(AmtInFigure);
            }
            catch (Exception e)
            {
                Amount = 0;
            }
            string amountString = Amount.ToString();
            if (Amount == 0)
            {
                return "शून्य";// 'Unique and exceptional case
            }
            if (amountString.Length > 15)
            {
                return "That's too long...";
            }
            int[] amountArray = new int[amountString.Length];
            for (int i = 0; i < amountString.Length; i++)
            {
                amountArray[i] = int.Parse(amountString.Substring(i,1));
            }

            //string[] amountArray = amountString.ToStringA();

            int j, digit = 0;
            string result = "", separator = "", higherDigitHindiString = "", codeIndex = "";

            for (int i = amountArray.Length; i >0; i--)
            {
                j = amountArray.Length - i;
                digit = amountArray[j];
                codeIndex = SouthAsianCodeArray[i-1];
                higherDigitHindiString = HigherDigitHindiNumberArray[int.Parse(codeIndex.Substring(0, 1)) - 1];
                

                if (codeIndex == "1")
                {
                    result = result + separator + HundredHindiDigitArray[digit];
                }
                else if (codeIndex.Length == 2 && digit != 0)
                {
                    int suffixDigit = amountArray[j + 1];
                    int wholeTenthPlaceDigit = digit * 10 + suffixDigit;
                    result = result + separator + HundredHindiDigitArray[wholeTenthPlaceDigit] + " " + higherDigitHindiString;
                    i -= 1;
                }
                else if (digit != 0)
                {
                    result = result + separator + HundredHindiDigitArray[digit] + " " + higherDigitHindiString;
                }
                separator = " ";
            }

            return RemoveSpaces(result);
        }
        private static string RemoveSpaces(string word)
        {
            var regEx = new System.Text.RegularExpressions.Regex("  ");
            return regEx.Replace(word, " ").Trim();
        }
        public static string Month_Nep(string month)
        {
            string NepMonth = "";
            switch (month.ToLower())
            {
                case "january":
                    NepMonth = "जनवरी";
                    break;
                case "february":
                    NepMonth = "फेब्रुअरी";
                    break;
                case "march":
                    NepMonth = "मार्च";
                    break;
                case "april":
                    NepMonth = "अप्रिल";
                    break;
                case "may":
                    NepMonth = "मे";
                    break;
                case "june":
                    NepMonth = "जून";
                    break;
                case "july":
                    NepMonth = "जुलाई";
                    break;
                case "august":
                    NepMonth = "अगष्ट";
                    break;
                case "september":
                    NepMonth = "सेप्टेम्बेर";
                    break;
                case "october":
                    NepMonth = "अक्टुवर";
                    break;
                case "november":
                    NepMonth = "नोवेम्बेर";
                    break;
                case "december":
                    NepMonth = "डिसेम्बर";
                    break;
                default:
                    break;
            }

            return NepMonth;
        }
        #endregion
        public static DataTable ToDataTable<T>(List<T> response)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in response)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
        public static void GetClosedXmlExcelSheet(TaskReportingCommon reportComponent)
        {
            //reportComponent.ReportTitle = "aa";
            string documentPath = ConfigurationManager.AppSettings["documentFilePath"];
            var wb = new XLWorkbook();
            // Add a DataTable as a worksheet
            string title = reportComponent.ReportTitle + " From " + reportComponent.TaskStartDate + " To " +
                            reportComponent.TaskEndDate;
            var ws = wb.Worksheets.Add(reportComponent.ReportData, reportComponent.ReportTitle);
            ws.Row(1).InsertRowsAbove(1);
            ws.Cell("A1").Value = title;
            ws.Range("A1:K1").Row(1).Merge();
            var fileNameWithPath = documentPath + "\\" + StaticData.GetUser() + "_" + reportComponent.TaskName + ".xlsx";
            if (System.IO.File.Exists(fileNameWithPath))
            {
                try
                {
                    System.IO.File.Delete(fileNameWithPath);
                }
                catch (Exception ex)
                {
                    //Do something
                }
            }
            string fileUrl = StaticData.GetUrlRoot() + "/Handler/FileHandler.ashx?file=" + StaticData.GetUser() + "_" + reportComponent.TaskName + ".xlsx";
            reportComponent.ExcelLink = fileUrl;
            wb.SaveAs(fileNameWithPath);
        }
    }
}
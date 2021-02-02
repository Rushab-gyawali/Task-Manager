using MVCERP.Web.Library;
using System;
using System.IO;
using System.Web;

namespace MVCERP.Web
{
    /// <summary>
    /// Summary description for img
    /// </summary>
    public class img : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var flag = context.Request.QueryString["flag"];
                var id = context.Request.QueryString["id"];
                id = id.Replace("'", "");

                var imgPath = "";
                if(flag=="Online")
                {
                     imgPath = StaticData.OnlineGetFilePath() + @"\na.gif";
                }
                else
                { 
                 imgPath = StaticData.GetFilePath() + @"\na.gif";
                }
                    var primaryFilePath = "";
                id = id.ToLower();
                
               
                if (flag == "agm")
                {
                    id = id.Replace(@"agmdocs\", "");
                    primaryFilePath = StaticData.GetFilePath() + @"AgmDocs\" + id;
                }
                else if (flag == "Online")
                {
                    primaryFilePath = StaticData.OnlineGetFilePath() + id;
                }
                else if (flag == "company") // company logo
                {
                    id = id.Replace(@"company\", "");
                    primaryFilePath = StaticData.GetFilePath() + @"Company\" + id;
                }
                else if (flag == "receipt")
                {
                    id = id.Replace(@"receiptimages\", "");
                    primaryFilePath = StaticData.GetFilePath() + @"ReceiptImages\" + id;
                }
                else if (flag == "document")
                {
                    id = id.Replace(@"documentimage\", "");
                    primaryFilePath = StaticData.GetFilePath() + @"DocumentImage\" + id;
                }
                else if (flag == "bonddoc")
                {
                    id = id.Replace(@"bonddocsimages\", "");
                    primaryFilePath = StaticData.GetFilePath() + @"BondDocsImages\" + id;
                }
                else if (flag == "SharePurchase")
                {
                    id = id.Replace(@"sharepurchase\", "");
                    primaryFilePath = StaticData.GetFilePath() + @"SharePurchase\" + id;
                }

                else if (flag == "devDoc")
                {
                    id = id.Replace(@"invdebenture\", "");
                    primaryFilePath = StaticData.GetFilePath() + @"InvDebenture\" + id;
                }
                else if (flag == "fd")
                {
                    id = id.Replace(@"fixeddeposit\", "");
                    primaryFilePath = StaticData.GetFilePath() + @"FixedDeposit\" + id;
                }
                else
                {
                    primaryFilePath = StaticData.GetFilePath() + @"\" + id;
                }
                if (File.Exists(primaryFilePath))
                {
                    imgPath = primaryFilePath;
                }
                var ext = Path.GetExtension(imgPath);
                if (ext == ".pdf")
                {

                    context.Response.Clear();
                    context.Response.ContentType = "application/pdf";
                    context.Response.WriteFile(imgPath);
                }
                else
                {
                    context.Response.ContentType = "image/png";
                    context.Response.WriteFile(imgPath);
                }

            }
            catch (Exception ex)
            {
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
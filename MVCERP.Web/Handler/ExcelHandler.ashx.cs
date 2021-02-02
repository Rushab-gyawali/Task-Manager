using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace MVCERP.Web.Handler
{
    /// <summary>
    /// Summary description for ExcelHandler
    /// </summary>
    public class ExcelHandler : IHttpHandler
    {
        private string filePath = ConfigurationManager.AppSettings["filePath"];
        
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var fileName = context.Request.QueryString["file"];
                var fileFullPath = filePath + @"\" + fileName;
                FileInfo fileToDownload = new FileInfo(fileFullPath);
                context.Response.Flush();
                context.Response.WriteFile(fileToDownload.FullName);
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
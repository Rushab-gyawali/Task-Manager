using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCERP.Web.Library
{
    public class ProjectGrid
    {
        //public static string MakeGrid(List<object> list, string ColtrolerName)
        public static IDictionary<string, string> column { get; set; }
        //public static string DateField  { get; set; }
        //public static int Pagesize  { get; set; }
        public static string MakeGrid<T>(List<T> obj, string ControlerName, string Search, int Pagesize, bool allowAdd = true, string DateField = "", string RowId = "", string breadcrumb1 = "", string breadcrumb2 = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<form id='form' method=\"get\" action='/" + ControlerName + "'>");
            sb.AppendLine("<div class=\"page-header\">");
            if (ControlerName.ToLower() != "hidebreadcrumb")
            {
                sb.AppendLine("<div class=\"row\">");
                sb.AppendLine("<div class=\"col-sm-6\">");
                //sb.AppendLine("<h1>Agency List</h1>");
                sb.AppendLine("<ol class=\"breadcrumb\">");
                sb.AppendLine(string.Format("<li><a href=\"#\">{0}</a></li>", (breadcrumb1 == "" ? "Home" : breadcrumb1)));
                sb.AppendLine(string.Format("<li><a href=\"#\">{0}</a></li>", (breadcrumb2 == "" ? "Management" : breadcrumb2)));
                sb.AppendLine("<li class=\"active\">" + ControlerName + " List</li>");
                sb.AppendLine("</ol>");
                sb.AppendLine("</div>");
                sb.AppendLine("<div class=\"col-sm-6\">");
                sb.AppendLine("<div class=\"form-group form-action\">");
                sb.AppendLine("<button class=\"btn btn-default\" onclick=\"GoBack();\" ><i class=\"mdi mdi-arrow-left\"></i> Back</button>");
                //sb.AppendLine("<button class=\"btn btn-info\"><i class=\"mdi mdi-view-list\"></i> View List</button>");
                //sb.AppendLine("<button class=\"btn btn-danger\"><i class=\"mdi mdi-delete\"></i> Delete</button>");
                if (allowAdd)
                {
                    var enc = StaticData.Base64Encode_URL(RowId);
                    if (ControlerName.ToLower() == "branch")
                    {
                        sb.AppendLine("<a href=\"/" + ControlerName + "/SubManage?id=" + RowId + "\" class=\"btn btn-success\" ><i class=\"mdi mdi-plus\"></i> Add New</a>");
                    }
                    else if (ControlerName.ToLower() == "staticdata")
                    {
                        sb.AppendLine("<a href=\"/" + ControlerName + "/SubManage?id=" + enc + "\" class=\"btn btn-success\" ><i class=\"mdi mdi-plus\"></i> Add New</a>");
                    }
                    else if (ControlerName.ToLower() == "agent")
                    {
                        sb.AppendLine("<a href=\"/" + ControlerName + "/NewAgent?" + enc + "\" class=\"btn btn-success\" ><i class=\"mdi mdi-plus\"></i> Add New</a>");
                    }
                    else if (ControlerName.ToLower() == "fppolicydetail")
                    {
                        sb.AppendLine("<a href=\"/" + ControlerName + "/Manage\" class=\"btn btn-success\" ><i class=\"mdi mdi-plus\"></i> Add New</a>");
                        sb.AppendLine("<a href=\"/" + ControlerName + "/Upload\" class=\"btn btn-success\" ><i class=\"mdi mdi-plus\"></i> Upload File</a>");
                        sb.AppendLine("<a href='#' data-toggle='modal' data-target='#PolicyPrint' title='Policy Print' class=\"btn btn-success\"> Bulk Policy Print</a>");

                    }
                    else if (ControlerName.ToLower() == "planrate")
                    {
                        sb.AppendLine("<a href=\"/" + ControlerName + "/Manage\" class=\"btn btn-success\" ><i class=\"mdi mdi-plus\"></i> Add New</a>");
                        sb.AppendLine("<a href=\"/" + ControlerName + "/Upload\" class=\"btn btn-success\" ><i class=\"mdi mdi-plus\"></i> Upload File</a>");
                    }
                    else if (ControlerName.ToLower() == "riderrate")
                    {
                        sb.AppendLine("<a href=\"/" + ControlerName + "/Manage\" class=\"btn btn-success\" ><i class=\"mdi mdi-plus\"></i> Add New</a>");
                        sb.AppendLine("<a href=\"/" + ControlerName + "/Upload\" class=\"btn btn-success\" ><i class=\"mdi mdi-plus\"></i> Upload File</a>");
                    }
                    else if (ControlerName.ToLower() == "grouppolicy")
                    {
                        sb.AppendLine("<a href=\"/" + ControlerName + "/Manage\" class=\"btn btn-success\" ><i class=\"mdi mdi-plus\"></i> Add New</a>");
                        sb.AppendLine("<a href=\"/" + ControlerName + "/GroupInformation\" class=\"btn btn-success\" ><i class=\"mdi mdi-plus\"></i> Add Group Info</a>");
                        sb.AppendLine("<a href=\"/" + ControlerName + "/GroupInformationIndex\" class=\"btn btn-success\" >Show Group</a>");
                        sb.AppendLine("<a href=\"/" + ControlerName + "/Upload\" class=\"btn btn-success\" ><i class=\"mdi mdi-plus\"></i> Upload File</a>");
                    }
                    else if (ControlerName.ToLower() == "grouppolicy/groupinformationindex")
                    {
                        sb.AppendLine("<a href=\"/GroupPolicy/GroupInformation\" class=\"btn btn-success\" ><i class=\"mdi mdi-plus\"></i> Add New</a>");
                       
                    }
                    else
                    {
                        sb.AppendLine("<a href=\"/" + ControlerName + "/Manage\" class=\"btn btn-success\" ><i class=\"mdi mdi-plus\"></i> Add New</a>");
                    }

                }
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
            }
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"panel panel-default\">");
            sb.AppendLine("<div class=\"panel-heading list-heading\">");
            sb.AppendLine("<div class=\"row\">");
            sb.AppendLine("<div class=\"col-xs-6\">");
            sb.AppendLine("<div class=\"form-group\">");
            sb.AppendLine("<div class=\"input-group input-search\">");
            sb.AppendLine("<input type=\"text\" id='Search' value='" + Search + "' name='Search' class=\"form-control\" placeholder=\"Type here to Search....\">");
            sb.AppendLine("<div class=\"input-group-btn\">");
            sb.AppendLine("<button class=\"btn btn-default\" onclick=\"FilterForm();\">");
            sb.AppendLine("<i class=\"mdi mdi-magnify\"></i> Search");
            sb.AppendLine("</button>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            //sb.AppendLine("<div class=\"col-xs-6\">");
            //sb.AppendLine("<div class=\"form-group form-filter\">");
            //sb.AppendLine("<button onclick=\"FilterForm();\" class=\"btn btn-info\"><i class=\"mdi mdi-view-list\"></i> Filter</button>");
            //sb.AppendLine("<a data-toggle=\"collapse\" href=\"#formFilter\" class=\"btn btn-sm btn-default\">");
            //sb.AppendLine("<i class=\"mdi mdi-filter\"></i>");
            //sb.AppendLine("Filter");
            //sb.AppendLine("<span class=\"caret\"></span>");
            //sb.AppendLine("</a>");
            //sb.AppendLine("</div>");
            //sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"col-xs-6\">");
            sb.AppendLine("<div class=\"form-inline form-show\">");
            sb.AppendLine("<div class=\"form-group\">");
            sb.AppendLine("<label>Show</label>");
            sb.AppendLine("<select id='Pagesize' name='Pagesize' class=\"form-control input-sm\" onchange=\"FilterForm();\" >");
            sb.AppendLine("<option " + (Pagesize == 10 ? "selected" : "") + ">10</option>");
            sb.AppendLine("<option " + (Pagesize == 25 ? "selected" : "") + ">25</option>");
            sb.AppendLine("<option " + (Pagesize == 50 ? "selected" : "") + ">50</option>");
            sb.AppendLine("<option " + (Pagesize == 75 ? "selected" : "") + ">75</option>");
            sb.AppendLine("<option " + (Pagesize == 100 ? "selected" : "") + ">100</option>");
            //---------

            sb.AppendLine("<option " + (Pagesize == 250 ? "selected" : "") + ">250</option>");
            sb.AppendLine("<option " + (Pagesize == 500 ? "selected" : "") + ">500</option>");
            sb.AppendLine("<option " + (Pagesize == 1000 ? "selected" : "") + ">1000</option>");
            sb.AppendLine("<option " + (Pagesize == 1500 ? "selected" : "") + ">1500</option>");
            sb.AppendLine("<option " + (Pagesize == 2000 ? "selected" : "") + ">2000</option>");
            sb.AppendLine("<option " + (Pagesize == 2500 ? "selected" : "") + ">2500</option>");

            //---------
            sb.AppendLine("</select>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"panel-body list-body\">");
            //sb.AppendLine("<div class=\"collapse\" id=\"formFilter\">");
            //sb.AppendLine("<div class=\"well well-filter\">");
            //sb.AppendLine("<div class=\"row\">");
            //sb.AppendLine("<div class=\"col-md-4\">");
            //sb.AppendLine("<div class=\"form-group\">");
            //sb.AppendLine("<label>Material</label>");
            //sb.AppendLine("<input type=\"text\" class=\"form-control input-sm\">");
            //sb.AppendLine("</div>");
            //sb.AppendLine("</div>");
            //sb.AppendLine("<div class=\"col-md-4\">");
            //sb.AppendLine("<div class=\"form-group\">");
            //sb.AppendLine("<label>Display</label>");
            //sb.AppendLine("<input type=\"text\" class=\"form-control input-sm\">");
            //sb.AppendLine("</div>");
            //sb.AppendLine("</div>");
            //sb.AppendLine("</div>");
            //sb.AppendLine("<div class=\"row\">");
            //sb.AppendLine("<div class=\"col-md-4\">");
            //sb.AppendLine("<div class=\"form-group\">");
            //sb.AppendLine("<input type=\"button\" class=\"btn btn-sm btn-info\" value=\"Filter\">");
            ////sb.AppendLine("<input type=\"button\" class=\"btn btn-sm btn-default\" value=\"Clear\">");
            //sb.AppendLine("</div>");
            //sb.AppendLine("</div>");
            //sb.AppendLine("</div>");
            //sb.AppendLine("</div>");
            //sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"table-responsive\">");
            sb.AppendLine("<table class=\"table table-hover\">");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>S.N</th>");
            foreach (var item in column)
            {
                sb.AppendLine("<th>" + item.Value + "</th>");
                //sql += ", @" + item.Key + " = " + wDao.FilterString(item.Value);
            }
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");

            int SN = 0;

            foreach (var item in obj)
            {
                SN++;
                int num = 0;
                sb.AppendLine("<tr>");
                sb.AppendLine("<td>" + SN + "</td>");
                Type t = item.GetType();
                foreach (var col in column)
                {
                    num++;
                    var value = item.GetType().GetProperty(col.Key).GetValue(item, null);
                    if (!string.IsNullOrWhiteSpace(DateField) && DateField.Split('|').Contains(num.ToString()))
                    {
                        if (null != value)
                        {
                            value = StaticData.DBToFrontDate(value.ToString());
                            value = (string.IsNullOrWhiteSpace(value.ToString()) ? "" : value.ToString().Substring(0, 10));
                        }
                    }
                    if (col.Key.Contains("IsActive") && null != value)
                    {
                        value = (value.ToString() == "True" ? "Yes" : (value.ToString() == "1" ? "Yes" : "No"));
                        //value = (value.ToString()=="1" ? "Yes" : "No");
                    }
                    sb.AppendLine("<td>" + value + "</td>");
                }
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"panel-footer\">");
            sb.AppendLine("<div class=\"row\">");
            sb.AppendLine("<div class=\"col-md-8\">");
            sb.AppendLine("<ul class=\"pagination listing-pagination hide\">");
            sb.AppendLine("<li>");
            sb.AppendLine("<a href=\"#\">");
            sb.AppendLine("<i class=\"mdi mdi-arrow-left\"></i>");
            sb.AppendLine("</a>");
            sb.AppendLine("</li>");
            sb.AppendLine("<li><a href=\"#\">1</a></li>");
            sb.AppendLine("<li><a href=\"#\">2</a></li>");
            sb.AppendLine("<li><a href=\"#\">3</a></li>");
            sb.AppendLine("<li><a href=\"#\">4</a></li>");
            sb.AppendLine("<li><a href=\"#\">5</a></li>");
            sb.AppendLine("<li>");
            sb.AppendLine("<a href=\"#\">");
            sb.AppendLine("<i class=\"mdi mdi-arrow-right\"></i>");
            sb.AppendLine("</a>");
            sb.AppendLine("</li>");
            sb.AppendLine("</ul>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"col-md-4\">");
            sb.AppendLine("<p class=\"text-right\">Showing <strong>" + SN + " Records</strong> out of <strong>" + SN + "</strong></p>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</form>");

            return sb.ToString();
        }

        public static string MakeGrid1<T>(List<T> obj, string _Value)
        {
            foreach (var item in obj)
            {
                Type t = item.GetType();
                foreach (var col in column)
                {
                    var a = item.GetType().GetProperty(col.Key).GetValue(item, null);

                }
            }

            ////Type t = obj.GetType();
            //PropertyInfo[] props = t.GetProperties();
            //var list = props[2].GetType();

            //foreach (var item in column)
            //{
            //    var a = obj.GetType().GetProperty(item.Key).GetValue(obj, null);

            //}
            ////var item = props[2];
            ////var b=item.PropertyType;
            //foreach (PropertyInfo prp in props)
            //{
            //    var a = prp.Name;
            //}

            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ButtonName">Name of the submit button</param>
        /// <param name="breadcrumb">BreadCrumb for Setup Page</param>
        /// <returns></returns>
        public static string GetBreadCum(string breadcrumb = "Home|Management|Manage", string ButtonName = "Submit")
        {
            var label = breadcrumb.Split('|');
            string breadCum = "";
            breadCum += "<div class='page-header' id='stickHeader'>";
            breadCum += "<div class='row'>";
            breadCum += "<div class='col-sm-6'>";
            breadCum += "<ol class='breadcrumb'>";
            breadCum += "<li><a href='#'>" + label[0] + "</a></li>";
            breadCum += "<li><a href='#'>" + label[1] + "</a></li>";
            breadCum += "<li class='active'>" + label[2] + "</li>";
            breadCum += "</ol>";
            breadCum += "</div>";
            breadCum += "<div class='col-sm-6'>";
            breadCum += "<div class='form-group form-action'>";
            breadCum += "<a onclick='GoBack();' class='btn btn-default'><i class='mdi mdi mdi-arrow-left'></i> Back </a>";
            if (!string.IsNullOrWhiteSpace(ButtonName))
            {
                breadCum += "<button type='submit' class='btn btn-success'><i class='mdi mdi-check-circle-outline'></i> " + ButtonName + "</button>";
            }
            breadCum += "</div>";
            breadCum += "</div>";
            breadCum += "</div>";
            breadCum += "</div>";
            return breadCum;

        }

        public static string GetAddBreadCum(string breadcrumb, string controller, string AddFunctionId)
        {
            var label = breadcrumb.Split('|');
            string breadCum = "";
            breadCum += "<div class=\"page-header\">";
            breadCum += "<div class=\"row\">";
            breadCum += "<div class=\"col-sm-6\">";
            breadCum += "<ol class=\"breadcrumb\">";
            breadCum += "<li><a href='#'>" + label[0] + "</a></li>";
            breadCum += "<li><a href='#'>" + label[1] + "</a></li>";
            breadCum += "<li class='active'>" + label[2] + "</li>";
            breadCum += "</ol>";
            breadCum += "</div>";
            breadCum += "<div class=\"col-sm-6\">";
            breadCum += "<div class=\"form-group form-action\">";
            breadCum += "<button class=\"btn btn-default\" onclick=\"GoBack();\"><i class=\"mdi mdi-arrow-left\"></i> Back</button>";
            if (controller.ToLower() == "claimapproval")
            {
                //breadCum += "<a href=\"/" + controller + "/Manage\" class=\"btn btn-success\"><i class=\"mdi mdi-plus\"></i> Add New</a>";
            }
            else if (StaticData.HasRight(controller, AddFunctionId))
            {
                breadCum += "<a href=\"/" + controller + "/Manage\" class=\"btn btn-success\"><i class=\"mdi mdi-plus\"></i> Add New</a>";
            }
            breadCum += "</div></div></div></div>";
            return breadCum;
        }
    }
}
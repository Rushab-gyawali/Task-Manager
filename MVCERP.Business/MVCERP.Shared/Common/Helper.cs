using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace MVCERP.Shared.Common
{
    public static class Helper
    {
        public static string GetActions(string Control,int Id,string ViewId){
            var link = "<a href='/" + Control + "/Manage/" + Id + "' ><i class='fa fa-pencil'></i></a>";
            link += " | <a href='/" + Control + "/Delete/" + Id + "'><i class='fa fa-remove'></i></a>";
            if (Control.ToLower()=="user")
            {
                link += " | <a href='#' data-toggle='modal' data-target='#AddModal' onclick='GetDetailById(" + Id + ")'><i class='fa fa-gear'></i></a>";
            } else if (Control.ToLower() == "role")
            {
                link += " | <a href='/" + Control + "/Role/" + Id + "'><i class='fa fa-gear'></i></a>";
            }
            return link;
        }

        public static string GetMenuList(List<RoleDetails> data)
        {
            var sb = new StringBuilder("<table class='table table-bordered'>");

            foreach (var item in data.GroupBy(x => x.menuGroup).Select(y => y.First()))
            {
                var menu = data.Where(m => m.menuGroup == item.menuGroup);
                sb.AppendLine("<tr><th style='cursor:pointer' onclick=\"CheckFunction('" + item.menuGroup.Replace(" ", "_") + item.groupPosition + "')\" rowspan='" + (menu.GroupBy(x => x.parentFunctionId).Count() + 1) + "'>" + item.ParentGroup + " --> " + item.menuGroup + "</th></tr>");
                sb.AppendLine(GetMenuList(menu));
            }

            //var menu = data.Where(m => m.menuGroup.ToLower() == "user management");
            //sb.AppendLine("<tr><th onclick=\"CheckFunction(\"User Management\")\" rowspan='" + (menu.GroupBy(x => x.parentFunctionId).Count() + 1) + "'>User Management</th></tr>");
            //sb.AppendLine(GetMenuList(menu));

            //menu = data.Where(m => m.menuGroup.ToLower() == "application log");
            //sb.AppendLine("<tr><th onclick=\"CheckFunction(\"Application Log\")\" rowspan='" + (menu.GroupBy(x => x.parentFunctionId).Count() + 1) + "'>Application Log</th></tr>");
            //sb.AppendLine(GetMenuList(menu));

            sb.AppendLine("</table>");

            return sb.ToString();
        }

        private static string GetMenuList(IEnumerable<RoleDetails> menu)
        {
            var sb = new StringBuilder("");

            foreach (var item in menu.GroupBy(x => x.parentFunctionId).Select(y => y.First()))
            {
                sb.AppendLine("<tr>");
                sb.AppendLine("<td style='cursor:pointer' onclick=\"CheckFunction('" + item.menuName.Replace(" ", "_") +item.groupPosition+ "')\" ><strong>" + item.menuName + "</strong></td>");
                sb.AppendLine("<td>");
                if (menu.Where(m=>m.parentFunctionId ==item.parentFunctionId) !=null )
                {
                    foreach (var m in menu.Where(m=>m.parentFunctionId ==item.parentFunctionId))
                    {
                        string chk = "";
                        if (!string.IsNullOrWhiteSpace(m.hasChecked))
                        {
                            chk = "checked='checked'";
                        }
                        sb.AppendLine("<input type='checkbox' " + chk + " id='functionRole' name='functionRole' class='" + item.menuGroup.Replace(" ", "_") + item.groupPosition + "  " + item.menuName.Replace(" ", "_") + item.groupPosition + "' value='" + m.functionId + "' />" + m.functionName);
                        sb.AppendLine("</br>");
                    }
                }
                sb.AppendLine("</td></tr>");
            }

            return sb.ToString();
        }

        public static string UppercaseFirstLetter(this string value)
        {            
            if (value.Length > 0)
            {
                char[] array = value.ToCharArray();
                array[0] = char.ToUpper(array[0]);
                return new string(array);
            }
            return value;
        }
        public static int ToInt(this string value)
        {
            int a = 0;
            if (value.Length > 0)
            {
                if (int.TryParse(value, out a))
                    return a;
            }
            return a;
        }


        public static float ToFloat(this string value)
        {
            float a = 0;
            if (value.Length > 0)
            {
                if (float.TryParse(value, out a))
                    return a;
            }
            return a;
        }
        public static bool IsImage(HttpPostedFileBase fileUpload)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (fileUpload.ContentType.ToLower() != "image/jpg" &&
                        fileUpload.ContentType.ToLower() != "image/jpeg" &&
                        fileUpload.ContentType.ToLower() != "image/pjpeg" &&
                        fileUpload.ContentType.ToLower() != "image/gif" &&
                        fileUpload.ContentType.ToLower() != "image/x-png" &&
                        fileUpload.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(fileUpload.FileName).ToLower() != ".jpg"
                && Path.GetExtension(fileUpload.FileName).ToLower() != ".png"
                && Path.GetExtension(fileUpload.FileName).ToLower() != ".gif"
                && Path.GetExtension(fileUpload.FileName).ToLower() != ".jpeg")
            {
                return false;
            }

            return true;
        }
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable ListToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}

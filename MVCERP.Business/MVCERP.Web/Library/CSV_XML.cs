using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace MVCERP.Web.Library
{
    public class CSV_XML
    {
        public static string GetCSVToXML(string filePath, bool hasHeader, string[] defaultHeader = null)
        {
            var dt = new DataTable();
            var columnList = new ArrayList();

            var sb = new StringBuilder("<root>");
            using (CsvReader reader = new CsvReader(filePath))
            {
                foreach (string[] values in reader.RowEnumerator)
                {
                    if (defaultHeader.Length > 0 && hasHeader)
                    {
                        if (defaultHeader.Length != values.Length)
                        {
                            break;
                        }
                        dt.Columns.Clear();
                        dt.Clear();
                        foreach (var itm in defaultHeader)
                        {
                            var data = itm.Replace(" ", "_").Replace("\"", "");
                            columnList.Add(data.ToUpper());
                        }
                        hasHeader = false;
                        continue;
                    }
                    else if (hasHeader)
                    {
                        dt.Columns.Clear();
                        dt.Clear();
                        foreach (var itm in values)
                        {
                            var data = itm.Replace(" ", "_").Replace("\"", "");
                            columnList.Add(data.ToUpper());
                        }
                        hasHeader = false;
                        continue;
                    }
                    if (values.Length > 0)
                    {
                        sb.Append("<row");
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (columnList[i].ToString() == "CLIENTBIRTHDATE" || columnList[i].ToString() == "LOANDATE" || columnList[i].ToString() == "SPOUSEBIRTHDATE")
                            {
                                var date="";
                                if (!String.IsNullOrEmpty(values[i]))
                                {
                                    date = values[i].Replace("-", "/");
                                    sb.Append(string.Format(" {0}=\"{1}\"", columnList[i], StaticData.GetNepaliToADDate(date)));
                                    

                                }
                                else
                                {
                                    sb.Append(string.Format(" {0}=\"{1}\"", columnList[i], values[i]));
                                }
                            }
                            else
                            {
                                sb.Append(string.Format(" {0}=\"{1}\"", columnList[i], values[i]));
                            }
                        }
                        sb.Append(" />");
                    }
                }
            }
            sb.Append("</root>");
            return sb.ToString();
        }
    }
}
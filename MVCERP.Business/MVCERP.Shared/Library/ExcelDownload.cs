using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MVCERP.Shared.Common;

namespace MVCERP.Shared.Library
{
    public class ExcelDownload
    {
        private static int rowsPerSheet = 50000;
        private static DataTable ResultsData=new DataTable();
        public void GenerateExcel(string sqlQuery, string titleData = null, ReportResultCommon result = null)
        {
            string firstRowTitle = "";
            if (!string.IsNullOrEmpty(titleData))
            {
                firstRowTitle = titleData;
            }
            if (null != result)
            {
                ResultsData = result.ResultSet;
                ExportToExcel(result);
            }
            //else
            //{
            //    var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            //    ResultsData.Clear();
            //    string queryString = sqlQuery;
            //    using (var connection = new SqlConnection(connectionString))
            //    {
            //        var command = new SqlCommand(queryString, connection);
            //        connection.Open();
            //        command.CommandTimeout = 3600;
            //        command.ExecuteNonQuery();
            //        SqlDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
            //        int c = 0;
            //        bool firstTime = true;

            //        //Get Column Name
            //        DataTable dtSchema = reader.GetSchemaTable();
            //        var listCols = new List<DataColumn>();
            //        if (dtSchema != null)
            //        {
            //            ResultsData.Columns.Clear();
            //            foreach (DataRow drow in dtSchema.Rows)
            //            {
            //                string columnName = Convert.ToString(drow["ColumnName"]);
            //                var column = new DataColumn(columnName, (Type)(drow["DataType"]));
            //                column.Unique = (bool)drow["IsUnique"];
            //                column.AllowDBNull = (bool)drow["AllowDBNull"];
            //                column.AutoIncrement = (bool)drow["IsAutoIncrement"];
            //                listCols.Add(column);
            //                //remove if previous column exist and re add it
            //                if (ResultsData.Columns.Contains(columnName))
            //                {
            //                    ResultsData.Columns.Remove(columnName);
            //                }

            //                ResultsData.Columns.Add(column);
            //            }
            //        }

            //        try
            //        {
            //            while (reader.Read())
            //            {
            //                DataRow dataRow = ResultsData.NewRow();
            //                for (int i = 0; i < listCols.Count; i++)
            //                {
            //                    dataRow[(listCols[i])] = reader[i];
            //                }
            //                ResultsData.Rows.Add(dataRow);
            //                c++;
            //                if (c == rowsPerSheet)
            //                {
            //                    c = 0;
            //                    ExportToExcel(firstTime);
            //                    ResultsData.Clear();
            //                    firstTime = false;
            //                }
            //            }
            //            if (ResultsData.Rows.Count > 0)
            //            {
            //                ExportToExcel(firstTime, firstRowTitle);
            //                ResultsData.Clear();
            //            }
            //            reader.Close();
            //        }
            //        catch (Exception e)
            //        {
            //            Console.WriteLine(e);
            //            throw;
            //        }
            //    }
            //}
        }
        private static void ExportToExcel(ReportResultCommon result)
        {
            bool firstTime = true;
            string filePath = ConfigurationManager.AppSettings["filePath"];
            string fileName = filePath+@"\Reports.xlsx";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            uint sheetId = 1;
            if (firstTime)
            {
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();
                var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                var bold1 = new Bold();
                CellFormat cf = new CellFormat();
                Sheets sheets;
                sheets = spreadsheetDocument.WorkbookPart.Workbook.
                    AppendChild<Sheets>(new Sheets());
                var sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.
                        GetIdOfPart(worksheetPart),
                    SheetId = sheetId,
                    Name = "Sheet" + sheetId
                };
                sheets.Append(sheet);

                //if (!string.IsNullOrEmpty(titleData))
                //{  
                //    Row firstRow = new Row();
                //    var cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(titleData)};
                //    firstRow.AppendChild(cell);
                //    sheetData.AppendChild(firstRow);
                //}
                //for header and filter data
                Row firstRow = new Row();
               
                var cellH = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(result.ReportHead),
                    StyleIndex = 0
                };
               

                firstRow.AppendChild(cellH);
                sheetData.AppendChild(firstRow);

                firstRow = new Row();

                var cellF = new Cell { DataType = CellValues.String, CellValue = new CellValue(result.Filters) };
                firstRow.AppendChild(cellF);
                sheetData.AppendChild(firstRow);
                //end of header and filter

                var headerRow = new Row();
                foreach (DataColumn column in ResultsData.Columns)
                {
                    var cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) };
                    headerRow.AppendChild(cell);
                }
                sheetData.AppendChild(headerRow);

                foreach (DataRow row in ResultsData.Rows)
                {
                    var newRow = new Row();
                    foreach (DataColumn col in ResultsData.Columns)
                    {
                        var cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(row[col].ToString())
                        };
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }
                workbookpart.Workbook.Save();

                spreadsheetDocument.Close();
            }
            else
            {
                var spreadsheetDocument = SpreadsheetDocument.Open(fileName, true);

                var workbookpart = spreadsheetDocument.WorkbookPart;
                if (workbookpart.Workbook == null)
                    workbookpart.Workbook = new Workbook();

                var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();


                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                var sheets = spreadsheetDocument.WorkbookPart.Workbook.Sheets;

                if (sheets.Elements<Sheet>().Any())
                {
                    sheetId = sheets.Elements<Sheet>().Max(s => s.SheetId.Value) + 1;
                }
                else
                {
                    sheetId = 1;
                }

                var sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.
                        GetIdOfPart(worksheetPart),
                    SheetId = sheetId,
                    Name = "Sheet" + sheetId
                };
                sheets.Append(sheet);
                var headerRow = new Row();

                //for header and filter data
                var cellH = new Cell { DataType = CellValues.String, CellValue = new CellValue(result.ReportHead) };
                headerRow.AppendChild(cellH);
                sheetData.AppendChild(headerRow);

                var cellF = new Cell { DataType = CellValues.String, CellValue = new CellValue(result.Filters) };
                headerRow.AppendChild(cellF);
                sheetData.AppendChild(headerRow);
                //end of header and filter

                foreach (DataColumn column in ResultsData.Columns)
                {
                    var cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) };
                    headerRow.AppendChild(cell);
                }
                sheetData.AppendChild(headerRow);
                
                foreach (DataRow row in ResultsData.Rows)
                {
                    var newRow = new Row();

                    foreach (DataColumn col in ResultsData.Columns)
                    {
                        var cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(row[col].ToString())
                        };
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }
                workbookpart.Workbook.Save();
                spreadsheetDocument.Close();
            }
        }
    }
}

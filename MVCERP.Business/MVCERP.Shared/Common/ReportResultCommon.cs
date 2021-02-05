using MVCERP.Shared.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MVCERP.Shared.Common
{
   public class ReportResultCommon:DbResponse
   {
       private DataTable _resultSet;
       private DataSet _result;
       private string _filters;
       private string _reportHead;
       private string _sql;

       public DataSet Result
       {
           set { _result = value; }
           get { return _result; }
       }

       public DataTable ResultSet
       {
           set { _resultSet = value; }
           get { return _resultSet; }
       }

       public string Filters
       {
           set { _filters = value; }
           get { return _filters; }
       }

       public string ReportHead
       {
           set { _reportHead = value; }
           get { return _reportHead; }
       }
       public string Sql
       {
           set { _sql = value; }
           get { return _sql; }
       }
   }
}

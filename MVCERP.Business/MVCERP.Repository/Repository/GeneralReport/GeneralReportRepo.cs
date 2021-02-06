using iSolutionLife.Shared.Common;
using iSolutionLife.Shared.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace iSolutionLife.Repository.Repository.GeneralReport
{
    public class GeneralReportRepo:IGeneralReportRepo
    {
         RepositoryDao dao;
         public GeneralReportRepo()
        {
            dao = new RepositoryDao();
        }

        public List<GeneralReportCommon> GetPolicyAsOn(GeneralReportCommon model)
        {
            var sql = "EXEC proc_GeneralReport @flag='rptPolicyAsOn' ";
            sql += ",@user = " + dao.FilterString(model.user);
            sql += ",@fromDate  = " + dao.FilterStringDR(model.FromDate);
            sql += ",@toDate = " + dao.FilterStringDR(model.ToDate);
            sql += ",@Branch = " + dao.FilterStringDR(model.Branch);
            var ds = dao.ExecuteDataset(sql);
            var policyDetails = (List<PolicyDetails1>)Mapper.DataTableToClass<PolicyDetails1>(ds.Tables[0]);
            var rptPolicy = (List<GeneralReportCommon>)Mapper.DataTableToClass<GeneralReportCommon>(ds.Tables[1]);
            rptPolicy[0].PolicyDetails = policyDetails;
            return rptPolicy;

        }

        
    }
}

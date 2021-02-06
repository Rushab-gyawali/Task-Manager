using System;
using System.Collections.Generic;
using System.Data;
using MVCERP.Repository.Repository.StaticData;
using MVCERP.Shared.Common;

namespace MVCERP.Repository.Repository.StaticData
{
    public class StaticDataRepository : IStaticDataRepository
    {
        RepositoryDao dao;
        public StaticDataRepository()
        {
            dao = new RepositoryDao();
        }
        public List<StaticDataCommon> GetList(string user, string id, string Search, int Pagesize)
        {
            var list = new List<StaticDataCommon>();
            try
            {

                var sql = "EXEC proc_tblStaticData ";
                sql += " @FLAG = " + dao.FilterString(id != "0" ? "S" : "A");
                sql += ",@User = " + dao.FilterString(user);
                sql += ",@ID = " + dao.FilterString(id.ToString());
                sql += ",@Search = " + dao.FilterString(Search);
                sql += ",@Pagesize = " + dao.FilterString(Pagesize.ToString());

                var dt = dao.ExecuteDataTable(sql);
                if (null != dt)
                {
                    int sn = 1;
                    foreach (DataRow item in dt.Rows)
                    {
                        var common = new StaticDataCommon()
                        {

                            UniqueId = Convert.ToInt32(item["Id"]),
                            TypeCode = item["StaticCode"].ToString(),
                            DataCode = item["Code"].ToString(),
                            DataValue = item["Value"].ToString(),
                            NepValue = item["NepValue"].ToString(),
                            User = item["CreatedBy"].ToString(),
                            CreatedDate = item["CreatedDate"].ToString()
                        };
                        sn++;
                        list.Add(common);
                    }
                }
            }
            catch (Exception e)
            {

                return list;
            }

            return list;
        }

        public List<StaticDataCommon> GetSubList(string User, string Id)
        {
            var list = new List<StaticDataCommon>();
            try
            {
                var sql = "EXEC proc_tblStaticData ";
            sql += " @FLAG = " + dao.FilterString("SubList");
            sql += ",@User = " + dao.FilterString(User);
            sql += ",@ID = " + dao.FilterString(Id.ToString());

            var dt = dao.ExecuteDataTable(sql);
            if (null != dt)
            {
                int sn = 1;
                foreach (DataRow item in dt.Rows)
                {
                    var common = new StaticDataCommon()
                    {
                        
                        UniqueId = Convert.ToInt32(item["Id"]),
                        TypeCode = item["StaticCode"].ToString(),
                        DataCode = item["Code"].ToString(),
                        DataValue = item["Value"].ToString(),
                        User = item["CreatedBy"].ToString(),
                        CreatedDate = item["CreatedDate"].ToString()
                    };
                    sn++;
                    list.Add(common);
                }
            }
            }
            catch (Exception e)
            {

                return list;
            }

            return list;
        }
       

        public DbResponse Manage(StaticDataCommon model)
        {
            var sql = "EXEC proc_tblStaticData ";
            sql += " @FLAG = " + dao.FilterString((model.UniqueId > 0 ? "U" : "I"));
            sql += ",@User = " + dao.FilterString(model.User);
            sql += ",@Id = " + dao.FilterString(model.UniqueId.ToString());
            sql += ",@StaticCode = " + dao.FilterString(model.TypeCode);
            sql += ",@Code = " + dao.FilterString(model.DataCode);
            sql += ",@Value = " + dao.FilterString(model.DataValue);
            sql += ",@NepValue = " + dao.FilterString(model.NepValue);
            return dao.ParseDbResponse(sql);
        }

        public DbResponse Delete(string User, int id)
        {
            var sql = "EXEC proc_tblStaticData ";
            sql += " @FLAG = " + dao.FilterString("D");
            sql += ",@User = " + dao.FilterString(User);
            sql += ",@Id = " + dao.FilterString(id.ToString());
            return dao.ParseDbResponse(sql);
        }


        public DbResponse InsertErrorLog(ErrorLogsCommon log)
        {
            var sql = "EXEC proc_tblErrors ";
            sql += " @FLAG = " + dao.FilterString("I");
            sql += ",@User = " + dao.FilterString(log.User);
            sql += ",@ErrorPage = " + dao.FilterString(log.ErrorPage);
            sql += ",@ErrorMsg = " + dao.FilterString(log.ErrorMsg);
            sql += ",@ErrorDetail = " + dao.FilterString(log.ErrorDetail);
            return dao.ParseDbResponse(sql);
        }


        public DbResponse EOD(string User)
        {
            var sql = "EXEC proc_EOD ";
            sql += "@User = " + dao.FilterString(User);
            return dao.ParseDbResponse(sql);
        }


        public DbResponse BOD(string User, string Date)
        {
            var sql = "EXEC proc_BOD ";
            sql += "@User = " + dao.FilterString(User);
            sql += ",@NewDate = " + dao.FilterString(Date);
            return dao.ParseDbResponse(sql);
        }
    }
}

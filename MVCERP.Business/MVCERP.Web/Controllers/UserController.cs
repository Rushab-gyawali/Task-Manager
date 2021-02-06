using System.Collections.Generic;
using System.Web.Mvc;
using MVCERP.Business.Business.User;
using MVCERP.Web.Library;
using MVCERP.Web.Models;
using MVCERP.Web.Filters;
using MVCERP.Shared.Common;
using iSolutionLife.Shared.Common;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace MVCERP.Web.Controllers
{
    [SessionExpiryFilter]
    public class UserController : Controller
    {
        string MenuName = "User";
        string ControllerName = "User";
        private const string ViewId = "108300";
        private const string AddEditId = "108310";
        private const string RoleId = "108320";
        private const string Changepassword = "$&23@73";

        IUserBusiness buss;
        MVCERP.Business.Business.Common.ICommonBuss ddl;
        public UserController(IUserBusiness _buss, MVCERP.Business.Business.Common.ICommonBuss _ddl)
        {
            buss = _buss;
            ddl = _ddl;
        }
        //
        // GET: /User/
        private void LoadDDl(UserModel model)
        {
            var ds = ddl.GetDropDownLists("UserSetup");
            model.BranchList = StaticData.SetDDLByTable(ds.Tables[0], model.Branch.ToString(), "Select Branch Type");
            model.DepartmentList = StaticData.SetDDLByTable(ds.Tables[1], model.DepartmentId.ToString(), "Select Department");
            model.DesignationList = StaticData.SetDDLByTable(ds.Tables[2], model.DesignationId.ToString(), "Select Designation");
            ViewData["Status"] = StaticData.GetIsActiveDdl(model.IsActive);
        }
        public ActionResult Index()
        {  
            return View();
        }
        [HttpPost]
        public string GetGridDetails(GridDetails param)
        {
            var accountDetails = new GridParam
            {
                DisplayLength = param.length,
                DisplayStart = param.start,
                SortDir = param.order[0].dir,
                SortCol = param.order[0].column,
                Flag = "A",
                Search = param.search.value,
                UserName = StaticData.GetUser()
            };
            var gridList = buss.GetGridList(accountDetails);
            foreach (var item in gridList)
            {
                //item.Action = StaticData.GetActions("FPAgentDetail", item.UniqueId, "", AddEdit);
                item.Action = StaticData.GetActions("User", item.UniqueId, RoleId, "108310");
                item.CreatedDate = StaticData.DBToFrontDate(item.CreatedDate);
            }
            HtmlGrid<UserCommon> companyGrid = new HtmlGrid<UserCommon>();
            var firstDefault = gridList.FirstOrDefault();
            companyGrid.aaData = gridList;
            if (firstDefault != null)
            {
                companyGrid.iTotalDisplayRecords = Convert.ToInt32(firstDefault.FilterCount);
                companyGrid.iTotalRecords = Convert.ToInt32(firstDefault.FilterCount);
            }


            var result = JsonConvert.SerializeObject(companyGrid).ToString();
            return result;
        }

        public ActionResult AssignRole(int id = 0)
        {
            var data = buss.GetUserRole(StaticData.GetUser(), id.ToString());
            if (id == 0)
            {
                return HttpNotFound();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignRole(FormCollection collection)
        {
            StaticData.CheckFunctionId(ControllerName, RoleId);
            var Username = collection["Username"];
            var RoleList = collection["RoleList"];
            if (string.IsNullOrWhiteSpace(Username))
            {
                return HttpNotFound();
            }

            var data = buss.AssignUserRole(StaticData.GetUser(), Username, RoleList);

            return RedirectToAction("Index");
        }
        //public ActionResult Create()
        //{
        //    StaticData.CheckFunctionId(ControllerName,"a");
        //    var user = new UserModel();
        //    SetDDL(user);
        //    return View(user);
        //}
       
        //
        // POST: /User/Create

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(UserModel model)
        //{
        //    StaticData.CheckFunctionId(ControllerName,"a");
        //    if (ModelState.IsValid)
        //    {
        //        UserCommon common = new UserCommon();

        //        var response = buss.ManageUse(common);
        //        if (response.ErrorCode == 1)
        //        {
        //            ModelState.AddModelError("", response.Message);
        //            return View(model);
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    SetDDL(model);
        //    return View(model);
        //}

        //
        // GET: /User/Edit/5

        public ActionResult Manage()
        {
            StaticData.CheckFunctionId(ControllerName, AddEditId);
            var model = new UserModel();
            LoadDDl(model);
            model.BreadCum = ProjectGrid.GetBreadCum("Setup|System Setup|User Setup");
            var data = buss.GetUserList(StaticData.GetUser(), StaticData.GetIdFromQuery());
            if (data.Count == 0)
            {
                return View(model);
            }
            model.UniqueId = data[0].UniqueId;
            model.Branch = data[0].Branch;
            model.DesignationId = data[0].DesignationId;
            model.DepartmentId = data[0].DepartmentId;
            model.UserId = data[0].UserID;
            model.FullName = data[0].FullName;
            model.Email = data[0].Email;
            model.PhoneNo = data[0].PhoneNo;
            model.IsAdminUser = data[0].IsAdminUser;
            model.IsActive = data[0].IsActive;
            model.AllowBackDate = data[0].AllowBackDate;
            model.AllowApproveDate = data[0].AllowApproveDate;
            LoadDDl(model);
            return View(model);
        }

        public ActionResult Decode(string Encode)
        {
            Encode = StaticData.Base64Decode(Encode);
            return Json(Encode, JsonRequestBehavior.AllowGet); 
        }
        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(UserModel model)
        {
            StaticData.CheckFunctionId(ControllerName, AddEditId);
            LoadDDl(model);
            if (ModelState.IsValid)
            {
                UserCommon common = new UserCommon();
                common.UniqueId = model.UniqueId;
                common.UserID = model.UserId;
                common.UserPwd = StaticData.Base64Encode(model.UserPwd);
                common.FullName = model.FullName;
                common.Email = model.Email;
                common.Branch = model.Branch;
                common.DesignationId = model.DesignationId;
                common.DepartmentId = model.DepartmentId;
                common.PhoneNo = model.PhoneNo;
                common.IsAdminUser = model.IsAdminUser;
                common.IsActive = model.IsActive;
                common.User = StaticData.GetUser();
                common.AllowBackDate = model.AllowBackDate;
                common.AllowApproveDate = model.AllowApproveDate;
                var response = buss.ManageUse(common);
                StaticData.SetMessageInSession(response);
                if (response.ErrorCode == 1)
                {
                    ModelState.AddModelError("", response.Message);
                    return View(model);
                }
                return RedirectToAction("Index");
            }
                //db.Entry(userprofile).State = EntityState.Modified;
                //db.SaveChanges();
           
            return View(model);
        }

        public ActionResult Monitoring(string  user="") {
            MVCERP.Web.Library.UserMonitor.GetInstance().RemoveUser(user);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeUserPassword()
        {
            //var id = StaticData.GetIdFromQuery();
            StaticData.CheckFunctionId(ControllerName, RoleId);
            var data = buss.Changeuserpassword(StaticData.GetUser(), StaticData.Base64Encode(Changepassword), Request.Form["userId"]);
            return RedirectToAction("Index");
        }
       
    }
}
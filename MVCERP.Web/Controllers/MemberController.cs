using MVCERP.Business.Business.Member;
using MVCERP.Web.Library;
using MVCERP.Business.Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCERP.Shared.Common;
using MVCERP.Web.Models;

namespace MVCERP.Web.Controllers
{
    public class MemberController : Controller
    {
       
        IMemberBusiness bussiness;
        ICommonBuss ddl;
        public MemberController(IMemberBusiness _buss, ICommonBuss _ddl)
        {
            StaticData.CheckSession();
            bussiness = _buss;
            ddl = _ddl;
        }
        public ActionResult Index()
        {
            StaticData.CheckSession();
            var user = StaticData.GetUser();
            ViewBag.user = user;
            ViewData["Roles"] = StaticData.SetDDLValue(ddl.SetDropdownRoles("ListRoles", StaticData.GetUser()), "", "Select Role");
            return View();
        }
        public JsonResult ListUsers()
        {
            StaticData.CheckSession();
            var data = bussiness.ListUsers();
                for (int i = 0; i < data.Count; i++)
                {
                    data[i].Action = StaticData.GetActions("Member",  data[i].ID, data[i].ID.ToString(), "New");
                }
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult New()
        {
            StaticData.CheckSession();
            string id = Request.QueryString["id"];
            var ID = StaticData.Base64Decode_URL(id);
            var model = new MemberModel();
            if (ID == "")
            {
                var user = StaticData.GetUser();
                ViewBag.user = user;
                return View();
            }
            else
            {
                StaticData.CheckSession();
                var data = bussiness.GetById(ID);
                model.ID = data[0].ID.ToString();
                model.FullName = data[0].FullName;
                model.UserName = data[0].UserName;
                model.Email = data[0].Email;
                model.PhoneNo = data[0].PhoneNo;
                model.Password = data[0].Password;
                //model.AdminRight = Convert.ToBoolean(data[0].AdminRight);


                return View(model);
            }
        }


        [HttpPost]
        public ActionResult New(MemberModel model)
        {
            StaticData.CheckSession();

            var user = StaticData.GetUser();

            if (ModelState.IsValid)
            {
                StaticData.CheckSession();
                MemberCommon common = new MemberCommon();

                common.ID = Convert.ToInt32(model.ID);
                common.FullName = model.FullName;
                common.UserName = model.UserName;
                common.Email = model.Email;
                common.PhoneNo = model.PhoneNo;
                common.Password = StaticData.Base64Encode(model.Password);
                common.AdminRight = Convert.ToBoolean(model.AdminRight);
                common.CreatedBy = user;
                var response = bussiness.AddUsers(common);
                StaticData.SetMessageInSession(response);
                if (response.ErrorCode == 1)
                {
                    ModelState.AddModelError("", response.Message);
                    return View(model);
                }
                return RedirectToAction("Index", "Member");
            }
            else
            {
                var errors = string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));

                ModelState.AddModelError("", errors);
            }
            ViewData["msg"] = "The password and Confirm password doesnot match.";
            return View(model);
        }

        public ActionResult DeleteUser()
        {
            StaticData.CheckSession();
            string id = Request.QueryString["id"];
            var Id = StaticData.Base64Decode_URL(id);
            var ID = Convert.ToInt32(Id);
            if (ModelState.IsValid)
            {
                MemberCommon common = new MemberCommon();
                common.ID = ID;
                var response = bussiness.DeleteUser(ID);
                StaticData.SetMessageInSession(response);
                if (response.ErrorCode == 1)
                {
                    ModelState.AddModelError("", response.Message);
                    ViewData["msg"] = "Delete Failed";
                    return RedirectToAction("Index", "Member");

                }
                ViewData["msg"] = "Delete Successful";
                return RedirectToAction("Index", "Member");
            }
            else
            {
                var errors = string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));

                ModelState.AddModelError("", errors);
            }
            return RedirectToAction("Index", "Member");
        }

        public ActionResult Profile()
        {
            StaticData.CheckSession();
            var user = StaticData.GetUser();
            MemberModel model = new MemberModel();
            MemberCommon common = new MemberCommon();
            common.UserName = user;
            var data = bussiness.ListUsersProfile(common);
            model.ID = data[0].ID.ToString(); 
            model.FullName = data[0].FullName;
            model.Email = data[0].Email;
            model.PhoneNo = data[0].PhoneNo;

            return View(model);
        }


        public ActionResult AssignRole()
        {
            string id = Request.QueryString["id"];
            var Id = id;
            var data = bussiness.GetUserRole(StaticData.GetUser(), Id);
            ViewBag.id = Id;
            
            if (Id == null)
            {
                return HttpNotFound();
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AssignRoles(MemberCommon model)
        {
            StaticData.CheckSession(); 
            string id = Request.QueryString["id"];
            var Id = id;
            var user = StaticData.GetUser();

            if (ModelState.IsValid)
            {
                StaticData.CheckSession();
                var response = bussiness.AssignUserRole(model);
                StaticData.SetMessageInSession(response);
                if (response.ErrorCode == 1)
                {
                    ModelState.AddModelError("", response.Message);
                    return RedirectToAction("Index", "Member");
                }
                return RedirectToAction("Index", "Member");
            }
            else
            {
                var errors = string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));

                ModelState.AddModelError("", errors);
            }

            return RedirectToAction("Index", "Member");



        }

        public ActionResult Role()
        {
            return View();
        }
    }
}

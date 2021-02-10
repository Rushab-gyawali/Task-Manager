using MVCERP.Business.Business.Member;
using MVCERP.Web.Library;
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
        public MemberController(IMemberBusiness _buss)
        {
            bussiness = _buss;

        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ListUsers()
        {
              var data = bussiness.ListUsers();
                for (int i = 0; i < data.Count; i++)
                {
                    data[i].Action = StaticData.GetActions("Member",  data[i].ID, data[i].ID.ToString(), "New");
                }
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult New()
        {
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

            if (ModelState.IsValid)
            {
                MemberCommon common = new MemberCommon();

                common.ID = Convert.ToInt32(model.ID);
                common.FullName = model.FullName;
                common.UserName = model.UserName;
                common.Email = model.Email;
                common.PhoneNo = model.PhoneNo;
                common.Password = StaticData.Base64Encode(model.Password);
                common.AdminRight = Convert.ToBoolean(model.AdminRight);
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

    }
}

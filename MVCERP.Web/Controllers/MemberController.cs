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
                    data[i].Action = StaticData.GetActions("Member", data[i].ID, "");
                }
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult New()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddUser(MemberModel model)
        {
    
            if (ModelState.IsValid)
            {
                MemberCommon common = new MemberCommon();
               
                common.ID = model.ID;
                common.FullName = model.FullName;
                common.UserName = model.UserName;
                common.Email = model.Email;
                common.PhoneNo = model.PhoneNo;
                common.Password = StaticData.Base64Encode(model.Password);
                var response = bussiness.AddUsers(common);
                StaticData.SetMessageInSession(response);
                if (response.ErrorCode == 0)
                {
                    ModelState.AddModelError("", response.Message);
                    return View(model);
                }
                return RedirectToAction("Index","Member");
            }
            else
            {
                var errors = string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));

                ModelState.AddModelError("", errors);
            }

            return View(model);
        }

    }
}

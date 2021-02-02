using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCERP.Web.Models
{
    public class UserModel
    {
        public int UniqueId { get; set; }
        [Required]
        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        //[Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string UserPwd { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("UserPwd", ErrorMessage = "The password and confirmation password do not match.")]
        public string CPassword { get; set; }


        [Required]
        [Display(Name = "Branch")]
        public int Branch { get; set; }
        public IEnumerable<SelectListItem> BranchList { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "PhoneNo")]
        public string PhoneNo { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }

        [Display(Name = "Designation")]
        public int DesignationId { get; set; }

        public IEnumerable<SelectListItem> DesignationList { get; set; }

        [Display(Name = "Is Active")]
        public string IsActive { get; set; }
        public string IsAdminUser{ get; set; }
        public bool AllowApproveDate { get; set; }
        public bool AllowBackDate { get; set; }
        public string  BreadCum { get; set; }
    }
    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCERP.Web.Models
{
    public class RoleModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string RoleName { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public int MenuId { get; set; }
        public int RoleId { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}
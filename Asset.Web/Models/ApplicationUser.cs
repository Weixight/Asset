using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Asset.Web.ViewModels;

namespace Asset.Web.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //public string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [NotMapped]
        public string? OtherName { get; set; }
        [NotMapped]
        public string? StaffNo { get; set; }
        [NotMapped]
        public string PID { get; set; }
        [NotMapped]
        public string? OldPass { get; set; }
        [NotMapped]
        public DateTime ActivationDate { get; set; }
        [NotMapped]
        public RoleViewModel[] Roles { get; set; }

    }
}

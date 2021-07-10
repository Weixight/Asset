using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class MyService
    {
        public int Id { get; set; }
        [Display(Name = "Service Code")]
        public int ServiceCode { get; set; }
        [Display(Name = "Service Name")]

        public string ServiceName { get; set; }
        [Display(Name = "root Id")]

        public int RootId { get; set; }
        [Display(Name = "ApprovalLevel")]

        public int ApprovalLevel {get; set; }
        [Display(Name = "ApprovalRequired")]

        public bool ApprovalRequired { get; set; }
        [Display(Name = "Approval Level Name")]


        public string ApprovalLevelName { get; set; }
        [Display(Name = "Service Owner")]

        public int ServiceOwner { get; set; }
        [Display(Name = "Service Owner Name")]

        public string ServiceOwnerName { get; set; }
        [Display(Name = "Upline")]

        public int Upline { get; set; }
        [Display(Name = "Down Line")]

        public int Downline { get; set; }
        public string ProcessTable { get; set; }

    }
}

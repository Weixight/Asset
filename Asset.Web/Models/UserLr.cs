using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Asset.Web.Models
{
    public class UserLr
    {
      
        [Key]
        public int PayerId { get; set; }
        public string PayerType { get; set; }
        public string Servicename { get; set; }
        public string ServiceCode { get; set; }
        public string CorpId { get; set; }
        public string Password { get; set; }
        public string AgencyCode { get; set; }
        public string UserEmail { get; set; }
        public string userPhone { get; set; }
        public string FirstName { get; set; }


    }
}

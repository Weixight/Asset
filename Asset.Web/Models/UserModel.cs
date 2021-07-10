using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asset.Web.Models
{
    public class UserModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PayerId { get; set; }
        public string PayerType { get; set; }
        public string Coy { get; set; }
        public string Userlevel { get; set; }
        public string Servicename { get; set; }
        public string ServiceCode { get; set; }
        public string CorpId { get; set; }
        public string Password { get; set; }
        public string AgencyCode { get; set; }
        public string UserEmail { get; set; }
        public string Right { get; set; }
        public string userPhone { get; set; }
        public string FirstName { get; set; }
        public string Sticket { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class ActivationMail
    {
        public int id { get; set; }
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
    }
}

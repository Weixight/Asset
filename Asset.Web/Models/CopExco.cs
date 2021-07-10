using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class CopExco
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidddleName { get; set; }
        public string Email { get; set; }
        public string CopRegNo { get; set; }
        public string Adddress { get; set; }
        public string City { get; set; }
        public string LGA { get; set; }
        public string State { get; set; }
    }
}

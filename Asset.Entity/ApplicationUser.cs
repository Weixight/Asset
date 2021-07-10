using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asset.Entity
{
    public class ApplicationUser:IdentityUser
    {
        public string CoperativeName { get; set; }
        public string Address { get; set; }
        public string Lga { get; set; }
        public string State { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class Country
    {
        public int id { get; set; }
        public string iso { get; set; }
        public string name { get; set; }
        public string nicename { get; set; }
        public string? iso3 { get; set; }
        public Nullable<int> numcode { get; set; }
        public Nullable <int> phonecode { get; set; }

    }
}

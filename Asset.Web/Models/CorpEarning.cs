using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class CorpEarning
    {
        public int id { get; set; }
        public int Corpid { get; set; }
        public string CorpName { get; set; }
        public int CalculatedName { get; set; }
        public int CalculatedItemid { get; set; }
        public decimal ValueAmount { get; set; }
        public DateTime ValueDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class CorpEarning
    {
        public int id { get; set; }
        [NotMapped]
        public string CalculatedItem { get; set; }

        public decimal ValueAmount { get; set; }
        public DateTime ValueDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        [NotMapped]
        public DateTime Date_One { get; set; }
        [NotMapped]
        public DateTime Date_Two { get; set; }
        [NotMapped]
        public DateTime Date_Three { get; set; }
        [NotMapped]
        public decimal Year_One { get; set; }
        [NotMapped]
        public decimal Year_Two { get; set; }
        [NotMapped]
        public decimal Year_Three { get; set; }

        [ForeignKey("Corpid")]
        public CorpReg CorpReg { get; set; }
        public int Corpid { get; set; }
        [ForeignKey("CorpEarningid")]
        public OurCorpSetUp KorpEarning { get; set; }
        public int CorpEarningid { get; set; }

       
       
    }
}

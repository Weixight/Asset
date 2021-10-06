using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class CorpEarning
    {
        public int id { get; set; }
        [NotMapped]
        [Display(Name ="Item")]
        public string CalculatedItem { get; set; }
        [Display(Name = "Value")]
        public decimal ValueAmount { get; set; }
        [Display(Name = "Value Date")]
        public DateTime ValueDate { get; set; }
        [Display(Name = "Created Date")]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Updated Date")]
        public DateTime DateUpdated { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Updated By")]
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
        [NotMapped]
        [Display(Name = "Average")]
        public decimal AverageValue { get; set; }
        [NotMapped]
        public decimal AverageEarning { get; set; }
        [NotMapped]
        [Display(Name = "Total Eearning")]
        public decimal TotalEearning { get; set; }

        [NotMapped]
        [Display(Name = "Total Expense")]
        public decimal TotalExpense { get; set; }
        [NotMapped]
        [Display(Name = "Average Expense")]
        public decimal AverageExpense { get; set; }
        [NotMapped]
        [Display(Name = "Purpose")]
        public string Purpose { get; set; }

        [NotMapped]
        public decimal Weight1 { get; set; }
        [NotMapped]
        public decimal Weight2 { get; set; }
        [NotMapped]
        public decimal Weight3 { get; set; }



    }
}

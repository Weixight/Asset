using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class CorpEarningSetUp
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
      
        [Display(Name = "Name")]
        [NotMapped]
        public string CorpName { get; set; }
        [Display(Name = "Item")]
        [NotMapped]
        public string CalculatedItemName { get; set; }
        [Display(Name= "Applied")]
        public bool Applied { get; set; }

        [ForeignKey("Corpid")]
        public CorpReg CorpReg { get; set; }
        public int Corpid { get; set; }
        [ForeignKey("CorpEarningid")]
        public AverageMaintanableEarningsWeighted CorpEarning { get; set; }
        public int CorpEarningid { get; set; }
        
        
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}

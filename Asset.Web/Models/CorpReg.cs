using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class CorpReg
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Display(Name ="Registration Number") ]
        public string CopRegNum { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "LGA")]
        public string LGA { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Website")]
        public string Website { get; set; }
        [Display(Name = "Reg State")]
        public string RegState { get; set; }
        [Display(Name = "Reg LGA")]
        public string RegLGA { get; set; }
        public string? CACRegno { get; set; }
        public string? AssetPrefix { get; set; }
        public virtual List<CorpEarningSetUp> CorpEarningSetUps { get; set; } = new List<CorpEarningSetUp>();

    }
}

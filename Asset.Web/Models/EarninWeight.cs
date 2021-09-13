using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class EarninWeight
    {
        [Key]
        public int id { get; set; }
        public decimal ValuePercentage { get; set; }
        public DateTime ValueDate { get; set; }
        public string  ValueYear { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}

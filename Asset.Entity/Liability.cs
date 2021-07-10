using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Asset.Entity
{
   public  class Liability
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Tenure { get; set; }
        public decimal Value { get; set; }
        public string CorpNName { get; set; }
        public int CorpId { get; set; }
        public DateTime AssetDate { get; set; }
    }
}

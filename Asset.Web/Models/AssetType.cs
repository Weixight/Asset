using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Asset.Web
{
    public  class AssetType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Tenure { get; set; }

        [Display(Name ="Assetl/Liability") ]
        public string AL { get; set; }

    }
}

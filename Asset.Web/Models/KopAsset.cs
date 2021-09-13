﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Asset.Web
{
   public class KopAsset
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Tenure { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal  Value { get; set; }
        public string CorpNName { get; set; }
        public string AssetRegNo { get; set; }
        public string CopAssetRegNo { get; set; }
        public int CorpId { get; set; }
        public DateTime AssetDate { get; set; }
    }
}

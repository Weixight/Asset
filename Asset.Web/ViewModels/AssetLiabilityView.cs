using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.ViewModels
{
    public class AssetLiabilityView
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Tenure { get; set; }
        public string ItemType { get; set; }
        public Decimal Value { get; set; }
        public string CopAssetRegNo { get; set; }
        public string CorpNName { get; set; }
        public string TenureName { get; set; }
        public string BasicType { get; set; }
        public int CorpId { get; set; }
        public DateTime AssetDate { get; set; }
    }
}

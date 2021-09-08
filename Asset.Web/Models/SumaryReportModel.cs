using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class SumaryReportModel
    {
        public int id { get; set; }
        public decimal  NonCurrentAsset { get; set; }
        public decimal CurrentAsset { get; set; }
        public decimal NonCurrentLiability { get; set; }
        public decimal CurrentLiabilty { get; set; }
        public decimal TNA { get; set; }
        public int NumberOfDepositor { get; set; }
        public decimal UsingNetAssetBasis { get; set; }
        public decimal EPMC { get; set; }
        public decimal EPR { get; set; }
        public decimal MV { get; set; }
        public decimal Valuation { get; set; }
        public int CorpId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class AverageMaintanableEarnings
    {
        public int id { get; set; }
        public int CorpId { get; set; }
        public string CorpName { get; set; }
        public decimal Revenue { get; set; }
        public decimal Costofgoodssold { get; set; }
        public decimal Grossprofit { get; set; }
        public decimal Operatingexpenses { get; set; }
        public decimal Otherexpenses{ get; set; }
        public decimal Depreciation{ get; set; }
        public decimal Profitbeforetax{ get; set; }
        public decimal Incometaxpaid{ get; set; }
        public decimal Profitaftertax{ get; set; }
        public  string Year { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Month { get; set; }
        public DateTime ValueDate { get; set; }
        public decimal Weighted { get; set; }


    }
}

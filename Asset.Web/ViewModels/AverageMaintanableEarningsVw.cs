using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asset.Web.Models;

namespace Asset.Web.ViewModels
{
    public class AverageMaintanableEarningsVw
    {
        public AverageMaintanableEarnings averageMaintanableEarnings { get; set; }
        public decimal TotalEarnings { get; set; }
        public decimal AverageEarning { get; set; }
       public decimal CapitalisationRate { get; set; }
        public decimal TotalValuation { get; set; }
        public decimal NetAsset{ get; set; }
        public decimal ThreeYrMaintainableEarning { get; set; }
        public decimal ThreeYrMaintainableEarningWeighted { get; set; }





    }
}

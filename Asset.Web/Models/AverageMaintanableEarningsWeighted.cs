﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Models
{
    public class AverageMaintanableEarningsWeighted
    {
        public int id { get; set; }
        public string CalculatedItem { get; set; }
        public string Purpose { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
       

    }
}

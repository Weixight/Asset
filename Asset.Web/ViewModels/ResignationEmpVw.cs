using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurHr.ViewModels
{
    public class ResignationEmpVw
    {
        public int id { get; set; }
        public string Empid { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string OrgName { get; set; }
        public string PayGrade { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}

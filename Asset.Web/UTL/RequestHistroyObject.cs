using OurHr.Data;
using OurHr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OurHr.UTL
{
    public class RequestHistroyObject
    {
        private readonly ApplicationDbContext _context;
        public RequestHistroyObject( ApplicationDbContext context)
        {
            _context = context;
        }

        public MyRequest MyRequest(string id)
        {
            
            List<StaffAllowance> MythistaffAllowances = new List<StaffAllowance>();
            StaffAllowance MySingle = new StaffAllowance();
            var MyrequestTbl = _context.RequestTbl.FirstOrDefault(K => K.RequestorId == Convert.ToInt32(id));
            var Emp = _context.EmpTbls.FirstOrDefault(K => K.EmpId == Convert.ToInt32(MyrequestTbl.RequestorId));
            //var EmpDeploy = _context.Deployement.FirstOrDefault(K => K.StaffId == id.ToString());
            //var GetGradePay = _context.GradeAlls.Where(k => k.GrSt == id.ToString());
            var Service = _context.ServiceTbl.FirstOrDefault(K => K.ServiceCode == MyrequestTbl.ServiceKode);
            MyRequest requestTbl = new MyRequest();
            requestTbl.RequestorId = (int)Emp.EmpId;
            requestTbl.RequestDate = System.DateTime.Now;
            //requestTbl.
            return requestTbl;
        }
        public RequestHisTbl MyRequestHistroy(MyRequest requestTbl )
        {
            RequestHisTbl requestHisTbl = new RequestHisTbl();
            requestHisTbl.RequestID = requestTbl.RequestId;
            requestHisTbl.RequestActDate = requestTbl.RequestDate;
            requestHisTbl.ServiceCode = requestTbl.ServiceKode;

            return requestHisTbl;
        }
    }
}

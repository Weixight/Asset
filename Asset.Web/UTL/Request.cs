using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OurHr.Data;
using OurHr.Models;
using OurHr.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OurHr.UTL
{
    public class Request
    {
       // private readonly ApplicationDbContext _db;
        private readonly IEmailSenderm _emailSender;
        private ApplicationDbContext _db;
        [Obsolete]
        private IHostingEnvironment _env;

        [Obsolete]
        public Request(ApplicationDbContext db, IEmailSenderm emailSender, IHostingEnvironment env)
        {
            _db = db;
            _emailSender = emailSender;
            _env = env;
          
        }

        //public Request(ApplicationDbContext _applicationDbContext)
        //{
        //    _applicationDbContext = applicationDbContext;
        //}



        [Obsolete]
        public int NewRequest (int StaffId, int ServiceCode, string Comment)
        {
            ComposeBody composeBody = new ComposeBody(_env);
           
            RequestHistroyObject requestHistroyObject = new RequestHistroyObject(_db);
            var MyDeploymentList = _db.Deployement.ToList();
            var MyDeploymentDetails = MyDeploymentList.FirstOrDefault(J => J.StaffId == StaffId.ToString());
            var ApproveeOrgList = _db.MyOrgList.ToList();
            var MyService = ApproveeOrgList.FirstOrDefault( t =>t.MyOrgId == MyDeploymentDetails.MyStructId);
            var MyNextApproveeDetail = ApproveeOrgList.FirstOrDefault(k =>k.MyOrgId == MyService.MyRootId);
            var MyreportToDeploy = MyDeploymentList.FirstOrDefault(k => k.MyStructId == MyNextApproveeDetail.MyOrgId);
            EmpTbl MyReportToDetail = new EmpTbl();
            if (MyreportToDeploy == null)
            {
                MyReportToDetail.Email = "KoyodeWasiu@gmail.com";
            }

            else
            {
                MyReportToDetail = _db.EmpTbls.FirstOrDefault(k => k.EmpId == Convert.ToInt16( MyreportToDeploy.StaffId));

            }
            RequestHisTbl requestHisTbl = new RequestHisTbl();
            
            var requestTbl = OwnRequest(StaffId);
            requestTbl.NextApprovalLevel = MyNextApproveeDetail.MyOrgId;
            requestTbl.NextApprovalName = MyNextApproveeDetail.MyName;
            var lastId = 0;
            requestTbl.ServiceKode = ServiceCode;
            requestHisTbl = requestHistroyObject.MyRequestHistroy(requestTbl);
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                requestHisTbl.Comment = Comment;
                 var empDetails =  _db.EmpTbls.FirstOrDefault(P => P.EmpId == StaffId);
                 _db.RequestTbl.Add(requestTbl);
                  _db.SaveChanges();
                lastId = requestTbl.RequestId;
                requestHisTbl.RequestID = lastId;
                requestHisTbl.ServiceCode = requestTbl.ServiceKode;
                _db.RequestHis.Add(requestHisTbl);
                _db.SaveChanges();
                transaction.Commit();
                _emailSender.SendEmailAsync(empDetails.Email, "Request", composeBody.ReadMyBody(empDetails));
                _emailSender.SendEmailAsync(MyReportToDetail.Email, "Request", composeBody.ReadMyBody(MyReportToDetail));

            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            
            return lastId;
        }

        [Obsolete]
        public int UpdateRequest( int StaffId,int Id,string Status,int ServiceCode, string Comment)
        {
            ComposeBody composeBody = new ComposeBody(_env);
            MyRequest myRequest = new MyRequest();
            RequestFlow requestStatusKonstant = new RequestFlow();
            OurRequestStatus requestStatus = new OurRequestStatus();
            RequestHisTbl requestHisTbl = new RequestHisTbl();
            var MyDeploymentList = _db.Deployement.ToList();
            var MyDeploymentDetails = MyDeploymentList.FirstOrDefault(J => J.StaffId == StaffId.ToString());
            var ApproveeOrgList = _db.MyOrgList.ToList();
            var MyOrgStruct = ApproveeOrgList.FirstOrDefault(K => K.MyOrgId == MyDeploymentDetails.MyStructId);
            var MyService = ApproveeOrgList.FirstOrDefault(t => t.MyOrgId == MyDeploymentDetails.MyStructId);
            var MyNextApproveeDetail = ApproveeOrgList.FirstOrDefault(k => k.MyOrgId == MyService.MyRootId);
            var MyreportToDeploy = MyDeploymentList.FirstOrDefault(k => k.MyStructId == MyDeploymentDetails.MyStructId);
            var MyReportToDetail = _db.EmpTbls.FirstOrDefault(k => k.EmpId == Convert.ToInt16(MyreportToDeploy.StaffId));
            var MyRequestStatus = _db.StatusTbl.ToList();
            var ServiceList = _db.ServiceTbl.ToList();
            
            requestStatus = MyRequestStatus.FirstOrDefault(k => k.StatusName == Status);
            myRequest = _db.RequestTbl.FirstOrDefault(k => k.Id == Id);
            var TheService = ServiceList.FirstOrDefault(k => k.ServiceCode == myRequest.ServiceKode);
            myRequest.RequestId = Id;
            var MyNewRequestStatus = 0;
            var lastId = myRequest.RequestId;
            if (Status == RequestFlow.Disapproved)
            {
                 MyNewRequestStatus = requestStatus.StatusId - 1;                            
            }
            else
            {
                if(TheService.ApprovalLevel >= MyOrgStruct.MyOrgId && myRequest.RequestSatatus !=RequestFlow.Proccessing)
                {
                    if(TheService.ApprovalLevel == MyOrgStruct.MyOrgId)
                    {
                        myRequest.RequestSatatus = RequestFlow.Proccessing;
                    }
                    else
                    {
                        myRequest.RequestSatatus = RequestFlow.Pending;
                    }
                    myRequest.NextApprovalLevel = ServiceList.Where(k => k.ServiceCode == ServiceCode).FirstOrDefault().ServiceOwner;
                    myRequest.NextApprovalName = ServiceList.Where( k =>k.ServiceCode == ServiceCode).FirstOrDefault().ServiceOwnerName;
                }

                else if (TheService.ApprovalLevel == MyOrgStruct.MyOrgId && myRequest.RequestSatatus == RequestFlow.Proccessing)
                {
                    myRequest.RequestSatatus = RequestFlow.Proccessed;
                    myRequest.NextApprovalLevel = ServiceList.Where(k => k.ServiceCode == ServiceCode).FirstOrDefault().ServiceOwner;
                    myRequest.NextApprovalName = ServiceList.Where(k => k.ServiceCode == ServiceCode).FirstOrDefault().ServiceOwnerName;
                }
                else
                {
                    myRequest.RequestSatatus = RequestFlow.Pending;
                    myRequest.NextApprovalLevel = MyNextApproveeDetail.MyOrgId;
                    myRequest.NextApprovalName = MyNextApproveeDetail.MyName;
                }
                 //MyNewRequestStatus = requestStatus.StatusId + 1;       
            }
           

            using var transaction = _db.Database.BeginTransaction();
            try
            {
             
                requestHisTbl.Comment = Comment;
                var ActorDetail = _db.EmpTbls.FirstOrDefault(P => P.EmpId == StaffId);
                var RequestorDetail = _db.EmpTbls.FirstOrDefault(p =>p.EmpId == myRequest.RequestorId);
                myRequest.ActorId = StaffId;
                _db.RequestTbl.Update(myRequest);
                _db.SaveChanges();
                lastId = myRequest.RequestId;
                requestHisTbl.RequestID = lastId;
                requestHisTbl.ServiceCode = myRequest.ServiceKode;
                requestHisTbl.RequestActStaus = myRequest.RequestSatatus; 
                _db.RequestHis.Add(requestHisTbl);
                _db.SaveChanges();
                transaction.Commit();
                _emailSender.SendEmailAsync(ActorDetail.Email, "Request", composeBody.ReadMyBody(ActorDetail));
                _emailSender.SendEmailAsync(RequestorDetail.Email, "Request", composeBody.ReadMyBody(ActorDetail));

            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            return lastId;
        }

        private MyRequest OwnRequest(int StaffId)
        {
            var MyNameList = _db.Deployement.ToList();
            var MyName = MyNameList.FirstOrDefault(k => k.StaffId == StaffId.ToString());
            var MyDetail = _db.EmpTbls.FirstOrDefault(k => k.EmpId == StaffId);
            var MyStr = _db.MyOrgList.ToList();
            var HighestRequestId = _db.RequestTbl.Max(J => J.RequestId);
            var myLevel = MyStr.FirstOrDefault(K => K.MyOrgId == MyName.MyStructId);
            var TheNextApproval = MyStr.FirstOrDefault(g => g.MyOrgId == myLevel.MyRootId);
            MyRequest myRequest = new MyRequest();       
            myRequest.RequestId =  HighestRequestId + 1;
            myRequest.RequestorId = StaffId;
            myRequest.Requestor = MyDetail.Firstname + " " + MyDetail.LastName;
            myRequest.NextApprovalLevel = TheNextApproval.MyOrgId;
            myRequest.NextApprovalName = TheNextApproval.MyName;
            myRequest.RequestDate = System.DateTime.Now;
            myRequest.NextRequestStatus = "Submitted";
            myRequest.ServiceKode = 4;
            var Theservice = _db.ServiceTbl.FirstOrDefault(k => k.ServiceCode == myRequest.ServiceKode);
            if(Theservice.ApprovalRequired == false)
            {
                myRequest.RequestSatatus = RequestFlow.Proccessing;
            }
            else
            {
                myRequest.RequestSatatus = RequestFlow.Submit;
            }
            // _db.RequestTbl.Add(myRequest);
            //_db.SaveChanges();
            // _ = RequestNotification(MyDetail.Email);

            return myRequest;

          //  return null;
        }

        public async Task RequestNotification(string RequestorEmail)
        {
            string confirmationLink = "";// Url.Action("ConfirmEmail", "Account", new { userid = user.Id, token = confirmationToken }, protocol: HttpContext.Request.Scheme);
            string subject = "New Request Notification";
            await _emailSender.SendEmailAsync(RequestorEmail, subject, confirmationLink);
        }
    }
}

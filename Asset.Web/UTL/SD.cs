using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections;
using System.IO;

namespace Asset.Web.UTL
{
   
    public class TenureType
    {
        public const string NonCurrentAsset = "Non Current Asset";
        public const string CurrentAsset = "Current Asset";
        public const string NonCurrentLiability = "Non Current Liability";
        public const string CurrentLiability = "Current Liability";
        public const string Asset = "Asset";
        public const string Liability = "Liability";



    }

    public class MailSubjet
    {
        public const string Welcome = "Welcome";
        public const string AccountActivation = "Account Activation";
    }


    public class Mailkonfig
    {
        public const string SenderEmail = "lasginfo@alphabetallp.com";
        public const string EmailPassword = "Alphabeta@123$";
        public const string SMTPhost = "smtp.office365.com";
        public const int port = 587;

    }

    public class RequestFlow
    {
        public const string Submit = "Submit";
        public const string Pending = "Pending";
        public const string Approved = "Approved";
        public const string FinalApproved = "Final Approved";
        public const string Disapproved = "Disapproved";
        public const string Proccessing = "Proccessing";
        public const string Proccessed = "Proccessed";
    }

    //public class Mailkonfig
    //{
    //    public const string SenderEmail = "wasiu.olayinka@weixight.com.ng";
    //    public const string EmailPassword = "Password2$";
    //    public const int port =  25;
    //    public const string SMTPhost = "mail.weixight.com.ng";

    //}


}

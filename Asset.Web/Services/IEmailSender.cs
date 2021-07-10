using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asset.Web.Services
{
    public interface IEmailSenderm
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}

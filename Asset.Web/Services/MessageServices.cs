using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Asset.Web.UTL;

namespace Asset.Web.Services
{
    public class AuthMessageSender : IEmailSenderm, ISmsSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                //From Address  
                string FromAddress = Mailkonfig.SenderEmail;
                string FromAdressTitle = "Account Activation";
                //To Address  
                string ToAddress = email;
                string ToAdressTitle = "HRM Email Confirmation";
                string Subject = subject;
                string BodyContent = message;

                //Smtp Server  
                string SmtpServer = Mailkonfig.SMTPhost;                //Smtp Port Number  
                int SmtpPortNumber = Mailkonfig.port;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress
                                        (FromAdressTitle,
                                         FromAddress
                                         ));
                mimeMessage.To.Add(new MailboxAddress
                                         (ToAdressTitle,
                                         ToAddress
                                         ));
                mimeMessage.Subject = Subject; //Subject
                mimeMessage.Body = new TextPart("html")
                {
                    Text = BodyContent
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    client.Authenticate(
                        Mailkonfig.SenderEmail,  //Enter your email here
                        Mailkonfig.EmailPassword //Enter your Password here.
                        );
                    await client.SendAsync(mimeMessage);
                    Console.WriteLine("The mail has been sent successfully !!");
                    //Console.ReadLine();
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}

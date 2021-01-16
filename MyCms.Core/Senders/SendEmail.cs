using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace MyCms.Core.Senders
{
    public class SendEmail
    {
        public static void Send(string to, string subject, string body)
        {
            var fromAddress = new MailAddress("hossein.naeemaei18@outlook.com", "My Website");
            var toAddress = new MailAddress(to, "Your Name"); 
            const string fromPassword = "Hossein@Naeemaei8";

            var smtp = new SmtpClient();
            smtp.Host = "smtp.live.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(fromAddress.Address, fromPassword);
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })

            {
                smtp.Send(message);
            }

        }
    }
}
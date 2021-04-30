using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BerendBebe.MvcHelpers.Mail
{
    public class SendMail
    {
        public static void MailSender(string subject, string body, string sendMail)
        {


            var fromAddress = new MailAddress("youremail@gmail.com");
            var toAddress = new MailAddress(sendMail);
            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,

                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "yourpassword")
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body.ToString() })
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace CourseManagementSystem
{
    public class EmailNotifications
    {
        private static readonly SmtpClient SmtpClient = new SmtpClient();

        public static Task Send(string recipient, string subject, string body)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(recipient));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            return SmtpClient.SendMailAsync(message);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNE.Services
{
    public class GmailSMTP
    {
        public static MailMessage CreateMail(string subject, string body)
        {
            var from = new MailAddress("create.the.adress@gmail.com", "Company name");
            var to = new MailAddress("companies.reports@mail.ru");
            var mail = new MailMessage(from, to);

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            return mail;
        }
        public static void SendMail( MailMessage mail)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials= new NetworkCredential("create.the.adress@gmail.com", "eysgtbadvteylrql");
            smtpClient.EnableSsl= true;
            smtpClient.Send(mail);
        }
    }
}

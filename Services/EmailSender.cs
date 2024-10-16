using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Project.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string destination, string customerName, string htmlMessage)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("trehieu040520@gmail.com");
                mail.To.Add(destination);
                mail.Subject = "Shop";
                mail.Body = htmlMessage;
                //mail.SubjectEncoding = System.Text.Encoding.UTF8;
                //mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;

                using SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("trehieu040520@gmail.com", "hieu01685360016");
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BenimProjem.BL.Services
{
    public static class MailService
    {
        public static bool SendMail(string email, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.Subject = subject;
                mail.Body = body;
                mail.To.Add(email);
                mail.From = new MailAddress("projectmy648@gmail.com", "FatihinProjesi", System.Text.Encoding.UTF8);

                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("projectmy648@gmail.com", "aklsjdLKAJ89yh+12g57gsaASd");
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace Lab5
{
    public static class SMTPManager
    {
        private static readonly string smtpServer = "smtp.gmail.com";
        private static readonly int smtpPort = 587;
        private static readonly string username = "";
        private static readonly string password = "";
        private static readonly bool enableSSL = true;

        public static void SendEmail(string toEmail, string subject, string body)
        {
            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                client.Credentials = new NetworkCredential(username, password);
                client.EnableSsl = enableSSL;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(username),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

                try
                {
                    client.Send(mailMessage);
                }
                catch (SmtpException ex)
                {
                    throw new InvalidOperationException("Помилка при відправленні електронного листа: " + ex.Message);
                }
            }
        }
    }
}
using InsightHub.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace InsightHub.Services
{
    public class EmailSenderServices : IEmailSenderServices
    {
        private static string mailBody = $"Dear User,\nNew report just came in. Be the first to download it. *HyperLink to insighthub url*";
        private static string mailSubject = "New Report Just Got Uploaded!";
        private static string ownerMail = "insighthub.official@gmail.com";
        /// <summary>
        /// Sends an automated message to an address.
        /// </summary>
        /// <param name="to">The target recipient Email address</param>
        public void AutoSendMail(string to)
        {
            var mailMessage = MailMessageMapper(to);
            var smtpClient = SmtpClientMapper();

            smtpClient.Send(mailMessage);
        }

        /// <summary>
        /// Maps the Subject, Body and From address of automated emails.
        /// </summary>
        /// <param name="to">The target recipient Email address.</param>
        /// <returns>MailMessage</returns>
        private MailMessage MailMessageMapper(string to)
        {
            var sendTo = to.Split(',').ToList();
            var mailM = new MailMessage();

            foreach (var email in sendTo)
            {
                mailM.To.Add(email);
            }
            mailM.Body = mailBody;
            mailM.Subject = mailSubject;
            mailM.From = new MailAddress(ownerMail);
            mailM.IsBodyHtml = false;
            return (mailM);
        }
        /// <summary>
        /// Maps the SMTP client for automated mailing.
        /// </summary>
        /// <returns>SmtpClient</returns>
        private SmtpClient SmtpClientMapper()
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = true,
                EnableSsl = true,
                Credentials = new NetworkCredential(ownerMail, "InsightHub")
            };
            return smtpClient;
        }
    }
}

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
        public void AutoSendMail(string to)
        {
            var mailMessage = MailMessageMapper(to);
            mailMessage.To.Clear();
            mailMessage.To.Add("teodor.atan.markov@gmail.com");
            var smtpClient = SmtpClientMapper();

            smtpClient.Send(mailMessage);
        }

        private MailMessage MailMessageMapper(string to)
        {
            var sendTo = to.Split(',').ToList();
            var mailM = new MailMessage();

            foreach (var email in sendTo)
            {
                mailM.To.Add(email);
            }
            mailM.Body = $"Dear User,\nNew report just came in. Be the first to downloaded it. *HyperLink to insighthub url*";
            mailM.Subject = "New Report Just Got Uploaded!";
            mailM.From = new MailAddress("insighthub.official@gmail.com");
            mailM.IsBodyHtml = false;
            return (mailM);
        }
        private SmtpClient SmtpClientMapper()
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("insighthub.official@gmail.com", "InsightHub");
            return smtpClient;
        }
    }
}

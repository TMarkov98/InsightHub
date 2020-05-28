using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Manage.Internal;
using InsightHub.Web.Models;
using Microsoft.CodeAnalysis.Emit;
using System.Net;

namespace InsightHub.Web.Controllers
{
    public class ContactWithUsController : Controller
    {
        // GET: Index
        public IActionResult Index()
        {
            return View();
        }
        // GET: Reports/Create
        public IActionResult ContactWithUs()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ContactWithUs(SendEmailModel sendEmailModel)
        {
            sendEmailModel.Body = $"Email from: {sendEmailModel.SentFrom}.\n" + sendEmailModel.Body;
            var mailMessage = MailMessageMapper("insighthub.official@gmail.com", sendEmailModel.Subject, sendEmailModel.Body);
            var smtpClient = SmtpClientMapper();

            smtpClient.Send(mailMessage);
            return RedirectToAction(nameof(Index));
        }
        private MailMessage MailMessageMapper(string to, string subject, string body)
        {
            var mailM = new MailMessage();
            mailM.To.Add(to);
            mailM.Subject = subject;
            mailM.Body = body;
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
        //public IActionResult SendMail(string to, string subject, string body)
        //{
        //    var mailMessage = MailMessageMapper(to, subject, body);
        //    var smtpClient = SmtpClientMapper();

        //    smtpClient.Send(mailMessage);
        //    return View();
        //}


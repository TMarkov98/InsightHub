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
        /// <summary>
        /// Load Contact with us Page
        /// </summary>
        /// <returns>On success - View</returns>
        // GET: Index
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Load Contact With Us Page
        /// </summary>
        /// <returns>On success - View</returns>
        // GET: ContactWithUs
        public IActionResult ContactWithUs()
        {
            return View();
        }
        /// <summary>
        /// Send Email to email
        /// </summary>
        /// <param name="sendEmailModel">Email Model (Data Transfer Object)</param>
        /// <returns>On success - Redirect to Index View</returns>
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
        /// <summary>
        /// Map Mail Message
        /// </summary>
        /// <param name="to">The emails to send to.</param>
        /// <param name="subject">The subject of the email</param>
        /// <param name="body">The body of the email.</param>
        /// <returns>On success - MailMessage</returns>
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
        /// <summary>
        /// The SmptpClient
        /// </summary>
        /// <returns>On success - SmtpClient</returns>
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


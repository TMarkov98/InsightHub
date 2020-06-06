using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InsightHub.Data;
using InsightHub.Data.Entities;
using System.Security.Claims;
using InsightHub.Services.Contracts;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace InsightHub.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PendingReportsController : Controller
    {
        private readonly IReportServices _reportServices;
        private readonly IUserServices _userServices;
        private readonly IEmailSenderServices _emailSenderServices;

        public PendingReportsController(IReportServices reportServices, IUserServices userServices, IEmailSenderServices emailSenderServices)
        {
            _reportServices = reportServices ?? throw new ArgumentNullException("ReportServices can NOT be null");
            _userServices = userServices ?? throw new ArgumentNullException("UserServices can NOT be null");
            _emailSenderServices = emailSenderServices ?? throw new ArgumentNullException("EmailSenderServices can NOT be null");
        }

        /// <summary>
        /// Get All Pending Reports
        /// </summary>
        /// <param name="search">The string to search for</param>
        /// <param name="pageNumber">The int for a page number</param>
        ///<returns>On success - View with reports(in a paged list). </returns>
        /// <response code="200">Returns All Pending Reports(in a paged list).</response>
        // GET: Admin/PendingReports/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Index(string sort, string search, int? pageNumber)
        {

            ViewData["CurrentSort"] = sort;
            ViewData["SortByTitle"] = sort == "title" ? "title_desc" : "title";
            ViewData["SortByAuthor"] = sort == "author" ? "author_desc" : "author";
            ViewData["SortByIndustry"] = sort == "industry" ? "industry_desc" : "industry";
            ViewData["SortByDate"] = sort == "newest" ? "oldest" : "newest";
            ViewData["SortByDownloads"] = sort == "downloads" ? "downloads_asc" : "downloads";

            if (search != null)
            {
                pageNumber = 1;
            }

            ViewData["Search"] = search;

            var reports = await _reportServices.GetPendingReports(sort, search);

            ViewData["ResultsCount"] = reports.Count;

            var pageSize = 8;
            return View(await reports.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }
        /// <summary>
        /// Aprove report
        /// </summary>
        /// <param name="id">The id of the report.</param>
        /// <returns>On success - Redirect to Index View</returns>
        /// <response code="308">Approved - Redirect To Index View.</response>
        /// <response code="404">If id is null - NotFound</response>
        // Get: Admin/PendingReports/Approve/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _reportServices.ApproveReport(id.Value);
            var approvedReport = await _reportServices.GetReport(id.Value);
            var subscribedEmails = await _userServices.GetSubscribedUsers(approvedReport.Industry);
            if (subscribedEmails.Length > 0)
            {
                _emailSenderServices.AutoSendMail(subscribedEmails);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

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

namespace InsightHub.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PendingReportsController : Controller
    {
        private readonly IReportServices _reportServices;
        private readonly IUserServices _userServices;

        public PendingReportsController(IReportServices reportServices, IUserServices userServices)
        {
            _reportServices = reportServices ?? throw new ArgumentNullException("ReportServices can NOT be null");
            _userServices = userServices ?? throw new ArgumentNullException("UserServices can NOT be null");
        }

        // GET: Admin/PendingReports
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

            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var reports = await _reportServices.GetReportsPending(sort, search);
            var pageSize = 10;
            return View(await reports.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }
        public async Task<IActionResult> Approve(int? id)
        {
            {
                if (id == null)
                {
                    return NotFound();
                }
                await _reportServices.ApproveReport(id.Value);
                var approvedReport = await _reportServices.GetReport(id.Value);
                var subscribedEmails = await _userServices.GetSubscribedUsers(approvedReport.Industry);
                _reportServices.AutoSendMail(subscribedEmails);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

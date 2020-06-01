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

namespace InsightHub.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DeletedReportsController : Controller
    {
        private readonly IReportServices _reportServices;

        public DeletedReportsController(IReportServices reportServices)
        {
            _reportServices = reportServices;
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
            var reports = await _reportServices.GetDeletedReports(sort, search);
            var pageSize = 10;
            return View(await reports.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Remove(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _reportServices.GetReport(id.Value);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(int id)
        {
            await _reportServices.PermanentlyDeleteReport(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Restore(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            await _reportServices.RestoreReport(id.Value);

            return RedirectToAction(nameof(Index));
        }
    }
}

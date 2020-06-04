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
    public class DeletedReportsController : Controller
    {
        private readonly IReportServices _reportServices;

        public DeletedReportsController(IReportServices reportServices)
        {
            _reportServices = reportServices;
        }

        /// <summary>
        /// Get All Deleted Users
        /// </summary>
        /// <param name="search">The string to search for</param>
        /// <param name="pageNumber">The int for a page number</param>
        ///<returns>On success - View with users(in a paged list). </returns>
        /// <response code="200">Returns All Deleted Users(in a paged list).</response>
        // GET: Admin/BannedUsers
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

            var reports = await _reportServices.GetDeletedReports(sort, search);

            ViewData["ResultsCount"] = reports.Count;

            var pageSize = 8;
            return View(await reports.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }
        /// <summary>
        /// Permanently delete a report(load form view)
        /// </summary>
        /// <param name="id">The id of the report</param>
        ///<returns>On success - load Remove form view.</returns>
        /// <response code="200">Load Remove form view.</response>
        /// <response code="404">If id or user is null - NotFound</response>
        // GET: Admin/DeletedUsers/Remove/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Permanently delete a report
        /// </summary>
        /// <param name="id">The id of the report.</param>
        /// <returns>On success - Redirect to Index View</returns>
        /// <response code="308">Deleted - Redirect To Index View.</response>
        // POST: Admin/DeletedUsers/Remove/5
        [HttpPost, ActionName("Remove")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveConfirmed(int id)
        {
            await _reportServices.PermanentlyDeleteReport(id);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Restore report
        /// </summary>
        /// <param name="id">The id of the report.</param>
        /// <returns>On success - Redirect to Index View</returns>
        /// <response code="308">Deleted - Redirect To Index View.</response>
        /// <response code="404">If id is null - NotFound</response>
        // POST: Admin/DeletedUsers/Remove/5
        [HttpPost, ActionName("Remove")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ValidateAntiForgeryToken]
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

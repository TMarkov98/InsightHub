using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InsightHub.Data;
using InsightHub.Data.Entities;
using InsightHub.Services.Contracts;
using InsightHub.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace InsightHub.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportServices _reportServices;
        private readonly IBlobServices _blobServices;
        private readonly IUserServices _userServices;
        private readonly IIndustryServices _industryServices;

        public ReportsController(IReportServices reportServices, IBlobServices blobServices, IUserServices userServices, IIndustryServices industryServices)
        {
            _reportServices = reportServices;
            _blobServices = blobServices;
            _userServices = userServices;
            _industryServices = industryServices;
        }

        /// <summary>
        /// Get all Reports
        /// </summary>
        /// <param name="sort">A string to sort by.</param>
        /// <param name="search">A string to search for.</param>
        /// <param name="author">A string to filter by.</param>
        /// <param name="industry">A string to filter by.</param>
        /// <param name="tag">A string to filter by.</param>
        /// <param name="pageNumber">A int page number.</param>
        /// <returns>On success - View with reports(in a paged list). </returns>
        /// <response code="200">Returns All Reports(in a paged list).</response>
        // GET: Reports
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Index(string sort, string search, string author, string industry, string tag, int? pageNumber)
        {
            ViewData["CurrentSort"] = sort;
            ViewData["SortByTitle"] = sort == "title" ? "title_desc" : "title";
            ViewData["SortByAuthor"] = sort == "author" ? "author_desc" : "author";
            ViewData["SortByIndustry"] = sort == "industry" ? "industry_desc" : "industry";
            ViewData["SortByDate"] = sort == "newest" ? "oldest" : "newest";
            ViewData["SortByDownloads"] = sort == "downloads" ? "downloads_asc" : "downloads";
            ViewData["PageNumber"] = pageNumber;
            ViewData["Search"] = search;
            ViewData["Industry"] = industry;
            ViewData["Tag"] = tag;
            ViewData["Author"] = author;

            var reports = await _reportServices.GetReports(sort, search, author, industry, tag);
            ViewData["ResultsCount"] = reports.Count;

            int pageSize = 8;
            return View(await reports.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }

        /// <summary>
        /// Gets Details View of certain Report
        /// </summary>
        /// <param name="id">The id of the certain report</param>
        /// <returns>On success - View of certain report's Details</returns>
        /// <response code="200">Returns View of certain report's Details.</response>
        /// <response code="404">If id or the report is null - NotFound.</response>
        // GET: Reports/Details/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound("Id can NOT be null");

            var report = await _reportServices.GetReport(id.Value);
            if (report == null)
                return NotFound("Report not found.");

            ViewData["Tags"] = report.Tags.Split(',').Select(t => t.Trim());

            return View(report);
        }
        /// <summary>
        /// Download a Report's content
        /// </summary>
        /// <param name="id">The id of the Report</param>
        /// <returns>On success - A Report's File
        /// If the Report does not exists - Throws Argument Null Exception</returns>
        /// <response code="200">Returns an Report's File</response>
        /// <response code="404">If id or report is null - NotFound</response>
        // GET: Reports/5/download
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin, Client")]
        public async Task<IActionResult> Download(int? id)
        {
            if (id == null)
                return NotFound("Report not found.");
            var report = await _reportServices.GetReport(id.Value);
            if (report == null)
                return NotFound("Report not found.");
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _reportServices.AddToDownloadsCount(userId, id.Value);
            var data = await _blobServices.GetBlobAsync($"{report.Id}.pdf");
            return File(data.Content, "application/pdf", report.Title);
        }

        /// <summary>
        /// Load the Create View
        /// </summary>
        /// <returns>On success - form view</returns>
        // GET: Reports/Create
        [Authorize(Roles = "Admin, Author")]
        public async Task<IActionResult> Create()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userServices.GetUser(userId);
            var industries = await _industryServices.GetAllIndustries(null, null);
            ViewData["Industry"] = new SelectList(industries.Select(i => i.Name));
            ViewData["Author"] = user.Email;
            return View();
        }

        /// <summary>
        /// Create a new Report
        /// </summary>
        /// <param name="report">Report to Bind</param>
        /// <returns>On success - Redirect to IndexView
        /// If ModelState is not true, load same page.</returns>
        /// <response code="308">Created - redirected to IndexView.</response>
        /// <response code="204">Not created - same view.</response>
        // POST: Reports/Create
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Summary,Description,Author,ImgUrl,Industry,Tags")] ReportModel report, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //Validate File Exists
                if (file == null)
                {
                    throw new ArgumentException("Please upload a file.");
                }

                //Validate File Extension
                else
                {
                    string[] permittedExtensions = { ".pdf" };


                    var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

                    if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                    {
                        throw new ArgumentException("Invalid file format. Please provide a PDF.");
                    }
                }
                
                //Create Report
                await _reportServices.CreateReport(report.Title, report.Summary, report.Description, report.Author, report.ImgUrl, report.Industry, report.Tags);

                //Get New Report ID
                var reportId = await _reportServices.GetReportsCount();
                //Upload Report File to Blob
                using (var stream = file.OpenReadStream())
                {
                    await _blobServices.UploadFileBlobAsync(stream, $"{reportId}.pdf");
                }
                return RedirectToAction("ReportPending", "Home");
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Edit an existring report(load form view) 
        /// </summary>
        /// <param name="id">The id of the edited report</param>
        /// <returns>On success - load Edit form view.</returns>
        /// <response code="200">Load Edit form view.</response>
        /// <response code="404">If id or report is null - NotFound</response>
        // GET: Reports/Edit/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin, Author")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound("Id can NOT be null");

            var report = await _reportServices.GetReport(id.Value);

            if (report == null)
                return NotFound("Report not found.");

            var industries = await _industryServices.GetAllIndustries(null, null);
            ViewData["Industry"] = new SelectList(industries.Select(i => i.Name));

            return View(report);
        }

        /// <summary>
        /// Edit an existing Report
        /// </summary>
        /// <param name="industry">Report to Bind</param>
        /// <returns>On success - Redirect to IndexView
        /// If ModelState is not true, load same page.
        /// If id is not the same - NotFound()</returns>
        /// <response code="308">Edited - redirected to IndexView.</response>
        /// <response code="404">Not edited - NotFound.</response>
        // POST: Reports/Edit/2
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Summary,Description,ImgUrl,Industry,Tags")] ReportModel report, IFormFile file)
        {
            if (id != report.Id)
                return NotFound("Id can NOT be null");

            if (ModelState.IsValid)
            {
                //Update Report
                await _reportServices.UpdateReport(id, report.Title, report.Summary, report.Description, report.ImgUrl, report.Industry, report.Tags);

                //Check if a new File was uploaded
                if (file != null)
                {
                    //Check that File is a PDF
                    string[] permittedExtensions = { ".pdf" };

                    var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

                    if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                    {
                        throw new ArgumentException("Invalid file format. Please provide a PDF.");
                    }

                    //Upload Report File to Blob
                    using var stream = file.OpenReadStream();
                    await _blobServices.UploadFileBlobAsync(stream, $"{report.Id}.pdf");
                }

                return RedirectToAction("ReportPending", "Home");
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Changes the state of the IsFeatured Bool in the target Report
        /// </summary>
        /// <param name="id">The ID of the target Report</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleFeatured(int? id)
        {
            if(id == null)
            {
                return NotFound("Id cannot be null.");
            }
            await _reportServices.ToggleFeatured(id.Value);

            return RedirectToAction(nameof(Details), new { id });
        }

        /// <summary>
        /// Delete an existring report (load form view)
        /// </summary>
        /// <param name="id">The id of the edited report</param>
        /// <returns>On success - load Edit form view.</returns>
        /// <response code="200">Load Delete form view.</response>
        /// <response code="404">If id or report is null - NotFound</response>
        // GET: Reports/Delete/5
        [Authorize(Roles = "Admin, Author")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound("Id can NOT be null");

            var report = await _reportServices.GetReport(id.Value);
            if (report == null)
                return NotFound("Report not found.");

            return View(report);
        }

        /// <summary>
        /// Delete an existing report
        /// </summary>
        /// <param name="id">The id of the deleted report.</param>
        /// <returns>On success - Redirect to Index view</returns>
        /// <response code="200">Load Delete form view.</response>
        // POST: Reports/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reportServices.DeleteReport(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

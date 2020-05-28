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

        // GET: Reports
        public async Task<IActionResult> Index(string sort, string search, string author, string industry, string tag, int? pageNumber)
        {
            ViewData["CurrentSort"] = sort;
            ViewData["SortByTitle"] = sort == "title" ? "title_desc" : "title";
            ViewData["SortByAuthor"] = sort == "author" ? "author_desc" : "author";
            ViewData["SortByIndustry"] = sort == "industry" ? "industry_desc" : "industry";
            ViewData["SortByDate"] = sort == "newest" ? "oldest" : "newest";
            ViewData["SortByDownloads"] = sort == "downloads" ? "downloads_asc" : "downloads";

            if (search != null || industry != null || tag != null || author != null)
            {
                pageNumber = 1;
            }
            ViewData["Search"] = search;
            ViewData["Industry"] = industry;
            ViewData["Tag"] = tag;
            ViewData["Author"] = author;

            var reports = await _reportServices.GetReports(sort, search, author, industry, tag);
            ViewData["ResultsCount"] = reports.Count;

            int pageSize = 10;
            return View(await reports.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound("Id can NOT be null");

            var report = await _reportServices.GetReport(id.Value);
            if (report == null)
                return NotFound("Report not found.");

            ViewData["AuthorEmail"] = report.Author.Split("- ").Select(a => a.Trim()).Last();
            ViewData["Tags"] = report.Tags.Split(',').Select(t => t.Trim());

            return View(report);
        }

        // GET: Reports/Details/5/Download
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
            var data = await _blobServices.GetBlobAsync($"{report.Title}.pdf");
            return File(data.Content, "application/pdf");
        }

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

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
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
                
                //Upload Report File to Blob
                using (var stream = file.OpenReadStream())
                {
                    await _blobServices.UploadFileBlobAsync(stream, $"{report.Title}.pdf");
                }
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Reports/Edit/5
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

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImgUrl,Industry,Tags")] ReportModel report)
        {
            if (id != report.Id)
                return NotFound("Id can NOT be null");

            if (ModelState.IsValid)
            {
                await _reportServices.UpdateReport(id, report.Title, report.Summary, report.Description, report.ImgUrl, report.Industry, report.Tags);
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }
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

        // GET: Reports/Delete/5
        [Authorize(Roles = "Admin, Author")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound("Id can NOT be null");

            var report = await _reportServices.GetReport(id.Value);
            if (report == null)
                return NotFound("Report not found.");

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reportServices.DeleteReport(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

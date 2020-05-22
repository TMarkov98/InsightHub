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

namespace InsightHub.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportServices _reportServices;
        private readonly IBlobServices _blobServices;

        public ReportsController(IReportServices reportServices, IBlobServices blobServices)
        {
            _reportServices = reportServices;
            _blobServices = blobServices;
        }

        // GET: Reports
        public async Task<IActionResult> Index(string sort, string search, string author, string industry, string tag, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sort;
            ViewData["SortByTitle"] = string.IsNullOrEmpty(sort) || sort == "title" || sort == "name" ? "title_desc" : "title";
            ViewData["SortByAuthor"] = string.IsNullOrEmpty(sort) || sort == "author" || sort == "creator" || sort == "user" ? "author_desc" : "author";
            ViewData["SortByIndustry"] = string.IsNullOrEmpty(sort) || sort == "industry" ? "industry_desc" : "industry";
            ViewData["SortByDate"] = sort == "newest" ? "oldest" : "newest";
            ViewData["Search"] = search;

            if (search != null)
            {
                pageNumber = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewData["CurrentFilter"] = search;

            var reports = await _reportServices.GetReports(sort, search, author, industry, tag);
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

            return View(report);
        }

        // GET: Reports/Details/5/Download
        [Authorize]
        public async Task<IActionResult> Download(int? id)
        {
            if (id == null)
                return NotFound("Report not found.");
            var report = await _reportServices.GetReport(id.Value);
            if (report == null)
                return NotFound("Report not found.");
            var data = await _blobServices.GetBlobAsync($"{report.Title}.pdf");
            return File(data.Content, "application/pdf");
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Author,ImgUrl,Industry,Tags")] ReportModel report, IFormFile file)
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

            return View(report);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound("Id can NOT be null");

            var report = await _reportServices.GetReport(id.Value);

            if (report == null)
                return NotFound("Report not found.");

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

        // GET: Reports/Delete/5
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

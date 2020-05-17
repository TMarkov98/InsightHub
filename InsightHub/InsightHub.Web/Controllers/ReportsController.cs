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
        public async Task<IActionResult> Index(string sort, string search, string author, string industry, string tag)
        {
            var reports = await _reportServices.GetReports(sort,search,author,industry,tag);
            return View(reports);
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

        public async Task<IActionResult> Download(int? id)
        {
            if (id == null)
                return NotFound("Report not found.");
            var report = await _reportServices.GetReport(id.Value);
            if (report == null)
                return NotFound("Report not found.");
            var data = await _blobServices.GetBlobAsync(report.Title + ".pdf");
            return File(data.Content, data.ContentType);
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
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Author,Industry,Tags")] ReportModel report)
        {
            if (ModelState.IsValid)
            {
                await _reportServices.CreateReport(report.Title, report.Description, report.Author, report.Industry, report.Tags);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Industry,Tags")] ReportModel report)
        {
            if (id != report.Id)
                return NotFound("Id can NOT be null");

            if (ModelState.IsValid)
            {
                await _reportServices.UpdateReport(id, report.Title, report.Description, report.Industry, report.Tags);
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

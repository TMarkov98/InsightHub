﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace InsightHub.Web.Areas.Author.Controllers
{
    [Area("Author")]
    public class UploadedReportsController : Controller
    {
        private readonly IUserServices _userServices;

        public UploadedReportsController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        // GET: UploadedReports
        [Authorize]
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
            var reports = await _userServices.GetUploadedReports(userId);
            var pageSize = 10;
            return View(await reports.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }
    }
}
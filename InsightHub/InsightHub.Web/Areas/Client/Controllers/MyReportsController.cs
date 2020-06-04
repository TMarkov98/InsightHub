using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace InsightHub.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class MyReportsController : Controller
    {
        private readonly IUserServices _userServices;

        public MyReportsController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        /// <summary>
        /// Get All Downloaded Reports Of Certain User
        /// </summary>
        /// <param name="search">The string to search for</param>
        /// <param name="pageNumber">The int for a page number</param>
        ///<returns>On success - View with reports(in a paged list). </returns>
        /// <response code="200">Returns All Downloaded Reports Of Certain User(in a paged list).</response>
        // GET: MyReports 
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Index(string search, int? pageNumber)
        {
            ViewData["Search"] = search;

            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var reports = await _userServices.GetDownloadedReports(userId, search);

            ViewData["ResultsCount"] = reports.Count;
            var pageSize = 8;
            return View(await reports.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }

    }
}
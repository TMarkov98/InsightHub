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
    public class MySubscriptions : Controller
    {
        private readonly IUserServices _userServices;

        public MySubscriptions(IUserServices userServices)
        {
            _userServices = userServices;
        }

        /// <summary>
        /// Get All Subscriptions Of Certain User
        /// </summary>
        /// <param name="search">The string to search for</param>
        /// <param name="pageNumber">The int for a page number</param>
        ///<returns>On success - View with subscriptions(industries)(in a paged list). </returns>
        /// <response code="200">Returns All Subscriptions Of Certain User(in a paged list).</response>
        // GET: MyReports 
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // GET: MySubscriptions 
        public async Task<IActionResult> Index(string search, int? pageNumber)
        {
            ViewData["PageNumber"] = pageNumber;
            ViewData["Search"] = search;

            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var industries = await _userServices.GetSubscriptions(userId);

            if(search != null)
            {
                industries = industries.Where(i => i.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            ViewData["ResultsCount"] = industries.Count;

            int pageSize = 8;
            return View(await industries.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }
    }
}
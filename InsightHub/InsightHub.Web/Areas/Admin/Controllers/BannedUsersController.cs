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
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace InsightHub.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BannedUsersController : Controller
    {
        private readonly IUserServices _userServices;

        public BannedUsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        /// <summary>
        /// Get All Banned Users
        /// </summary>
        /// <param name="search">The string to search for</param>
        /// <param name="pageNumber">The int for a page number</param>
        ///<returns>On success - View with banned users(in a paged list). </returns>
        /// <response code="200">Returns All Banned Users(in a paged list).</response>
        // GET: Admin/BannedUsers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Index(string search, int? pageNumber)
        {
            var users = await _userServices.GetBannedUsers(search);
            ViewData["ResultsCount"] = users.Count;
            ViewData["Search"] = search;
            if (search != null)
            {
                pageNumber = 1;
            }
            int pageSize = 10;
            return View(await users.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }

        /// <summary>
        /// Unban an existing user(load form view) 
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>On success - load Unban form view.</returns>
        /// <response code="200">Load Unban form view.</response>
        /// <response code="404">If id or user is null - NotFound</response>
        // GET: Admin/BannedUsers/Unban/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Unban(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userServices.GetUser(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        /// <summary>
        /// Unban an existing user
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>On success - Redirect to Index view</returns>
        /// <response code="308">Redirect to Index view.</response>
        // POST: Admin/BannedUsers/Unban/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost, ActionName("Unban")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnbanConfirmed(int id)
        {
            await _userServices.UnbanUser(id);
            return RedirectToAction("Index");
        }
    }
}

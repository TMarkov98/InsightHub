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
    public class PendingUsersController : Controller
    {
        private readonly IUserServices _userServices;

        public PendingUsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        /// <summary>
        /// Get All Pending Users
        /// </summary>
        /// <param name="search">The string to search for</param>
        /// <param name="pageNumber">The int for a page number</param>
        ///<returns>On success - View with users(in a paged list). </returns>
        /// <response code="200">Returns All Pending Users(in a paged list).</response>
        // GET: Admin/PendingUsers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Index(string search, int? pageNumber)
        {
            var users = await _userServices.GetPendingUsers(search);
            ViewData["Search"] = search;
            ViewData["ResultsCount"] = users.Count;
            if (search != null)
            {
                pageNumber = 1;
            }
            int pageSize = 10;
            return View(await users.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }
        /// <summary>
        /// Aprove user
        /// </summary>
        /// <param name="id">The id of the user.</param>
        /// <returns>On success - Redirect to Index View</returns>
        /// <response code="308">Approved - Redirect To Index View.</response>
        /// <response code="404">If id is null - NotFound</response>
        // Get: Admin/PendingUsers/Approve/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Approve(int? id)
        {
            {
                if (id == null)
                {
                    return NotFound();
                }
                await _userServices.ApproveUser(id.Value);

                return RedirectToAction(nameof(Index));
            }
        }
    }
}

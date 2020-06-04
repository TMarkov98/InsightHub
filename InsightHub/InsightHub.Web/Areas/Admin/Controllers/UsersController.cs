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
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace InsightHub.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        /// <summary>
        /// Get All Users
        /// </summary>
        /// <param name="search">The string to search for</param>
        /// <param name="pageNumber">The int for a page number</param>
        ///<returns>On success - View with users(in a paged list). </returns>
        /// <response code="200">Returns All Users(in a paged list).</response>
        // GET: Admin/Users
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Index(string search, int? pageNumber)
        {
            var users = await _userServices.GetUsers(search);
            ViewData["ResultsCount"] = users.Count();
            if(search != null)
            {
                pageNumber = 1;
            }
            int pageSize = 10;
            return View(await users.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }

        /// <summary>
        /// Gets Details View of certain User
        /// </summary>
        /// <param name="id">The id of the certain user</param>
        /// <returns>On success - View of certain user's Details</returns>
        /// <response code="200">Returns View of certain user's Details.</response>
        /// <response code="404">If id or the user is null - NotFound.</response>
        // GET: Admin/Users/Details/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details(int? id)
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
        /// Edit an existring user(load form view) 
        /// </summary>
        /// <param name="id">The id of the edited user</param>
        /// <returns>On success - load Edit form view.</returns>
        /// <response code="200">Load Edit form view.</response>
        /// <response code="404">If id or user is null - NotFound</response>
        // GET: Admin/Users/Edit/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(int? id)
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
        /// Edit an existing User
        /// </summary>
        /// <param name="industry">User to Bind</param>
        /// <returns>On success - Redirect to IndexView
        /// If ModelState is not true, load same page.
        /// If id is not the same - NotFound()</returns>
        /// <response code="308">Edited - redirected to IndexView.</response>
        /// <response code="404">Not edited - NotFound.</response>
        // POST: Users/Edit/2
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,IsPending,CreatedOn,ModifiedOn,IsBanned,BanReason,Id")] UserModel userModel)
        {
            if (id != userModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _userServices.UpdateUser(id, userModel.FirstName, userModel.LastName, userModel.IsBanned, userModel.BanReason);
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }


        /// <summary>
        /// Delete an existring user (load form view)
        /// </summary>
        /// <param name="id">The id of the edited user</param>
        /// <returns>On success - load Edit form view.</returns>
        /// <response code="200">Load Delete form view.</response>
        /// <response code="404">If id or user is null - NotFound</response>
        // GET: Admin/Users/Delete/5
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int? id)
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
        /// Delete an existing user
        /// </summary>
        /// <param name="id">The id of the deleted user.</param>
        /// <returns>On success - Redirect to Index view</returns>
        /// <response code="308">Redirect to Index view</response>
        // POST: Admin/Users/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userServices.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Ban an existring user (load form view)
        /// </summary>
        /// <param name="id">The id of the banned user</param>
        /// <returns>On success - load Edit form view.</returns>
        /// <response code="200">Load Delete form view.</response>
        /// <response code="404">If id or user is null - NotFound</response>
        // GET: Admin/Users/Ban/5
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Ban(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userServices.GetUser(id.Value);
            if(user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        /// <summary>
        /// Ban an existing user
        /// </summary>
        /// <param name="id">The id of the banned user.</param>
        /// <returns>On success - Redirect to Index view</returns>
        /// <response code="308">Redirect to Index view</response>
        // POST: Admin/Users/Ban/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Ban")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BanConfirmed(int id, string banReason)
        {
            await _userServices.BanUser(id, banReason);
            return RedirectToAction("Index");
        }
    }
}

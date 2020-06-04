using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InsightHub.Data;
using InsightHub.Models;
using InsightHub.Services.Contracts;
using System.Net.Http;
using Newtonsoft.Json;
using InsightHub.Data.Entities;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace InsightHub.Web.Controllers
{
    public class IndustriesController : Controller
    {
        private readonly IIndustryServices _industryServices;

        public IndustriesController(IIndustryServices industryServices)
        {
            _industryServices = industryServices;
        }
        /// <summary>
        /// Get all Industries
        /// </summary>
        /// <param name="sort">A string to sort by.</param>
        /// <param name="search">A string to search for.</param>
        /// <param name="pageNumber">A int page number.</param>
        /// <returns>On success - View with industries(in a paged list). </returns>
        /// <response code="200">Returns All Industries(in a paged list).</response>
        // GET: Tags
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Index(string sort, string search, int? pageNumber)
        {
            ViewData["CurrentSort"] = sort;
            ViewData["SortByName"] = sort == "name" ? "name_desc" : "name";
            ViewData["SortByDate"] = sort == "newest" ? "oldest" : "newest";
            ViewData["PageNumber"] = pageNumber;
            ViewData["Search"] = search;

            var industries = await _industryServices.GetAllIndustries(sort, search);
            ViewData["ResultsCount"] = industries.Count;
            int pageSize = 8;
            return View(await industries.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }

        /// <summary>
        /// Gets Details View of certain Industry
        /// </summary>
        /// <param name="id">The id of the certain industry</param>
        /// <returns>On success - View of certain industry's Details</returns>
        /// <response code="200">Returns View of certain industry's Details.</response>
        /// <response code="404">If id or the industry is null - NotFound.</response>
        // GET: Industry/Details/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details(int? id)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewData["SubscriptionExists"] = await _industryServices.SubscriptionExists(userId, id.Value);

            if (id == null)
                return NotFound();

            var industry = await _industryServices.GetIndustry(id.Value);

            if (industry == null)
                return NotFound();

            return View(industry);
        }

        /// <summary>
        /// Subscribe a certain user with some industry
        /// </summary>
        /// <param name="id">The id of the industry</param>
        /// <returns>On success - Redirect to industry's Details View</returns>
        /// <response code="308">Successful subscribed - Redirect to industry's Details View</response>
        /// <response code="404">If id is null - NotFound</response>
        [Authorize(Roles="Client")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Subscribe(int? id)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (id == null)
                return NotFound();
            await _industryServices.AddSubscription(userId, id.Value);
            return RedirectToAction(nameof(Details), new { id });
        }

        /// <summary>
        /// Remove Subscription of a certain user with some industry
        /// </summary>
        /// <param name="id">The id of the industry</param>
        /// <returns>On success - Redirect to industry's Details View</returns>
        /// <response code="308">Successful removed subscription - Redirect to industry's Details View</response>
        /// <response code="404">If id is null - NotFound</response>
        [Authorize(Roles = "Client")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveSubscription(int? id)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (id == null)
                return NotFound();
            await _industryServices.RemoveSubscription(userId, id.Value);
            return RedirectToAction(nameof(Details), new { id });
        }

        /// <summary>
        /// Load the Create View
        /// </summary>
        /// <returns>On success - View</returns>
        // GET: Industries/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create a new Industry
        /// </summary>
        /// <param name="industry">Industry to Bind</param>
        /// <returns>On success - Redirect to IndexView
        /// If ModelState is not true, load same page.</returns>
        /// <response code="308">Created - redirected to IndexView.</response>
        /// <response code="204">Not created - same view.</response>
        // POST: Industries/Create
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Create([Bind("Id,Name,ImgUrl")] Industry industry)
        {
            if (ModelState.IsValid)
            {
                var name = industry.Name;
                var imgUrl = industry.ImgUrl;
                var newIndustry = await _industryServices.CreateIndustry(name, imgUrl);
                return RedirectToAction(nameof(Index));

            }
            return View(industry);
        }

        /// <summary>
        /// Edit an existring industry(load form view) 
        /// </summary>
        /// <param name="id">The id of the edited industry</param>
        /// <returns>On success - load Edit form view.</returns>
        /// <response code="200">Load Edit form view.</response>
        /// <response code="404">If id or industry is null - NotFound</response>
        // GET: Industries/Edit/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var industry = await _industryServices.GetIndustry(id.Value);
            return View(industry);

        }

        /// <summary>
        /// Edit an existing Industry
        /// </summary>
        /// <param name="industry">Industry to Bind</param>
        /// <returns>On success - Redirect to IndexView
        /// If ModelState is not true, load same page.
        /// If id is not the same - NotFound()</returns>
        /// <response code="308">Edited - redirected to IndexView.</response>
        /// <response code="404">Not edited - NotFound.</response>
        // POST: Industries/Edit/2
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImgUrl,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] Industry industry)
        {
            if (id != industry.Id)
                return NotFound();

            if (ModelState.IsValid)
            {

                await _industryServices.UpdateIndustry(industry.Id, industry.Name, industry.ImgUrl);

                return RedirectToAction(nameof(Index));
            }
            return View(industry);
        }

        /// <summary>
        /// Delete an existring industry (load form view)
        /// </summary>
        /// <param name="id">The id of the edited industry</param>
        /// <returns>On success - load Edit form view.</returns>
        /// <response code="200">Load Delete form view.</response>
        /// <response code="404">If id or industry is null - NotFound</response>
        // GET: Industry/Delete/5
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var industry = await _industryServices.GetIndustry(id.Value);
            return View(industry);

        }

        /// <summary>
        /// Delete an existing industry
        /// </summary>
        /// <param name="id">The id of the deleted industry.</param>
        /// <returns>On success - Redirect to Index view</returns>
        /// <response code="308">Redirect to Index view</response>
        // POST: Industries/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _industryServices.DeleteIndustry(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

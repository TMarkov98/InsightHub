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
using InsightHub.Data.Entities;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace InsightHub.Web.Controllers
{
    public class TagsController : Controller
    {
        private readonly ITagServices _tagServices;

        public TagsController(ITagServices tagServices)
        {
            _tagServices = tagServices;
        }

        /// <summary>
        /// Get all Tags
        /// </summary>
        /// <param name="sort">A string to sort by.</param>
        /// <param name="search">A string to search for.</param>
        /// <param name="pageNumber">A int page number.</param>
        /// <returns>On success - View with tags(in a paged list). </returns>
        /// <response code="200">Returns All Tags(in a paged list).</response>
        // GET: Tags
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string sort, string search, int? pageNumber)
        {
            ViewData["CurrentSort"] = sort;
            ViewData["SortByName"] = sort == "name" ? "name_desc" : "name";
            ViewData["SortByDate"] = sort == "newest" ? "oldest" : "newest";
            ViewData["SortByReportsCount"] = sort == "reports" ? "reports_asc" : "reports";
            ViewData["Search"] = search;
            ViewData["PageNumber"] = pageNumber;
            var tags = await _tagServices.GetTags(sort, search);
            int pageSize = 10;
            return View(await tags.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }

        /// <summary>
        /// Gets Details View of certain Tag
        /// </summary>
        /// <param name="id">The id of the certain tag</param>
        /// <returns>On success - View of certain tag's Details</returns>
        /// <response code="200">Returns View of certain tag's Details.</response>
        /// <response code="404">If id or tag is null - NotFound.</response>
        // GET: Tags/Details/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _tagServices.GetTag(id.Value);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        /// <summary>
        /// Load the Create View
        /// </summary>
        /// <returns>On success - View</returns>
        // GET: Tags/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create a new Tag
        /// </summary>
        /// <param name="tag">Tag to Bind</param>
        /// <returns>On success - Redirect to IndexView
        /// If ModelState is not true, load same page.</returns>
        /// <response code="308">Created - redirected to IndexView.</response>
        /// <response code="204">Not created - same view.</response>
        // POST: Tags/Create
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                await _tagServices.CreateTag(tag.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        /// <summary>
        /// Edit an existring tag 
        /// </summary>
        /// <param name="id">The id of the edited tag</param>
        /// <returns>On success - load Edit form view.</returns>
        /// <response code="200">Load Edit form view.</response>
        /// <response code="404">If id or tag is null - NotFound</response>
        // GET: Tags/Edit/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _tagServices.GetTag(id.Value);

            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        /// <summary>
        /// Edit an existing Tag
        /// </summary>
        /// <param name="tag">Tag to Bind</param>
        /// <returns>On success - Redirect to IndexView
        /// If ModelState is not true, load same page.
        /// If id is not the same - NotFound()</returns>
        /// <response code="308">Edited - redirected to IndexView.</response>
        /// <response code="404">Not edited - NotFound.</response>
        // POST: Tags/Edit/2
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] Tag tag)
        {
            if (id != tag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _tagServices.UpdateTag(tag.Id, tag.Name);

                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }
        /// <summary>
        /// Delete an existring tag (load form view)
        /// </summary>
        /// <param name="id">The id of the edited tag</param>
        /// <returns>On success - load Edit form view.</returns>
        /// <response code="200">Load Delete form view.</response>
        /// <response code="404">If id or tag is null - NotFound</response>
        // GET: Tags/Delete/5
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _tagServices.GetTag(id.Value);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }
        /// <summary>
        /// Delete an existing tag
        /// </summary>
        /// <param name="id">The id of the deleted tag.</param>
        /// <returns>On success - Redirect to Index view</returns>
        /// <response code="200">Load Delete form view.</response>
        // POST: Tags/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = await _tagServices.GetTag(id);
            await _tagServices.DeleteTag(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

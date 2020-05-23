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

namespace InsightHub.Web.Controllers
{
    public class IndustriesController : Controller
    {
        private readonly IIndustryServices _industryServices;

        public IndustriesController(IIndustryServices industryServices)
        {
            _industryServices = industryServices;
        }

        // GET: Industries
        [HttpGet]
        public async Task<IActionResult> Index(string sort, string search, int? pageNumber)
        {
            ViewData["CurrentSort"] = sort;
            ViewData["SortByName"] = sort == "name" ? "name_desc" : "name";
            ViewData["SortByDate"] = sort == "newest" ? "oldest" : "newest";

            if (search != null)
            {
                pageNumber = 1;
            }

            ViewData["Search"] = search;

            var industries = await _industryServices.GetAllIndustries(sort, search);
            int pageSize = 10;
            return View(await industries.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }

        // GET: Industries/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id, int? pageNumber)
        {
            if (id == null)
                return NotFound();
            ViewData["PageNumber"] = pageNumber;
            var industry = await _industryServices.GetIndustry(id.Value);
            return View(industry);

        }
        [Authorize]
        public async Task<IActionResult> Subscribe(int? id)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (id == null)
                return NotFound();
            await _industryServices.AddSubscription(userId, id.Value);
            return RedirectToAction(nameof(Details), new { id = id });
        }

        // GET: Industries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Industries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Industries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var industry = await _industryServices.GetIndustry(id.Value);
            return View(industry);

        }

        // POST: Industries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Industries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var industry = await _industryServices.GetIndustry(id.Value);
            return View(industry);

        }

        // POST: Industries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _industryServices.DeleteIndustry(id))
                return RedirectToAction(nameof(Index));
            else
                return NotFound();
        }
    }
}

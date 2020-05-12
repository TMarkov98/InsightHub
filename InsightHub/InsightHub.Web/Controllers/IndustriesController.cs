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
using InsightHub.Services.DTOs;
using Newtonsoft.Json;

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
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5000/api/industries"))
                {
                    try
                    {
                        response.EnsureSuccessStatusCode();
                    }
                    catch (HttpRequestException ex)
                    {
                        //TODO: Add behavior when filter returns status which is not success
                        return View();
                    }
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var industries = JsonConvert.DeserializeObject<List<IndustryDTO>>(apiResponse);
                    return View(industries);
                }
            }
        }

        // GET: Industries/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            try
            {
                var industry = await _industryServices.GetIndustry(id.Value);
                return View(industry);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new { message = ex.Message });
            }
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
        public async Task<IActionResult> Create([Bind("Id,Name")] Industry industry)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var name = industry.Name;
                    var newIndustry = await _industryServices.CreateIndustry(name);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentNullException ex)
                {
                    return NotFound(new { message = ex.Message });
                }
                catch (ArgumentException ex)
                {
                    return NotFound(new { message = ex.Message });
                }
            }
            return View(industry);
        }

        // GET: Industries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            try
            {
                var industry = await _industryServices.GetIndustry(id.Value);
                return View(industry);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // POST: Industries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] Industry industry)
        {
            if (id != industry.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _industryServices.UpdateIndustry(industry.Id, industry.Name);
                }
                catch (ArgumentNullException ex)
                {
                    return NotFound(new { message = ex.Message });
                }
                catch (ArgumentException ex)
                {
                    return NotFound(new { message = ex.Message });
                }
                return RedirectToAction(nameof(Index));
            }
            return View(industry);
        }

        // GET: Industries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            try
            {
                var industry = await _industryServices.GetIndustry(id.Value);
                return View(industry);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(new { message = ex.Message });
            }
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

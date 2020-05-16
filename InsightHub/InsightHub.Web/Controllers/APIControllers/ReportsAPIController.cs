using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using InsightHub.Models;
using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsightHub.Web.Controllers.APIControllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsAPIController : ControllerBase
    {
        private readonly IReportServices _reportServices;
        private readonly IBlobServices _blobServices;

        public ReportsAPIController(IReportServices reportServices, IBlobServices blobServices)
        {
            this._reportServices = reportServices ?? throw new ArgumentNullException("Report Services can NOT be null.");
            this._blobServices = blobServices ?? throw new ArgumentNullException("Blob Services can NOT be null.");
        }

        // GET: api/Reports
        [HttpGet]
        public async Task<IActionResult> Get(string sort, string search, string author, string industry, string tag)
        {
            var model = await _reportServices.GetReports();

            if (sort != null)
            {
                switch (sort.ToLower())
                {
                    case "name":
                    case "title":
                        model = model.OrderBy(r => r.Title).ToList();
                        break;
                    case "author":
                    case "user":
                    case "creator":
                        model = model.OrderBy(r => r.Author).ToList();
                        break;
                    case "industry":
                        model = model.OrderBy(r => r.Industry).ToList();
                        break;
                    case "newest":
                        model = model.OrderByDescending(r => r.CreatedOn).ToList();
                        break;
                    case "oldest":
                        model = model.OrderBy(r => r.CreatedOn).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (search != null)
            {
                model = model.Where(r => r.Title.ToLower().Contains(search.ToLower())
                    || r.Description.ToLower().Contains(search.ToLower())).ToList();
            }

            if (author != null)
            {
                model = model.Where(r => r.Author.ToLower().Contains(author)).ToList();
            }
            if (industry != null)
            {
                model = model.Where(r => r.Industry.ToLower().Contains(industry)).ToList();
            }
            if (tag != null)
            {
                model = model.Where(r => string.Join(' ', r.Tags).ToLower().Contains(tag)).ToList();
            }
            if (model.Count == 0)
            {
                return NotFound(new { message = "No tags found." });
            }
            return Ok(model);
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _reportServices.GetReport(id);
                return Ok(model);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Reports/5/download
        [HttpGet("{id}/download")]
        public async Task<IActionResult> Download(int id)
        {
            try
            {
                var model = await _reportServices.GetReport(id);
                var data = await _blobServices.GetBlobAsync(model.Title + ".pdf");
                return File(data.Content, data.ContentType);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Azure.RequestFailedException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // POST: api/Reports
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReportModel report)
        {
            try
            {
                var model = await _reportServices.CreateReport(report.Title, report.Description, report.Author, report.Industry, report.Tags.ToString());
                return Ok(model);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Reports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ReportModel report)
        {
            try
            {
                var model = await _reportServices.UpdateReport(id, report.Title, report.Description, report.Industry, report.Tags.ToString());
                return Ok(model);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _reportServices.DeleteReport(id);
            if (model)
                return NoContent();
            return BadRequest();
        }
    }
}

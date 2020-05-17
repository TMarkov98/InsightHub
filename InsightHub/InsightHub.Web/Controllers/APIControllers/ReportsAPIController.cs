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
            var model = await _reportServices.GetReports(sort,search,author,industry,tag);
            if (model.Count == 0)
            {
                return NotFound(new { message = "No Reports found." });
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

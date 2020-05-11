using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsightHub.Models;
using InsightHub.Services.Contracts;
using InsightHub.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsightHub.Web.Controllers.APIControllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsAPIController : ControllerBase
    {
        private readonly IReportServices _reportServices;

        public ReportsAPIController(IReportServices reportServices)
        {
            this._reportServices = reportServices ?? throw new ArgumentNullException("Report Services can NOT be null.");
        }

        // GET: api/Reports
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await _reportServices.GetReports();
            if (model.Count == 0)
            {
                return Ok(new { message = "No tags found." });
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

        // POST: api/Reports
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReportDTO reportDTO)
        {
            try
            {
                var model = await _reportServices.CreateReport(reportDTO.Title, reportDTO.Description, reportDTO.Author, reportDTO.Industry, reportDTO.Tags.ToString());
                return Ok(model);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Reports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ReportDTO reportDTO)
        {
            try
            {
                var model = await _reportServices.UpdateReport(id, reportDTO.Title, reportDTO.Description, reportDTO.Industry, reportDTO.Tags.ToString());
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

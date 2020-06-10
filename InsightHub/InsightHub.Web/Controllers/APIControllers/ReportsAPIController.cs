using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Threading.Tasks;
using InsightHub.Models;
using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUserServices _userServices;

        public ReportsAPIController(IReportServices reportServices, IBlobServices blobServices, IUserServices userServices)
        {
            this._reportServices = reportServices;
            this._blobServices = blobServices;
            this._userServices = userServices;
        }

        /// <summary>
        /// Get all Reports
        /// </summary>
        /// <param name="sort">A string to sort by.</param>
        /// <param name="search">A string to search for.</param>
        /// <param name="author">A string to filter by.</param>
        /// <param name="industry">A string to filter by.</param>
        /// <param name="tag">A string to filter by.</param>
        /// <returns>On success - All Reports(sorted or/and filtered). </returns>
        /// <response code="200">Returns All Reports(sorted or/and filtered).</response>
        /// <response code="404">When no industries were found.</response>
        // GET: api/Reports
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string sort, string search, string author, string industry, string tag)
        {
            var model = await _reportServices.GetReports(sort, search, author, industry, tag);
            if (model.Count == 0)
            {
                return NotFound(new { message = "No Reports found." });
            }
            return Ok(model);
        }
        /// <summary>
        /// Get a Report by id
        /// </summary>
        /// <param name="id">The id of the Report.</param>
        /// <returns>On success - A Report Model.
        /// If the Report does not exists - Throws Argument Null Exception</returns>
        /// <response code="200">Returns a Report Model</response>
        // GET: api/Reports/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _reportServices.GetReport(id);
            return Ok(model);
        }

        /// <summary>
        /// Download a Report's content
        /// </summary>
        /// <param name="id">The id of the Report</param>
        /// <returns>On success - A Report's File
        /// If the Report does not exists - Throws Argument Null Exception</returns>
        /// <response code="200">Returns an Report's File</response>
        // GET: api/Reports/5/download
        [HttpGet("{id}/download")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Download(int id)
        {
            var data = await _blobServices.GetBlobAsync(id + ".pdf");
            return File(data.Content, data.ContentType);
        }

        /// <summary>
        /// Create a new Reports
        /// </summary>
        /// <param name="report">The Report Model(Data Transfer Object)</param>
        /// <returns>On success - A Report Model 
        /// If the Report already exists - Throws Argument Exception</returns>
        /// <response code="200">Returns an Report Model</response>
        // POST: api/Reports
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, Author")]
        public async Task<IActionResult> Post([FromBody] ReportModel report)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userServices.GetUser(userId);
            var model = await _reportServices.CreateReport(report.Title, report.Summary, report.Description, user.Email, report.ImgUrl, report.Industry, report.Tags.ToString());
            return Ok(model);
        }

        /// <summary>
        /// Update an existing Report
        /// </summary>
        /// <param name="id">The id of the updated report</param>
        /// <param name="report">The Report Model(Data Transfer Object)</param>
        /// <returns>On success - An updated Report Model. 
        /// If the Report already exists - Throws Argument Exception</returns>
        /// <response code="200">Returns an updated Report Model.</response>
        // PUT: api/Reports/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] ReportModel report)
        {
            var model = await _reportServices.UpdateReport(id, report.Title, report.Summary, report.Description, report.ImgUrl, report.Industry, report.Tags.ToString());
            return Ok(model);
        }

        /// <summary>
        /// Delete a Report
        /// </summary>
        /// <param name="id">The id of the deleted report</param>
        /// <returns>On success - Nothing if the Industry was deleted. 
        /// If the Report does not exists - Throws Argument Null Exception</returns>
        /// <response code="204">Nothing if the Report was deleted</response>
        /// <response code="400">If the Report does not exists - Throws Argument Null Exception</response>            
        // DELETE: api/Delete/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin, Author")]
        public async Task<IActionResult> Delete(int id)
        {
            await _reportServices.DeleteReport(id);
            return NoContent();
        }
    }
}

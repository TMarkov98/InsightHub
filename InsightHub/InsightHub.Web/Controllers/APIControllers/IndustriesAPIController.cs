using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using InsightHub.Services;
using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace InsightHub.Web.Controllers.APIControllers
{
    [Route("api/industries")]
    [ApiController]
    public class IndustriesAPIController : ControllerBase
    {
        private readonly IIndustryServices _industriesServices;
        public IndustriesAPIController(IIndustryServices industryServices)
        {
            this._industriesServices = industryServices;
        }

        /// <summary>
        /// Get all Industries
        /// </summary>
        /// <param name="sort">A string to sort by.</param>
        /// <param name="search">A string to search for.</param>
        /// <returns>On success - All Industries(sorted or/and filtered). </returns>
        /// <response code="200">Returns All Industries(sorted or/and filtered).</response>
        /// <response code="404">When no industries were found.</response>
        // GET: api/Industries
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] string sort, [FromQuery] string search)
        {
            var industries = await _industriesServices.GetAllIndustries(sort, search);
            if (industries.Count == 0)
            {
                return NotFound(new { message = "No industries found." });
            }
            return Ok(industries);
        }

        /// <summary>
        /// Get an Industry by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /Industries/
        ///     {
        ///        "id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The id of the Industry.</param>
        /// <returns>On success - An Industry Model.
        /// If the Industry does not exists - Throws Argument Null Exception</returns>
        /// <response code="200">Returns an Industry Model</response>
        // GET: api/Industries/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var industry = await _industriesServices.GetIndustry(id);
            return Ok(industry);

        }

        /// <summary>
        /// Create a new Industry
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Industries
        ///     {
        ///        "id": 1,
        ///        "name": "Business",
        ///        "imgUrl": https://i.imgur.com/MWr58IA.png
        ///     }
        ///
        /// </remarks>
        /// <param name="name">The title of the new Industry. Between 5 and 50 characters</param>
        /// <param name="imgUrl">The URL for the industry's image, which appears on the industry's card.</param>
        /// <returns>On success - An Industry Model 
        /// If the Industry already exists - Throws Argument Exception</returns>
        /// <response code="201">Returns an Industry Model</response>
        // POST: api/Industries
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] string name, string imgUrl)
        {
            var industry = await _industriesServices.CreateIndustry(name, imgUrl);
            return Created("POST", name);

        }

        /// <summary>
        /// Update an existing Industry
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /Industries
        ///     {
        ///        "id": 1,
        ///        "name": "Business",
        ///        "imgUrl": https://i.imgur.com/MWr58IA.png
        ///     }
        ///
        /// </remarks>
        /// <param name="name">The new title of the Industry. Between 5 and 50 characters</param>
        /// <param name="imgUrl">The new URL for the industry's image, which appears on the industry's card.</param>
        /// <returns>On success - An updated Industry Model. 
        /// If the Industry already exists - Throws Argument Exception</returns>
        /// <response code="200">Returns an updated Industry Model.</response>
        // PUT: api/Industries/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] string name, string imgUrl)
        {
            var industry = await _industriesServices.UpdateIndustry(id, name, imgUrl);
            return Ok(industry);
        }

        /// <summary>
        /// Delete an Industry
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /Industries
        ///     {
        ///        "id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The id of the deleted industry</param>
        /// <returns>On success - Nothing if the Industry was deleted. 
        /// If the Industry does not exists - Throws Argument Null Exception</returns>
        /// <response code="204">Nothing if the Industry was deleted</response>
        /// <response code="400">If the Industry does not exists - Throws Argument Null Exception</response>            
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _industriesServices.DeleteIndustry(id))
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsightHub.Web.Controllers.APIControllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagsAPIController : ControllerBase
    {
        private readonly ITagServices _tagServices;

        public TagsAPIController(ITagServices tagServices)
        {
            this._tagServices = tagServices ?? throw new ArgumentNullException("TagServices can NOT be null.");
        }
        /// <summary>
        /// Get all Tags
        /// </summary>
        /// <param name="sort">A string to sort by.</param>
        /// <param name="search">A string to search for.</param>
        /// <returns>On success - All Tags(sorted or/and filtered). </returns>
        /// <response code="200">Returns All Tags(sorted or/and filtered).</response>
        /// <response code="404">When no tags were found.</response>
        // GET: api/Tags
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("")]
        public async Task<IActionResult> Get([FromQuery] string sort, [FromQuery] string search)
        {
            var tags = await _tagServices.GetTags(sort, search);
            if (sort != null)
            {
                switch (sort.ToLower())
                {
                    case "name":
                        tags = tags.OrderBy(t => t.Name).ToList();
                        break;
                    case "newest":
                        tags = tags.OrderByDescending(t => t.CreatedOn).ToList();
                        break;
                    case "oldest":
                        tags = tags.OrderBy(t => t.CreatedOn).ToList();
                        break;
                    default:
                        break;
                }
            }
            if (search != null)
            {
                tags = tags.Where(t => t.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            if (tags.Count == 0)
            {
                return NotFound(new { message = "No tags found." });
            }
            return Ok(tags);
        }

        /// <summary>
        /// Get a Tag by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /Tags/
        ///     {
        ///        "id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The id of the Tag.</param>
        /// <returns>On success - An Tag Model.
        /// If the Tag does not exists - Throws Argument Null Exception</returns>
        /// <response code="200">Returns an Tag Model</response>
        // GET: api/Tags/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _tagServices.GetTag(id);
            return Ok(model);
        }

        /// <summary>
        /// Create a new Tag
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Tag
        ///     {
        ///        "id": 1,
        ///        "name": "Business",
        ///     }
        ///
        /// </remarks>
        /// <param name="name">The title of the new Tag.</param>
        /// <returns>On success - An Tag Model <returns>
        /// <response code="200">Returns an Tag Model</response>
        // POST: api/Tags
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(string name)
        {
            var model = await _tagServices.CreateTag(name);
            return Ok(model);
        }

        /// <summary>
        /// Update an existing Tag
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /Tag/
        ///     {
        ///        "id": 1,
        ///        "name": "Business",
        ///     }
        ///
        /// </remarks>
        /// <param name="name">The new name of the Tag.</param>
        /// <returns>On success - An updated Tag Model. 
        /// If the Tag already exists - Throws Argument Exception</returns>
        /// <response code="200">Returns an updated Tag Model.</response>
        // PUT: api/Tags/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] string name)
        {
            var model = await _tagServices.UpdateTag(id, name);
            return Ok(model);
        }

        /// <summary>
        /// Delete an Tag
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /Tags/
        ///     {
        ///        "id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The id of the deleted tag</param>
        /// <returns>On success - Nothing if the Tag was deleted. 
        /// If the Tag does not exists - Throws Argument Null Exception</returns>
        /// <response code="204">Nothing if the Tag was deleted</response>
        /// <response code="400">If the Tag does not exists - Throws Argument Null Exception</response>            
        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tagServices.DeleteTag(id);
            return NoContent();
        }
    }
}

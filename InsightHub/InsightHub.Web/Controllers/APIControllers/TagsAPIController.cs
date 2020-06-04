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

        // GET: api/Tags
        [HttpGet]
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

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _tagServices.GetTag(id);
            return Ok(model);
        }

        // POST: api/Tags
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] string name)
        {

            var model = await _tagServices.CreateTag(name);
            return Ok(model);

        }

        // PUT: api/Tags/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] string name)
        {
            var model = await _tagServices.UpdateTag(id, name);
            return Ok(model);

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tagServices.DeleteTag(id);
            return NoContent();
        }
    }
}

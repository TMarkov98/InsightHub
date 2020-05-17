using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsightHub.Services.Contracts;
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
            try
            {
                var model = await _tagServices.GetTag(id);
                return Ok(model);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST: api/Tags
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string name)
        {
            try
            {
                var model = await _tagServices.CreateTag(name);
                return Ok(model);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Tags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string name)
        {
            try
            {
                var model = await _tagServices.UpdateTag(id, name);
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
            var model = await _tagServices.DeleteTag(id);
            if (model)
                return NoContent();
            return BadRequest();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsightHub.Web.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagServices _tagServices;

        public TagsController(ITagServices tagServices)
        {
            this._tagServices = tagServices ?? throw new ArgumentNullException("TagServices can NOT be null.");
        }

        // GET: api/Tags
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var model = await _tagServices.GetTags();
            return Ok(model);
        }

        // GET: api/Tags/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _tagServices.GetTag(id);
            return Ok(model);
        }

        // POST: api/Tags
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string name)
        {
            var model = await _tagServices.CreateTag(name);
            return Created("Post", model);
        }

        // PUT: api/Tags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string name)
        {
            var model = await _tagServices.UpdateTag(id, name);
            return Ok(model);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _tagServices.DeleteTag(id);
            if(model)
                return NoContent();
            return BadRequest();
        }
    }
}

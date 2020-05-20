using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using InsightHub.Services;
using InsightHub.Services.Contracts;
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
        // GET: api/Industries
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string sort, [FromQuery] string search)
        {
            var industries = await _industriesServices.GetAllIndustries(sort, search);
            if (industries.Count == 0)
            {
                return NotFound(new { message = "No industries found." });
            }
            return Ok(industries);
        }

        // GET: api/Industries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var industry = await _industriesServices.GetIndustry(id);
            return Ok(industry);

        }

        // POST: api/Industries
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string name, string imgUrl)
        {
            var industry = await _industriesServices.CreateIndustry(name, imgUrl);
            return Created("POST", name);

        }

        // PUT: api/Industries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] string name, string img)
        {
            var industry = await _industriesServices.UpdateIndustry(id, name, img);
            return Ok(industry);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
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

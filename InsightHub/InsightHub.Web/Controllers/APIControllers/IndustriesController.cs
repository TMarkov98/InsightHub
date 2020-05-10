using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsightHub.Services;
using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsightHub.Web.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustriesController : ControllerBase
    {
        private readonly IIndustryServices _industriesServices;
        public IndustriesController(IIndustryServices industryServices)
        {
            this._industriesServices = industryServices;
        }
        // GET: api/Industries
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var industries = await _industriesServices.GetAllIndustries();
            return Ok(industries);
        }

        // GET: api/Industries/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var industry = await _industriesServices.GetIndustry(id);
            return Ok(industry);
        }

        // POST: api/Industries
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string name)
        {
            var industry = await _industriesServices.CreateIndustry(name);
            return Created("POST", industry);
        }

        // PUT: api/Industries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] string name)
        {
            var industry = await _industriesServices.UpdateIndustry(id, name);
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

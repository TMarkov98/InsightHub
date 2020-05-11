using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsightHub.Services;
using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

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
            if (industries.Count == 0)
            {
                return Ok(new { message = "No industry found." });
            }
            return Ok(industries);
        }

        // GET: api/Industries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var industry = await _industriesServices.GetIndustry(id);
                return Ok(industry);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        // POST: api/Industries
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string name)
        {
            try
            {
                var industry = await _industriesServices.CreateIndustry(name);
                return Created("POST", name);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message});
            }

        }

        // PUT: api/Industries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] string name)
        {
            try
            {
                var industry = await _industriesServices.UpdateIndustry(id, name);
                return Ok(industry);
            }
            catch(ArgumentNullException ex)
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
            if (await _industriesServices.DeleteIndustry(id))
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}

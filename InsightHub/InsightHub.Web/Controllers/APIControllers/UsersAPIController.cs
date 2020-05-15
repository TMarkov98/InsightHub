using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsightHub.Models;
using InsightHub.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsightHub.Web.Controllers.APIControllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersAPIController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UsersAPIController(IUserServices userServices)
        {
            this._userServices = userServices ?? throw new ArgumentNullException("UserServices can NOT be null.");
        }
        // GET: api/UsersAPI
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var model = await _userServices.GetUsers();
            return Ok(model);
        }

        // GET: api/UsersAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _userServices.GetUser(id);
                return Ok(model);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/UsersAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserModel user)
        {
            try
            {
                var model = await _userServices.UpdateUser(id, user.FirstName, user.LastName, user.IsBanned, user.BanReason);
                return Ok(model);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

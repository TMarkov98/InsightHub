using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsightHub.Models;
using InsightHub.Services;
using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsightHub.Web.Controllers.APIControllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersAPIController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UsersAPIController(IUserServices userServices)
        {
            this._userServices = userServices ?? throw new ArgumentNullException("UserServices can NOT be null.");
        }
        /// <summary>
        /// Get all Users
        /// </summary>
        /// <param name="search">A string to search for.</param>
        /// <returns>On success - All Users(sorted or/and filtered). </returns>
        /// <response code="200">Returns All Users(sorted or/and filtered).</response>
        // GET: api/UsersAPI
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string search)
        {
            var model = await _userServices.GetUsers(search);
            return Ok(model);
        }

        /// <summary>
        /// Get an Users by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /Users/
        ///     {
        ///        "id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="id">The id of the User.</param>
        /// <returns>On success - An User Model.
        /// If the User does not exists - Throws Argument Null Exception</returns>
        /// <response code="200">Returns an User Model</response>
        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _userServices.GetUser(id);
            return Ok(model);
        }

        /// <summary>
        /// Update an existing Industry
        /// </summary>
        /// <param name="user">The User Model(Data Transfer Object)</param>
        /// <returns>On success - An updated User Model. 
        /// If the User already exists - Throws Argument Exception</returns>
        /// <response code="200">Returns an updated User Model.</response>
        // PUT: api/Users/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, [FromBody] UserModel user)
        {
            var model = await _userServices.UpdateUser(id, user.FirstName, user.LastName, user.IsBanned, user.BanReason);
            return Ok(model);
        }
    }
}

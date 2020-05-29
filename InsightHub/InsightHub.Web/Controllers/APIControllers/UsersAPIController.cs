﻿using System;
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
        // GET: api/UsersAPI
        [HttpGet]
        public async Task<IActionResult> Get(string search)
        {
            var model = await _userServices.GetUsers(search);
            return Ok(model);
        }

        // GET: api/UsersAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
                var model = await _userServices.GetUser(id);
                return Ok(model);
        }

        // PUT: api/UsersAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserModel user)
        {
                var model = await _userServices.UpdateUser(id, user.FirstName, user.LastName, user.IsBanned, user.BanReason);
                return Ok(model);
        }
    }
}

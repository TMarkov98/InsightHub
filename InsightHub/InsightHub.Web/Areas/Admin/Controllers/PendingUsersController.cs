using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InsightHub.Data;
using InsightHub.Data.Entities;
using InsightHub.Services.Contracts;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace InsightHub.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PendingUsersController : Controller
    {
        private readonly IUserServices _userServices;

        public PendingUsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        // GET: Admin/PendingUsers
        public async Task<IActionResult> Index(string search, int? pageNumber)
        {
            var users = await _userServices.GetPendingUsers(search);
            ViewData["Search"] = search;
            ViewData["ResultsCount"] = users.Count;
            if (search != null)
            {
                pageNumber = 1;
            }
            int pageSize = 10;
            return View(await users.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Approve(int? id)
        {
            {
                if (id == null)
                {
                    return NotFound();
                }
                await _userServices.ApproveUser(id.Value);

                return RedirectToAction(nameof(Index));
            }
        }
    }
}

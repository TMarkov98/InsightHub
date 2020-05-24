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

namespace InsightHub.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannedUsersController : Controller
    {
        private readonly IUserServices _userServices;

        public BannedUsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        // GET: Admin/BannedUsers
        public async Task<IActionResult> Index(string search, int? pageNumber)
        {
            var users = await _userServices.GetBannedUsers(search);
            if (search != null)
            {
                pageNumber = 1;
            }
            int pageSize = 10;
            return View(await users.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }
        // GET: Admin/BannedUsers/Unban/5
        public async Task<IActionResult> Unban(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userServices.GetUser(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admin/BannedUsers/Unban/5
        [HttpPost, ActionName("Unban")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnbanConfirmed(int id)
        {
            await _userServices.UnbanUser(id);
            return RedirectToAction("Index");
        }
    }
}

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
using InsightHub.Models;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace InsightHub.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index(string search, int? pageNumber)
        {
            var users = await _userServices.GetUsers(search);
            if(search != null)
            {
                pageNumber = 1;
            }
            int pageSize = 10;
            return View(await users.ToPagedListAsync(pageNumber ?? 1, pageSize));
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(int? id)
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
        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,IsPending,CreatedOn,ModifiedOn,IsBanned,BanReason,Id")] UserModel userModel)
        {
            if (id != userModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _userServices.UpdateUser(id, userModel.FirstName, userModel.LastName, userModel.IsBanned, userModel.BanReason);
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userServices.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Users/Ban/5
        public async Task<IActionResult> Ban(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userServices.GetUser(id.Value);
            if(user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Ban/5
        [HttpPost, ActionName("Ban")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BanConfirmed(int id, string banReason)
        {
            await _userServices.BanUser(id, banReason);
            return RedirectToAction("Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace InsightHub.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class MySubscriptions : Controller
    {
        private readonly IUserServices _userServices;

        public MySubscriptions(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [Authorize]
        // GET: MySubscriptions 
        public async Task<IActionResult> Index()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var subscriptions = await _userServices.GetSubscriptions(userId);
            return View(subscriptions);
        }
    }
}
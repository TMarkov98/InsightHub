using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsightHub.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class DashboardController : Controller
    {
        // GET: Dashboard 
        public IActionResult Index()
        {
            return View();
        }
    }
}
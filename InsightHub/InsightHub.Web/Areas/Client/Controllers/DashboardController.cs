using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsightHub.Web.Areas.Client.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard 
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
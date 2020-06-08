using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InsightHub.Web.Models;
using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Diagnostics;
using System.IO;

namespace InsightHub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Load Home Page
        /// </summary>
        /// <returns>On success - View</returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Load Privacy Page
        /// </summary>
        /// <returns>On success - View</returns>
        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// Load Error Page
        /// </summary>
        /// <returns>On success - View</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// Load NotFound Middleware Page
        /// </summary>
        /// <returns>On success - View</returns>
        public new IActionResult NotFound()
        {
            return View();
        }
        /// <summary>
        /// Load UserVanned Page
        /// </summary>
        /// <returns>On success - View</returns>
        public IActionResult UserBanned()
        {
            return View();
        }
        /// <summary>
        /// Load UserPending Page
        /// </summary>
        /// <returns>On success - View</returns>
        public IActionResult UserPending()
        {
            return View();
        }
        /// <summary>
        /// Load NotFound Middleware Page
        /// </summary>
        /// <returns>On success - View</returns>
        public IActionResult DevelopersApi()
        {
            return Redirect("/swagger");
        }
        public IActionResult ReportPending()
        {
            return View();
        }
    }
}

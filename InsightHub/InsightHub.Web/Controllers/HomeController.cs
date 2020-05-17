using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InsightHub.Web.Models;
using InsightHub.Services.Contracts;

namespace InsightHub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReportServices _reportServices;

        public HomeController(ILogger<HomeController> logger, IReportServices reportServices)
        {
            _logger = logger;
            this._reportServices = reportServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
        public async Task<IActionResult> GetTop5_MostDownloaded()
        {
            var reports = await _reportServices.GetTop5MostDownloads();
            return PartialView(reports);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

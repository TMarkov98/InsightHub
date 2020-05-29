using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightHub.Web.Models
{
    [ViewComponent(Name = "Top5MostDownloadedViewComponent")]
    public class Top5MostDownloadedViewComponent : ViewComponent
    {

        private readonly IReportServices _reportServices;

        public Top5MostDownloadedViewComponent(IReportServices reportServices)
        {
            this._reportServices = reportServices;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var reports = await _reportServices.GetTop5MostDownloads();
            return View(reports);
        }
    }
}

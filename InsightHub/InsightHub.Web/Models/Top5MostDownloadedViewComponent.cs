using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightHub.Web.Models
{
    [ViewComponent(Name = "MostDownloadedViewComponent")]
    public class MostDownloadedViewComponent : ViewComponent
    {

        private readonly IReportServices _reportServices;

        public MostDownloadedViewComponent(IReportServices reportServices)
        {
            this._reportServices = reportServices;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var reports = await _reportServices.GetMostDownloadedReports();
            return View(reports);
        }
    }
}

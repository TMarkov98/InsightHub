using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightHub.Web.Views.Home
{
    [ViewComponent(Name = "Top5NewestViewComponent")]
    public class Top5NewestViewComponent : ViewComponent
    {

        private readonly IReportServices _reportServices;

        public Top5NewestViewComponent(IReportServices reportServices)
        {
            this._reportServices = reportServices;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var reports = await _reportServices.GetTop5NewReports();
            return View(reports);
        }
    }
}

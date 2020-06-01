using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightHub.Web.Models
{
    [ViewComponent(Name = "FeaturedViewComponent")]
    public class FeaturedViewComponent : ViewComponent
    {

        private readonly IReportServices _reportServices;

        public FeaturedViewComponent(IReportServices reportServices)
        {
            this._reportServices = reportServices;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var reports = await _reportServices.GetFeaturedReports();
            return View(reports);
        }
    }
}

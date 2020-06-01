using InsightHub.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightHub.Web.Views.Home
{
    [ViewComponent(Name = "NewestViewComponent")]
    public class NewestViewComponent : ViewComponent
    {

        private readonly IReportServices _reportServices;

        public NewestViewComponent(IReportServices reportServices)
        {
            this._reportServices = reportServices;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var reports = await _reportServices.GetNewestReports();
            return View(reports);
        }
    }
}

using InsightHub.Data;
using InsightHub.Data.Entities;
using InsightHub.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.UserServicesTests
{
    [TestClass]
    public class GetDownloadedReports_Should
    {
        [TestMethod]
        public async Task ReturnAllDownloadedReports()
        {
            var options = Utils.GetOptions(nameof(ReturnAllDownloadedReports));
            var user1 = TestModelsSeeder.SeedUser();
            var report1 = TestModelsSeeder.SeedReport();
            var report2 = TestModelsSeeder.SeedReport2();

            var tag1 = TestModelsSeeder.SeedTag();
            var tag2 = TestModelsSeeder.SeedTag2();

            var industry1 = TestModelsSeeder.SeedIndustry();
            var industry2 = TestModelsSeeder.SeedIndustry2();

            user1.IsPending = false;
            report1.IsPending = false;
            report2.IsPending = false;

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user1);
                arrangeContext.Reports.Add(report1);
                arrangeContext.Reports.Add(report2);
                arrangeContext.Tags.Add(tag1);
                arrangeContext.Tags.Add(tag2);
                arrangeContext.Industries.Add(industry1);
                arrangeContext.Industries.Add(industry2);

                arrangeContext.DownloadedReports.Add(new DownloadedReport
                {
                    UserId = 1,
                    ReportId = 1,
                });
                arrangeContext.DownloadedReports.Add(new DownloadedReport
                {
                    UserId = 1,
                    ReportId = 2,
                });

                await arrangeContext.SaveChangesAsync();
                
            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sut = new UserServices(assertContext);
                var result = await sut.GetDownloadedReports(user1.Id);
                Assert.AreEqual(2, result.Count);
            }
        }
    }
}

using InsightHub.Data;
using InsightHub.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.ReportServicesTests
{
    [TestClass]
    public class GetReportsFeatured_Should
    {
        [TestMethod]
        public async Task GetsFeaturedReports_When_ParamsValid()
        {
            var options = Utils.GetOptions(nameof(GetsFeaturedReports_When_ParamsValid));
            var report1 = TestModelsSeeder.SeedReport();
            var industry1 = TestModelsSeeder.SeedIndustry();
            var user = TestModelsSeeder.SeedUser();
            var tag = TestModelsSeeder.SeedTag();
            var tag2 = TestModelsSeeder.SeedTag2();
            var tag3 = TestModelsSeeder.SeedTag3();
            var report2 = TestModelsSeeder.SeedReport2();
            var industry2 = TestModelsSeeder.SeedIndustry2();
            var report3 = TestModelsSeeder.SeedReport3();
            var industry3 = TestModelsSeeder.SeedIndustry3();
            report1.IsFeatured = true;
            report2.IsFeatured = true;

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Reports.AddAsync(report1);
                await arrangeContext.Tags.AddAsync(tag);
                await arrangeContext.Industries.AddAsync(industry1);
                await arrangeContext.Reports.AddAsync(report2);
                await arrangeContext.Tags.AddAsync(tag2);
                await arrangeContext.Industries.AddAsync(industry2);
                await arrangeContext.Reports.AddAsync(report3);
                await arrangeContext.Tags.AddAsync(tag3);
                await arrangeContext.Industries.AddAsync(industry3);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sutTags = new TagServices(assertContext);
                var sut = new ReportServices(assertContext, sutTags);
                var act = await sut.GetFeaturedReports();
                var result = act.ToArray();
                Assert.AreEqual(2, result.Length);
                Assert.AreEqual(report1.Title, result[0].Title);
                Assert.AreEqual(report2.Title, result[1].Title);
            }
        }
    }
}

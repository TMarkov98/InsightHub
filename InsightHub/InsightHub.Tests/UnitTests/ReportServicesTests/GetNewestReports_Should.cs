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
    public class GetNewestReports_Should
    {
        [TestMethod]
        public async Task GetsTop5NewReports_When_ParamsValid()
        {
            var options = Utils.GetOptions(nameof(GetsTop5NewReports_When_ParamsValid));
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

            using var assertContext = new InsightHubContext(options);
            var sutTags = new TagServices(assertContext);
            var sut = new ReportServices(assertContext, sutTags);
            var act = await sut.GetNewestReports();
            var result = act.ToArray();
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(report3.Title, result[0].Title);
            Assert.AreEqual(report3.Summary, result[0].Summary);
            Assert.IsTrue(result[0].CreatedOn > result[1].CreatedOn);
            Assert.AreEqual(report2.Title, result[1].Title);
            Assert.AreEqual(report2.Summary, result[1].Summary);
            Assert.IsTrue(result[1].CreatedOn > result[2].CreatedOn);
            Assert.AreEqual(report1.Title, result[2].Title);
            Assert.AreEqual(report1.Summary, result[2].Summary);
        }
    }
}

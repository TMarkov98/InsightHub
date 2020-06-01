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
    public class GetReportsDeleted_Should
    {
        [TestMethod]
        public async Task GetsDeletedReports_When_ParamsValid()
        {
            var options = Utils.GetOptions(nameof(GetsDeletedReports_When_ParamsValid));
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
            report1.IsDeleted = true;
            report2.IsDeleted = true;

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
                var act = await sut.GetDeletedReports(null, null);
                var result = act.ToArray();
                Assert.AreEqual(2, result.Length);
                Assert.AreEqual(report1.Title, result[0].Title);
                Assert.AreEqual(report1.Summary, result[0].Summary);
                Assert.AreEqual(report2.Title, result[1].Title);
                Assert.AreEqual(report2.Summary, result[1].Summary);
            }
        }
        [TestMethod]
        public async Task ReturnDeletedReports_WithSort_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnDeletedReports_WithSort_When_ParamValid));
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
            report1.IsDeleted = true;
            report2.IsDeleted = true;

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
                var act = await sut.GetDeletedReports("title_desc", null);
                var result = act.ToArray();
                Assert.AreEqual(2, result.Length);
                Assert.AreEqual(report2.Title, result[0].Title);
                Assert.AreEqual(report2.Summary, result[0].Summary);
                Assert.AreEqual(report1.Title, result[1].Title);
                Assert.AreEqual(report1.Summary, result[1].Summary);
            }
        }
        [TestMethod]
        public async Task ReturnDeletedReports_WithSearch_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnDeletedReports_WithSearch_When_ParamValid));
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
            report1.IsDeleted = true;
            report2.IsDeleted = true;

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
                var act = await sut.GetDeletedReports(null, "TestReport");
                var result = act.ToArray();
                Assert.AreEqual(2, result.Length);
                Assert.AreEqual(report1.Title, result[0].Title);
                Assert.AreEqual(report1.Summary, result[0].Summary);
                Assert.IsTrue(result[0].Title.Contains("TestReport"));
                Assert.AreEqual(report2.Title, result[1].Title);
                Assert.AreEqual(report2.Summary, result[1].Summary);
                Assert.IsTrue(result[1].Title.Contains("TestReport"));
            }
        }

        [TestMethod]
        public async Task ReturnDeletedReport_WithSortAndSearch_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnDeletedReport_WithSortAndSearch_When_ParamValid));
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
            report1.IsDeleted = true;
            report2.IsDeleted = true;
            report3.IsDeleted = true;


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
                var act = await sut.GetDeletedReports("title_desc", "TestReport");
                var result = act.ToList();
                Assert.AreEqual(3, result.Count());
                Assert.AreEqual(report3.Title, result[0].Title);
                Assert.AreEqual(report3.Summary, result[0].Summary);
                Assert.IsTrue(result[0].Title.Contains("TestReport"));
                Assert.AreEqual(report2.Title, result[1].Title);
                Assert.AreEqual(report2.Summary, result[1].Summary);
                Assert.IsTrue(result[1].Title.Contains("TestReport"));
                Assert.AreEqual(report1.Title, result[2].Title);
                Assert.AreEqual(report1.Summary, result[2].Summary);
                Assert.IsTrue(result[2].Title.Contains("TestReport"));
            }
        }
    }
}

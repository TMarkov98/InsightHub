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
    public class GetReports_Should
    {
        [TestMethod]
        public async Task GetsCorrectReports_When_ParamsValid()
        {
            var options = Utils.GetOptions(nameof(GetsCorrectReports_When_ParamsValid));
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
                var result = await sut.GetReports(null, null, null, null, null);
                Assert.AreEqual(2, result.Count);
            }
        }
        [TestMethod]
        public async Task ReturnNotDeletedReports_WithSort_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnNotDeletedReports_WithSort_When_ParamValid));
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
                var act = await sut.GetReports("title_desc", null, null, null, null);
                var result = act.ToList();
                Assert.AreEqual(2, result.Count());
                Assert.AreEqual(report3.Title, result[0].Title);
                Assert.AreEqual(report2.Title, result[1].Title);
            }
        }
        [TestMethod]
        public async Task ReturnNotDeletedReports_WithSearch_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnNotDeletedReports_WithSearch_When_ParamValid));
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
                var act = await sut.GetReports(null, "2", null, null, null);
                var result = act.ToList();
                Assert.AreEqual(1, result.Count());
                Assert.AreEqual(report2.Title, result[0].Title);
            }
        }
        [TestMethod]
        public async Task ReturnNotDeletedReport_WithSortAndSearch_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnNotDeletedReport_WithSortAndSearch_When_ParamValid));
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

            using (var assertContext = new InsightHubContext(options))
            {
                var sutTags = new TagServices(assertContext);
                var sut = new ReportServices(assertContext, sutTags);
                var act = await sut.GetReports("title_desc", "TestReport", null, null, null);
                var result = act.ToList();
                Assert.AreEqual(3, result.Count());
                Assert.AreEqual(report3.Title, result[0].Title);
                Assert.AreEqual(report2.Title, result[1].Title);
                Assert.AreEqual(report1.Title, result[2].Title);
            }
        }
        [TestMethod]
        public async Task ReturnNotDeletedReport_WithSortAndSearchAndAuthor_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnNotDeletedReport_WithSortAndSearchAndAuthor_When_ParamValid));
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

            using (var assertContext = new InsightHubContext(options))
            {
                var sutTags = new TagServices(assertContext);
                var sut = new ReportServices(assertContext, sutTags);
                var act = await sut.GetReports("title_desc", "TestReport", "firstTest@user.com", null, null);
                var result = act.ToList();
                Assert.AreEqual(3, result.Count());
                Assert.AreEqual(report3.Title, result[0].Title);
                Assert.IsTrue(result[0].Author.Contains("firstTest@user.com"));
                Assert.AreEqual(report2.Title, result[1].Title);
                Assert.IsTrue(result[1].Author.Contains("firstTest@user.com"));
                Assert.AreEqual(report1.Title, result[2].Title);
                Assert.IsTrue(result[2].Author.Contains("firstTest@user.com"));
            }
        }
        [TestMethod]
        public async Task ReturnNotDeletedReport_WithSortAndSearchAndIndustry_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnNotDeletedReport_WithSortAndSearchAndIndustry_When_ParamValid));
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

            using (var assertContext = new InsightHubContext(options))
            {
                var sutTags = new TagServices(assertContext);
                var sut = new ReportServices(assertContext, sutTags);
                var act = await sut.GetReports("title_desc", "TestReport", null, "Industry", null);
                var result = act.ToList();
                Assert.AreEqual(3, result.Count());
                Assert.AreEqual(report3.Title, result[0].Title);
                Assert.IsTrue(result[0].Industry.Contains("Industry"));
                Assert.AreEqual(report2.Title, result[1].Title);
                Assert.IsTrue(result[1].Industry.Contains("Industry"));
                Assert.AreEqual(report1.Title, result[2].Title);
                Assert.IsTrue(result[2].Industry.Contains("Industry"));
            }
        }
        [TestMethod]
        public async Task ReturnNotDeletedReport_WithTag_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnNotDeletedReport_WithTag_When_ParamValid));
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

            using (var assertContext = new InsightHubContext(options))
            {
                var sutTags = new TagServices(assertContext);
                var sut = new ReportServices(assertContext, sutTags);
                var act = await sut.GetReports(null, null, null, null, "TestTag1");
                var result = act.ToList();
                Assert.AreEqual(1, result.Count());
                Assert.AreEqual(report1.Title, result[0].Title);
                Assert.IsTrue(result[0].Tags.Contains("TestTag1"));
            }
        }

    }
}

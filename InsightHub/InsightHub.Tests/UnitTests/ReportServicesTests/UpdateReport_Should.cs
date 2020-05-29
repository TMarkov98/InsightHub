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
    public class UpdateReport_Should
    {
        [TestMethod]
        public async Task UpdateReport_WhenParamsAreValid()
        {
            var options = Utils.GetOptions(nameof(UpdateReport_WhenParamsAreValid));
            var newTitle = "New Report Title";
            var newSummary = "New Report Summary";
            var newDescription = "New Report Description";
            var newIndustry = TestModelsSeeder.SeedIndustry2();
            var newImgURL = "imageurl";
            var newTag = TestModelsSeeder.SeedTag2();

            var report1 = TestModelsSeeder.SeedReport();
            var industry1 = TestModelsSeeder.SeedIndustry();
            var user = TestModelsSeeder.SeedUser();
            var tag = TestModelsSeeder.SeedTag();

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.Industries.AddAsync(industry1);
                await arrangeContext.Industries.AddAsync(newIndustry);
                await arrangeContext.Tags.AddAsync(tag);
                await arrangeContext.Tags.AddAsync(newTag);
                await arrangeContext.Reports.AddAsync(report1);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sutTags = new TagServices(assertContext);
                var sutReports = new ReportServices(assertContext, sutTags);
                var act = await sutReports.UpdateReport(1, newTitle, newSummary, newDescription, newImgURL, newIndustry.Name, newTag.Name);
                var result = assertContext.Reports.FirstOrDefault(t => t.Title == newTitle);
                Assert.AreEqual(newTitle, result.Title);
                Assert.AreEqual(newSummary, result.Summary);
                Assert.AreEqual(newDescription, result.Description);
                Assert.AreEqual(newIndustry.Name, result.Industry.Name);
                Assert.AreEqual(newImgURL, result.ImgUrl);
                Assert.AreEqual(newTag.Name, result.Tags.First().Tag.Name);
            }
        }

        [TestMethod]
        public async Task ThrowArgumentException_WhenTitleAlreadyExists()
        {
            var options = Utils.GetOptions(nameof(ThrowArgumentException_WhenTitleAlreadyExists));
            var newSummary = "New Report Summary";
            var newDescription = "New Report Description";
            var newIndustry = TestModelsSeeder.SeedIndustry2();
            var newImgURL = "imageurl";
            var newTag = TestModelsSeeder.SeedTag2();

            var report1 = TestModelsSeeder.SeedReport();
            var industry1 = TestModelsSeeder.SeedIndustry();
            var user = TestModelsSeeder.SeedUser();
            var tag = TestModelsSeeder.SeedTag();

            var report2 = TestModelsSeeder.SeedReport2();
            var industry2 = TestModelsSeeder.SeedIndustry3();
            var tag2 = TestModelsSeeder.SeedTag3();

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.Industries.AddAsync(industry1);
                await arrangeContext.Industries.AddAsync(industry2);
                await arrangeContext.Industries.AddAsync(newIndustry);
                await arrangeContext.Tags.AddAsync(tag);
                await arrangeContext.Tags.AddAsync(newTag);
                await arrangeContext.Tags.AddAsync(tag2);
                await arrangeContext.Reports.AddAsync(report1);
                await arrangeContext.Reports.AddAsync(report2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sutTags = new TagServices(assertContext);
                var sutReports = new ReportServices(assertContext, sutTags);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sutReports.UpdateReport(1, report2.Title, newSummary, newDescription, newImgURL, newIndustry.Name, newTag.Name));
            }

        }
    }
}

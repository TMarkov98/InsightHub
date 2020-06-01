using InsightHub.Data;
using InsightHub.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.ReportServicesTests
{
    [TestClass]
    public class CreateReport_Should
    {
        [TestMethod]
        public async Task CreateReport_WhenParamsAreValid()
        {
            var options = Utils.GetOptions(nameof(CreateReport_WhenParamsAreValid));
            var title = "Report Title";
            var summary = "Report Summary";
            var description = "Report Description";
            var author = TestModelsSeeder.SeedUser();
            var industry = TestModelsSeeder.SeedIndustry();
            var imgURL = "imageurl";
            var tag = TestModelsSeeder.SeedTag();

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Users.AddAsync(author);
                await arrangeContext.Industries.AddAsync(industry);
                await arrangeContext.Tags.AddAsync(tag);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sutTags = new TagServices(assertContext);
                var sutReports = new ReportServices(assertContext, sutTags);
                var act = await sutReports.CreateReport(title, summary, description, author.Email, imgURL, industry.Name, tag.Name);
                var result = assertContext.Reports.FirstOrDefault(t => t.Title == title);
                Assert.AreEqual(title, result.Title);
                Assert.AreEqual(description, result.Description);
                Assert.AreEqual(tag.Name, result.ReportTags.First().Tag.Name);
            }
        }

        [TestMethod]
        public async Task ThrowArgumentException_When_TitleAlreadyExists()
        {
            var options = Utils.GetOptions(nameof(ThrowArgumentException_When_TitleAlreadyExists));

            var summary = "Report Summary";
            var description = "Report Description";
            var author = TestModelsSeeder.SeedUser();
            var industry = TestModelsSeeder.SeedIndustry();
            var imgURL = "imageurl";
            var tag = TestModelsSeeder.SeedTag();
            var report = TestModelsSeeder.SeedReport();

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Users.AddAsync(author);
                await arrangeContext.Industries.AddAsync(industry);
                await arrangeContext.Tags.AddAsync(tag);
                await arrangeContext.Reports.AddAsync(report);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sutTags = new TagServices(assertContext);
                var sutReports = new ReportServices(assertContext, sutTags);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sutReports.CreateReport(report.Title, summary, description, author.Email, imgURL, industry.Name, tag.Name));
            }

        }
    }
}

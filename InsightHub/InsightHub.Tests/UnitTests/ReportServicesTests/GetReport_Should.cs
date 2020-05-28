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
    public class GetReport_Should
    {
        [TestMethod]
        public async Task GetsCorrectReport_When_ParamsValid()
        {
            var options = Utils.GetOptions(nameof(GetsCorrectReport_When_ParamsValid));
            var report1 = TestModelsSeeder.SeedReport();
            var industry1 = TestModelsSeeder.SeedIndustry();
            var user = TestModelsSeeder.SeedUser();
            var tag = TestModelsSeeder.SeedTag();

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Reports.AddAsync(report1);
                await arrangeContext.Tags.AddAsync(tag);
                await arrangeContext.Industries.AddAsync(industry1);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sutTags = new TagServices(assertContext);
                var sut = new ReportServices(assertContext, sutTags);
                var result = await sut.GetReport(1);
                Assert.AreEqual(report1.Title, result.Title);
                Assert.AreEqual(report1.Summary, result.Summary);
                Assert.AreEqual(report1.Description, result.Description);
                Assert.AreEqual(report1.Author.Email, result.Author.Split(" ").Last());
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Throw_When_IdIsInvalid()
        {
            var options = Utils.GetOptions(nameof(Throw_When_IdIsInvalid));

            using (var assertContext = new InsightHubContext(options))
            {
                var sutTag = new TagServices(assertContext);
                var sut = new ReportServices(assertContext, sutTag);
                await sut.GetReport(5);
            }
        }
    }
}

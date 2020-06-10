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
    public class ApproveReport_Should
    {
        [TestMethod]
        public async Task ApproveReport_When_ParamsValid()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ApproveReport_When_ParamsValid));
            var report1 = TestModelsSeeder.SeedReport();
            var industry1 = TestModelsSeeder.SeedIndustry();
            var user = TestModelsSeeder.SeedUser();
            var tag = TestModelsSeeder.SeedTag();
            report1.IsPending = true;

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Reports.AddAsync(report1);
                await arrangeContext.Tags.AddAsync(tag);
                await arrangeContext.Industries.AddAsync(industry1);
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.SaveChangesAsync();
            }
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sutTag = new TagServices(assertContext);
            var sut = new ReportServices(assertContext, sutTag);
            await sut.ApproveReport(1);
            Assert.IsFalse(assertContext.Reports.First(u => u.Id == 1).IsPending);
        }

        [TestMethod]
        public async Task ThrowArgumentException_WhenReportNotExists()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ThrowArgumentException_WhenReportNotExists));
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
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sutTag = new TagServices(assertContext);
            var sut = new ReportServices(assertContext, sutTag);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.ApproveReport(5));
        }
    }
}

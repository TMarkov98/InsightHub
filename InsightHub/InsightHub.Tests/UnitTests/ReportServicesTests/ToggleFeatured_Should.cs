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
    public class ToggleFeatured_Should
    {
        [TestMethod]
        public async Task ToggleReport_ToFeatured_When_NotFeatured()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ToggleReport_ToFeatured_When_NotFeatured));
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
            Assert.IsFalse(assertContext.Reports.First(u => u.Id == 1).IsFeatured);
            await sut.ToggleFeatured(1);
            Assert.IsTrue(assertContext.Reports.First(u => u.Id == 1).IsFeatured);
        }

        [TestMethod]
        public async Task ToggleReport_NotFeatured_When_IsFeatured()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ToggleReport_NotFeatured_When_IsFeatured));
            var report1 = TestModelsSeeder.SeedReport();
            var industry1 = TestModelsSeeder.SeedIndustry();
            var user = TestModelsSeeder.SeedUser();
            var tag = TestModelsSeeder.SeedTag();
            report1.IsFeatured = true;
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
            Assert.IsTrue(assertContext.Reports.First(u => u.Id == 1).IsFeatured);
            await sut.ToggleFeatured(1);
            Assert.IsFalse(assertContext.Reports.First(u => u.Id == 1).IsFeatured);
        }


        [TestMethod]
        public async Task ThrowArgumentException_When_FeaturedReportNotExists()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ThrowArgumentException_When_FeaturedReportNotExists));
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
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.ToggleFeatured(5));
        }
    }
}

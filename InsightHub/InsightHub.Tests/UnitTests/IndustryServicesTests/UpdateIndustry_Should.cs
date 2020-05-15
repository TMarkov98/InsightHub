using InsightHub.Data;
using InsightHub.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.IndustryServicesTests
{
    [TestClass]
    public class UpdateIndustry_Should
    {
        [TestMethod]
        public async Task UpdateIndustryCorrectly_WhenParamsAreValid()
        {
            var options = Utils.GetOptions(nameof(UpdateIndustryCorrectly_WhenParamsAreValid));
            var industry = TestModelsSeeder.SeedIndustry();

            using(var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.AddAsync(industry);
                await arrangeContext.SaveChangesAsync();
            }

            using(var assertContext = new InsightHubContext(options))
            {
                var sut = new IndustryServices(assertContext);
                var act = await sut.UpdateIndustry(1, "NewName");
                var result = await sut.GetIndustry(1);
                Assert.AreEqual(industry.Id, result.Id);
                Assert.AreEqual(result.Name, "NewName");
            }
        }
        [TestMethod]
        public async Task Throw_WhenIdIsInvalid()
        {
            var options = Utils.GetOptions(nameof(Throw_WhenIdIsInvalid));
            var industry = TestModelsSeeder.SeedIndustry();

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.AddAsync(industry);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sut = new IndustryServices(assertContext);
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.UpdateIndustry(5, "NewName"));
            }
        }
        [TestMethod]
        public async Task Throw_WhenIndustryWithNameAlreadyExists()
        {
            var options = Utils.GetOptions(nameof(Throw_WhenIndustryWithNameAlreadyExists));
            var industry = TestModelsSeeder.SeedIndustry();
            var industry2 = TestModelsSeeder.SeedIndustry2();

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.AddAsync(industry);
                await arrangeContext.AddAsync(industry2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sut = new IndustryServices(assertContext);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.UpdateIndustry(1, industry2.Name));
            }
        }
    }
}

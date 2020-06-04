using InsightHub.Data;
using InsightHub.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.IndustryServicesTests
{
    [TestClass]
    public class DeleteIndustry_Should
    {
        [TestMethod]
        public async Task SetDeletedFlag_WhenParamsAreValid()
        {
            {
                var options = Utils.GetOptions(nameof(SetDeletedFlag_WhenParamsAreValid));
                var industry = TestModelsSeeder.SeedIndustry();

                using (var arrangeContext = new InsightHubContext(options))
                {
                    await arrangeContext.Industries.AddAsync(industry);
                    await arrangeContext.SaveChangesAsync();
                }

                using (var assertContext = new InsightHubContext(options))
                {
                    var sut = new IndustryServices(assertContext);
                    await sut.DeleteIndustry(1);
                    var result = await assertContext.Industries.FirstOrDefaultAsync(i => i.Name == industry.Name);
                    Assert.IsTrue(result.IsDeleted);
                }
            }
        }

        [TestMethod]
        public async Task Throw_WhenIdIsInvalid()
        {
            var options = Utils.GetOptions(nameof(Throw_WhenIdIsInvalid));
            using (var assertContext = new InsightHubContext(options))
            {
                var sut = new IndustryServices(assertContext);
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.DeleteIndustry(5));
            }

        }
        [TestMethod]
        public async Task ReturnFalse_WhenIndustryAlreadyDeleted()
        {
            var options = Utils.GetOptions(nameof(ReturnFalse_WhenIndustryAlreadyDeleted));
            var industry = TestModelsSeeder.SeedIndustry();
            industry.IsDeleted = true;

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Industries.AddAsync(industry);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sut = new IndustryServices(assertContext);
                await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.DeleteIndustry(industry.Id));
            }
        }
    }
}

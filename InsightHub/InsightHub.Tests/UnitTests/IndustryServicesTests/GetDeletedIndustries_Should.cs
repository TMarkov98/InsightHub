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
    public class GetDeletedIndustries_Should
    {
        [TestMethod]
        public async Task ReturnDeletedIndustries()
        {
            var options = Utils.GetOptions(nameof(ReturnDeletedIndustries));
            var industry = TestModelsSeeder.SeedIndustry();
            var industry2 = TestModelsSeeder.SeedIndustry2();
            industry.IsDeleted = true;

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Industries.AddAsync(industry);
                await arrangeContext.Industries.AddAsync(industry2);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sut = new IndustryServices(assertContext);
                var result = await sut.GetDeletedIndustries(null);
                Assert.AreEqual(1, result.Count);
            }
        }
    }
}

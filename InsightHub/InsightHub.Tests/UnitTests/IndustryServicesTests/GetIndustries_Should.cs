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
    public class GetIndustries_Should
    {
        [TestMethod]
        public async Task ReturnNotDeletedIndustries()
        {
            var options = Utils.GetOptions(nameof(ReturnNotDeletedIndustries));
            var industry = TestModelsSeeder.SeedIndustry();
            var industry2 = TestModelsSeeder.SeedIndustry2();
            var industry3 = TestModelsSeeder.SeedIndustry3();
            industry.IsDeleted = true;

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Industries.AddAsync(industry);
                await arrangeContext.Industries.AddAsync(industry2);
                await arrangeContext.Industries.AddAsync(industry3);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sut = new IndustryServices(assertContext);
                var result = await sut.GetAllIndustries();
                Assert.AreEqual(2, result.Count);
            }
        }
    }
}

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
    public class GetIndustry_Should
    {
        [TestMethod]
        public async Task ReturnCorrectIndustry_WhenParamsAreValid()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ReturnCorrectIndustry_WhenParamsAreValid));
            var industry = TestModelsSeeder.SeedIndustry();

            using(var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.AddAsync(industry);
                await arrangeContext.SaveChangesAsync();
            }
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sut = new IndustryServices(assertContext);
            var result = await sut.GetIndustry(1);
            Assert.AreEqual(industry.Id, result.Id);
            Assert.AreEqual(industry.Name, result.Name);
        }
        [TestMethod]
        public async Task Throw_WhenIdIsInvalid()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(Throw_WhenIdIsInvalid));
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sut = new IndustryServices(assertContext);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.GetIndustry(5));
        }
    }
}

using InsightHub.Data;
using InsightHub.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.IndustryServicesTests
{
    [TestClass]
    public class GetAllIndustries_Should
    {
        [TestMethod]
        public async Task ReturnNotDeletedIndustries()
        {
            //Arrange
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
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sut = new IndustryServices(assertContext);
            var result = await sut.GetAllIndustries(null, null);
            Assert.AreEqual(2, result.Count);
        }
        [TestMethod]
        public async Task ReturnNotDeletedIndustries_WithSort_When_ParamValid()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ReturnNotDeletedIndustries_WithSort_When_ParamValid));
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
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sut = new IndustryServices(assertContext);
            var result = (await sut.GetAllIndustries("name_desc", null)).ToArray();
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(industry3.Name, result[0].Name);
            Assert.AreEqual(industry2.Name, result[1].Name);
        }
        [TestMethod]
        public async Task ReturnNotDeletedIndustries_WithSearch_When_ParamValid()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ReturnNotDeletedIndustries_WithSearch_When_ParamValid));
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
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sut = new IndustryServices(assertContext);
            var result = (await sut.GetAllIndustries(null, "3")).ToArray();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(industry3.Name, result[0].Name);
        }
        [TestMethod]
        public async Task ReturnNotDeletedIndustries_WithSortAndSearch_When_ParamValid()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ReturnNotDeletedIndustries_WithSortAndSearch_When_ParamValid));
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
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sut = new IndustryServices(assertContext);
            var result = (await sut.GetAllIndustries("name_desc", "Test")).ToArray();
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(industry3.Name, result[0].Name);
            Assert.AreEqual(industry2.Name, result[1].Name);
        }
    }
}

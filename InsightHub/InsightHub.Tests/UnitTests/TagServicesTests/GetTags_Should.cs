using InsightHub.Data;
using InsightHub.Services;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.TagServicesTests
{
    [TestClass]
    public class GetTags_Should
    {
        [TestMethod]
        public async Task ReturnCorrectTags_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnCorrectTags_When_ParamValid));
            var firstTag = TestModelsSeeder.SeedTag();
            var secondTag = TestModelsSeeder.SeedTag2();
            var thirdTag = TestModelsSeeder.SeedTag3();

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Tags.Add(firstTag);
                arrangeContext.Tags.Add(secondTag);
                arrangeContext.Tags.Add(thirdTag);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            var act = await sut.GetTags(null, null);
            var result = act.ToArray();
            Assert.AreEqual(firstTag.Name, result[0].Name);
        }
        [TestMethod]
        public async Task ReturnCorrectTags_WithSort_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnCorrectTags_WithSort_When_ParamValid));
            var firstTag = TestModelsSeeder.SeedTag();
            var secondTag = TestModelsSeeder.SeedTag2();
            var thirdTag = TestModelsSeeder.SeedTag3();

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Tags.Add(firstTag);
                arrangeContext.Tags.Add(secondTag);
                arrangeContext.Tags.Add(thirdTag);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            var act = await sut.GetTags("name_desc", null);
            var result = act.ToArray();
            Assert.AreEqual(thirdTag.Name, result[0].Name);
            Assert.AreEqual(secondTag.Name, result[1].Name);
            Assert.AreEqual(firstTag.Name, result[2].Name);
        }
        [TestMethod]
        public async Task ReturnCorrectTags_WithSearch_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnCorrectTags_WithSearch_When_ParamValid));
            var firstTag = TestModelsSeeder.SeedTag();
            var secondTag = TestModelsSeeder.SeedTag2();
            var thirdTag = TestModelsSeeder.SeedTag3();

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Tags.Add(firstTag);
                arrangeContext.Tags.Add(secondTag);
                arrangeContext.Tags.Add(thirdTag);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            var act = await sut.GetTags(null, "3");
            var result = act.ToArray();
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(thirdTag.Name, result[0].Name);
        }
        [TestMethod]
        public async Task ReturnCorrectTags_WithSortAndSearch_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnCorrectTags_WithSortAndSearch_When_ParamValid));
            var firstTag = TestModelsSeeder.SeedTag();
            var secondTag = TestModelsSeeder.SeedTag2();
            var thirdTag = TestModelsSeeder.SeedTag3();

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Tags.Add(firstTag);
                arrangeContext.Tags.Add(secondTag);
                arrangeContext.Tags.Add(thirdTag);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            var act = await sut.GetTags("name_desc", "Test");
            var result = act.ToArray();
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(thirdTag.Name, result[0].Name);
        }
    }
}

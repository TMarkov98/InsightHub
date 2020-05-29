using InsightHub.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using InsightHub.Services;
using System.Threading.Tasks;
using InsightHub.Tests.UnitTests;

namespace InsightHub.Tests.UnitTests.TagServicesTests
{
    [TestClass]
    public class CreateTag_Should
    {
        [TestMethod]
        public async Task ReturnCorrectNewTag_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnCorrectNewTag_When_ParamValid));
            var tagName = "space";

            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            var act = await sut.CreateTag(tagName);
            var result = assertContext.Tags.FirstOrDefault(t => t.Name == tagName);
            Assert.AreEqual(tagName, result.Name);
        }

        [TestMethod]
        public async Task ThrowArgumentException_When_AlreadyExists()
        {
            var options = Utils.GetOptions(nameof(ThrowArgumentException_When_AlreadyExists));
            var tag = TestModelsSeeder.SeedTag();
            var tagName = "TestTag1";

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Tags.Add(tag);
                arrangeContext.SaveChanges();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateTag(tagName));
        }
    }
}
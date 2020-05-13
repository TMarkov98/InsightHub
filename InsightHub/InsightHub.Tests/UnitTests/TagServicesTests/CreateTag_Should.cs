using InsightHub.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using InsightHub.Services;
using System.Threading.Tasks;

namespace InsightHub.Tests.TagServicesTests
{
    [TestClass]
    public class CreateTag_Should
    {
        [TestMethod]
        public async Task ReturnCurrectTag_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnCurrectTag_When_ParamValid));
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
            var options = Utils.GetOptions(nameof(ReturnCurrectTag_When_ParamValid));
            var tagName = "space";

            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            var act = sut.CreateTag(tagName);
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateTag(tagName));
        }
    }
}
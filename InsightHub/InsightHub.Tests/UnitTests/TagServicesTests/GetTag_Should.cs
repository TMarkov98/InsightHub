using InsightHub.Data;
using InsightHub.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.TagServicesTests
{
    [TestClass]
    public class GetTag_Should
    {
        [TestMethod]
        public async Task ReturnCorrectTag_When_ParamValid()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ReturnCorrectTag_When_ParamValid));
            var tag = TestModelsSeeder.SeedTag();

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Tags.Add(tag);
                await arrangeContext.SaveChangesAsync();
            }
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            var act = await sut.GetTag(1);
            var result = act.Name;
            Assert.AreEqual(tag.Name, result);
        }
        [TestMethod]
        public async Task ThrowArgumentNullException_When_NotExists()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ThrowArgumentNullException_When_NotExists));
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetTag(1));
        }
    }
}

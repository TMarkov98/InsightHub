using InsightHub.Data;
using InsightHub.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.TagServicesTests
{
    [TestClass]
    public class DeleteTag_Should
    {
        [TestMethod]
        public async Task DeleteTag_When_ParamsValid()
        {
            var options = Utils.GetOptions(nameof(DeleteTag_When_ParamsValid));
            var tag = TestModelsSeeder.SeedTag();

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Tags.Add(tag);
                await arrangeContext.SaveChangesAsync();
            }
            
            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            await sut.DeleteTag(1);
            Assert.IsTrue(assertContext.Tags.FirstOrDefault().IsDeleted);
        }
        [TestMethod]
        public async Task Throw_When_ParamsNotValid()
        {
            var options = Utils.GetOptions(nameof(Throw_When_ParamsNotValid));
            
            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.DeleteTag(5));
        }

        [TestMethod]
        public async Task Throw_WhenTagAlreadyDeleted()
        {
            var options = Utils.GetOptions(nameof(Throw_WhenTagAlreadyDeleted));
            var tag = TestModelsSeeder.SeedTag();
            tag.IsDeleted = true;

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Tags.Add(tag);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.DeleteTag(tag.Id));
        }
    }
}
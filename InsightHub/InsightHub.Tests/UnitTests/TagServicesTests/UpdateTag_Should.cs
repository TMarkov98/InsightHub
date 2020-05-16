using InsightHub.Data;
using InsightHub.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.TagServicesTests
{
    [TestClass]
    public class UpdateTag_Should
    {
        [TestMethod]
        public async Task ReturnCorrectUpdatedTag_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnCorrectUpdatedTag_When_ParamValid));
            var tag = TestModelsSeeder.SeedTag();
            var prevModDate = tag.ModifiedOn;
            string newName = "Space";

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Tags.Add(tag);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            var act = await sut.UpdateTag(1, newName);
            var result = act.Name;
            Assert.AreEqual(newName, result);
            Assert.AreNotEqual(prevModDate, act.ModifiedOn);
        }
        [TestMethod]
        public async Task ThrowArgumentNullException_When_NotExists()
        {
            var options = Utils.GetOptions(nameof(ThrowArgumentNullException_When_NotExists));

            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetTag(1));
        }
        [TestMethod]
        public async Task ThrowArgumentException_When_ParamNotValid()
        {
            var options = Utils.GetOptions(nameof(ThrowArgumentException_When_ParamNotValid));
            var tagToUpdate = TestModelsSeeder.SeedTag();
            var tagOther = TestModelsSeeder.SeedTag2();

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Tags.Add(tagToUpdate);
                arrangeContext.Tags.Add(tagOther);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.UpdateTag(1, tagOther.Name));
        }
    }
}

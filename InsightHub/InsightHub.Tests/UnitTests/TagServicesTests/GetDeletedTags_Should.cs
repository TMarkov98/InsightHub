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
    public class GetDeletedTags_Should
    {
        [TestMethod]
        public async Task ReturnCurrectDeletedTags_When_ParamsValid()
        {
            var options = Utils.GetOptions(nameof(ReturnCurrectDeletedTags_When_ParamsValid));
            var firstTag = TestModelsSeeder.SeedTag(); 
            var secondTag = TestModelsSeeder.SeedTag2();

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Tags.Add(firstTag);
                arrangeContext.Tags.Add(secondTag);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sut = new TagServices(assertContext);
                await sut.DeleteTag(1);
                await sut.DeleteTag(2);
                var act = await sut.GetDeletedTags();
                var result = act.ToArray();
                Assert.AreEqual(2, result.Length);
                Assert.AreEqual(firstTag.Name, result[0].Name);
                Assert.AreEqual(secondTag.Name, result[1].Name);
            }
        }
    }
}

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
        public async Task ReturnCurrectTags_When_ParamValid()
        {
            var options = Utils.GetOptions(nameof(ReturnCurrectTags_When_ParamValid));
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
    }
}

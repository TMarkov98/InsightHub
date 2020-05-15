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
    public class DeleteTag_Should
    {
        [TestMethod]
        public async Task ReturnTrue_When_ParamsValid()
        {
            var options = Utils.GetOptions(nameof(ReturnTrue_When_ParamsValid));
            var tag = TestModelsSeeder.SeedTag();

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Tags.Add(tag);
                await arrangeContext.SaveChangesAsync();
            }
            
            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            var result = await sut.DeleteTag(1);
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public async Task ReturnFalse_When_ParamsNotValid()
        {
            var options = Utils.GetOptions(nameof(ReturnFalse_When_ParamsNotValid));
            
            using var assertContext = new InsightHubContext(options);
            var sut = new TagServices(assertContext);
            var result = await sut.DeleteTag(1);
            Assert.AreEqual(false, result);
        }
    }
}
//var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

//            if (tag == null || tag.IsDeleted)
//            {
//                return false;
//            }

//            tag.IsDeleted = true;
//            tag.DeletedOn = DateTime.UtcNow;
//            await _context.SaveChangesAsync();

//            return true;
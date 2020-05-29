using InsightHub.Data;
using InsightHub.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.IndustryServicesTests
{
    [TestClass]
    public class AddSubscription_Should
    {
        [TestMethod]
        public async Task AddSubscription_WhenParamsAreValid()
        {
            {
                var options = Utils.GetOptions(nameof(AddSubscription_WhenParamsAreValid));
                var industry = TestModelsSeeder.SeedIndustry();
                var user = TestModelsSeeder.SeedUser();

                using (var arrangeContext = new InsightHubContext(options))
                {
                    await arrangeContext.Industries.AddAsync(industry);
                    await arrangeContext.Users.AddAsync(user);
                    await arrangeContext.SaveChangesAsync();
                }

                using (var assertContext = new InsightHubContext(options))
                {
                    var sut = new IndustryServices(assertContext);
                    
                    await sut.AddSubscription(user.Id, industry.Id);
                    var result = await assertContext.IndustrySubscriptions.AnyAsync(ui => ui.UserId == user.Id && ui.IndustryId == industry.Id);
                    Assert.IsTrue(result);
                }
            }
        }
        [TestMethod]
        public async Task DoNotThrowArgumentException_When_AlreadyExists()
        {
            {
                var options = Utils.GetOptions(nameof(DoNotThrowArgumentException_When_AlreadyExists));
                var industry = TestModelsSeeder.SeedIndustry();
                var user = TestModelsSeeder.SeedUser();

                using (var arrangeContext = new InsightHubContext(options))
                {
                    await arrangeContext.Industries.AddAsync(industry);
                    await arrangeContext.Users.AddAsync(user);
                    await arrangeContext.SaveChangesAsync();
                }

                using (var assertContext = new InsightHubContext(options))
                {
                    var sut = new IndustryServices(assertContext);

                    await sut.AddSubscription(user.Id, industry.Id);
                    await sut.AddSubscription(user.Id, industry.Id);
                    var result = await assertContext.IndustrySubscriptions.AnyAsync(ui => ui.UserId == user.Id && ui.IndustryId == industry.Id);
                    Assert.IsTrue(result);
                    Assert.AreEqual(1, assertContext.IndustrySubscriptions.Count());
                }
            }
        }
    }
}

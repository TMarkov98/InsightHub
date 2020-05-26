using InsightHub.Data;
using InsightHub.Data.Entities;
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
    public class RemoveSubscription_Should
    {
        [TestMethod]
        public async Task RemoveSubscription_WhenParamsAreValid()
        {
            {
                var options = Utils.GetOptions(nameof(RemoveSubscription_WhenParamsAreValid));
                var industry = TestModelsSeeder.SeedIndustry();
                var user = TestModelsSeeder.SeedUser();

                using (var arrangeContext = new InsightHubContext(options))
                {
                    await arrangeContext.Industries.AddAsync(industry);
                    await arrangeContext.Users.AddAsync(user);
                    await arrangeContext.IndustrySubscriptions.AddAsync(
                        new IndustrySubscription
                        {
                            UserId = user.Id,
                            IndustryId = industry.Id
                        });
                    await arrangeContext.SaveChangesAsync();
                }

                using (var assertContext = new InsightHubContext(options))
                {
                    var sut = new IndustryServices(assertContext);

                    var act = await assertContext.IndustrySubscriptions.AnyAsync(ui => ui.UserId == user.Id && ui.IndustryId == industry.Id);
                    Assert.IsTrue(act);
                    await sut.RemoveSubscription(user.Id, industry.Id);
                    var result = await assertContext.IndustrySubscriptions.AnyAsync(ui => ui.UserId == user.Id && ui.IndustryId == industry.Id);
                    Assert.IsFalse(result);
                }
            }
        }
        [TestMethod]
        public async Task DoNotThrowArgumentException_When_AlreadyRemoved()
        {
            {
                var options = Utils.GetOptions(nameof(DoNotThrowArgumentException_When_AlreadyRemoved));
                var industry = TestModelsSeeder.SeedIndustry();
                var user = TestModelsSeeder.SeedUser();

                using (var arrangeContext = new InsightHubContext(options))
                {
                    await arrangeContext.Industries.AddAsync(industry);
                    await arrangeContext.Users.AddAsync(user);
                    await arrangeContext.IndustrySubscriptions.AddAsync(
                        new IndustrySubscription
                        {
                            UserId = user.Id,
                            IndustryId = industry.Id
                        });
                    await arrangeContext.SaveChangesAsync();
                }

                using (var assertContext = new InsightHubContext(options))
                {
                    var sut = new IndustryServices(assertContext);

                    var act = await assertContext.IndustrySubscriptions.AnyAsync(ui => ui.UserId == user.Id && ui.IndustryId == industry.Id);
                    Assert.IsTrue(act);

                    await sut.RemoveSubscription(user.Id, industry.Id);
                    await sut.RemoveSubscription(user.Id, industry.Id);
                    var result = await assertContext.IndustrySubscriptions.AnyAsync(ui => ui.UserId == user.Id && ui.IndustryId == industry.Id);
                    Assert.IsFalse(result);

                }
            }
        }
    }
}

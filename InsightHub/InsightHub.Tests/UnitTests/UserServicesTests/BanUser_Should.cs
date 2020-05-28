using InsightHub.Data;
using InsightHub.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.UserServicesTests
{
    [TestClass]
    public class BanUser_Should
    {
        [TestMethod]
        public async Task CorrectlyBanUser_WhenParamsValid()
        {
            var options = Utils.GetOptions(nameof(CorrectlyBanUser_WhenParamsValid));
            var user = TestModelsSeeder.SeedUser();

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            await sut.BanUser(user.Id, "Test");
            Assert.IsTrue(assertContext.Users.FirstOrDefault().IsBanned);
            Assert.AreEqual("Test", assertContext.Users.FirstOrDefault().BanReason);
        }

        [TestMethod]
        public async Task Throw_WhenUserDoesntExist()
        {
            var options = Utils.GetOptions(nameof(Throw_WhenUserDoesntExist));

            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.BanUser(5, "Test"));
        }

        [TestMethod]
        public async Task Throw_WhenUserAlreadyBanned()
        {
            var options = Utils.GetOptions(nameof(Throw_WhenUserAlreadyBanned));
            var user = TestModelsSeeder.SeedUser();
            user.IsBanned = true;

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.BanUser(user.Id, "Test"));
        }
    }
}
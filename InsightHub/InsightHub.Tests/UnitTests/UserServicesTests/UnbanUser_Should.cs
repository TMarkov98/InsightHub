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
    public class UnbanUser_Should
    {
        [TestMethod]
        public async Task CorrectlyUnbanUser_WhenParamsValid()
        {
            var options = Utils.GetOptions(nameof(CorrectlyUnbanUser_WhenParamsValid));
            var user = TestModelsSeeder.SeedUser();
            user.IsBanned = true;

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            await sut.UnbanUser(user.Id);
            Assert.IsFalse(assertContext.Users.FirstOrDefault().IsBanned);
            Assert.AreEqual(string.Empty, assertContext.Users.FirstOrDefault().BanReason);
        }

        [TestMethod]
        public async Task Throw_WhenUserDoesntExist()
        {
            var options = Utils.GetOptions(nameof(Throw_WhenUserDoesntExist));

            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.UnbanUser(5));
        }

        [TestMethod]
        public async Task Throw_When_UserNotBanned()
        {
            var options = Utils.GetOptions(nameof(Throw_When_UserNotBanned));
            var user = TestModelsSeeder.SeedUser();
            user.IsBanned = false;

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await sut.UnbanUser(user.Id));
        }
    }
}
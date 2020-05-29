using InsightHub.Data;
using InsightHub.Services;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.UserServicesTests
{
    [TestClass]
    public class GetBannedUsers_Should
    {
        [TestMethod]
        public async Task ReturnOnlyBannedUsers()
        {
            var options = Utils.GetOptions(nameof(ReturnOnlyBannedUsers));
            var user1 = TestModelsSeeder.SeedUser();
            var user2 = TestModelsSeeder.SeedUser2();
            var user3 = TestModelsSeeder.SeedUser3();

            user1.IsBanned = true;
            user2.IsBanned = true;
            user3.IsBanned = false;

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user1);
                arrangeContext.Users.Add(user2);
                arrangeContext.Users.Add(user3);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            var act = await sut.GetBannedUsers(null);
            var result = act.ToList();
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(user1.Email, result[0].Email);
            Assert.AreEqual(user2.Email, result[1].Email);
        }

        [TestMethod]
        public async Task ReturnCorrectUsers_WhenSearching()
        {
            var options = Utils.GetOptions(nameof(ReturnCorrectUsers_WhenSearching));
            var user1 = TestModelsSeeder.SeedUser();
            var user2 = TestModelsSeeder.SeedUser2();

            user1.IsBanned = true;
            user2.IsBanned = true;

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user1);
                arrangeContext.Users.Add(user2);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            var act = await sut.GetBannedUsers("first");
            var result = act.ToList();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(user1.Email, result[0].Email);
        }
    }
}

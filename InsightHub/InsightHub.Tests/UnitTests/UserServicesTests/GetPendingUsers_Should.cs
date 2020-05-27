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
    public class GetPendingUsers_Should
    {
        [TestMethod]
        public async Task ReturnOnlyPendingUsers()
        {
            var options = Utils.GetOptions(nameof(ReturnOnlyPendingUsers));
            var user1 = TestModelsSeeder.SeedUser();
            var user2 = TestModelsSeeder.SeedUser2();
            var user3 = TestModelsSeeder.SeedUser3();

            user1.IsPending = true;
            user2.IsPending = true;
            user3.IsPending = false;

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user1);
                arrangeContext.Users.Add(user2);
                arrangeContext.Users.Add(user3);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            var act = await sut.GetPendingUsers(null);
            var result = act.ToList();
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(user1.Email, result[0].Email);
            Assert.AreEqual(user2.Email, result[1].Email);
        }

        [TestMethod]
        public async Task ReturnCorrectPendingUsers_WhenSearching()
        {
            var options = Utils.GetOptions(nameof(ReturnCorrectPendingUsers_WhenSearching));
            var user1 = TestModelsSeeder.SeedUser();
            var user2 = TestModelsSeeder.SeedUser2();

            user1.IsPending = true;
            user2.IsPending = true;

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user1);
                arrangeContext.Users.Add(user2);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            var act = await sut.GetPendingUsers("first");
            var result = act.ToList();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(user1.Email, result[0].Email);
        }
    }
}

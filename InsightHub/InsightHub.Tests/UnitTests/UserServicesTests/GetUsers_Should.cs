using InsightHub.Data;
using InsightHub.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.UserServicesTests
{
    [TestClass]
    public class GetUsers_Should
    {
        [TestMethod]
        public async Task ReturnActiveUsers()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ReturnActiveUsers));
            var user1 = TestModelsSeeder.SeedUser();
            var user2 = TestModelsSeeder.SeedUser2();
            var user3 = TestModelsSeeder.SeedUser3();

            user1.IsPending = false;
            user1.IsBanned = false;
            user2.IsPending = false;
            user2.IsBanned = true;
            user3.IsPending = true;
            user3.IsBanned = false;

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user1);
                arrangeContext.Users.Add(user2);
                arrangeContext.Users.Add(user3);
                await arrangeContext.SaveChangesAsync();
            }
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            var result = (await sut.GetUsers(null)).ToList();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(user1.Email, result[0].Email);
        }

        [TestMethod]
        public async Task ReturnUsers_WhenSearching()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ReturnUsers_WhenSearching));
            var user1 = TestModelsSeeder.SeedUser();
            var user2 = TestModelsSeeder.SeedUser2();

            user1.IsBanned = false;
            user1.IsPending = false;
            user2.IsBanned = false;
            user2.IsPending = false;

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user1);
                arrangeContext.Users.Add(user2);
                await arrangeContext.SaveChangesAsync();
            }
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            var result = (await sut.GetUsers("first")).ToList();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(user1.Email, result[0].Email);
        }

        [TestMethod]
        public async Task ReturnOnlyActiveUsers_WhenSearching()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ReturnOnlyActiveUsers_WhenSearching));
            var user1 = TestModelsSeeder.SeedUser();
            var user2 = TestModelsSeeder.SeedUser();
            user2.Id = 2;

            user1.IsBanned = false;
            user1.IsPending = false;
            user2.IsBanned = true;
            user2.IsPending = false;

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user1);
                arrangeContext.Users.Add(user2);
                await arrangeContext.SaveChangesAsync();
            }
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            var result = (await sut.GetUsers("first")).ToList();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(user1.Email, result[0].Email);
        }
    }
}

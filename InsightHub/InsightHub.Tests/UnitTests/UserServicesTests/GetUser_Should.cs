using InsightHub.Data;
using InsightHub.Data.Entities;
using InsightHub.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.UserServicesTests
{
    [TestClass]
    public class GetUser_Should
    {
        [TestMethod]
        public async Task ReturnCorrectUser_WhenParamsValid()
        {
            var options = Utils.GetOptions(nameof(ReturnCorrectUser_WhenParamsValid));
            var user = TestModelsSeeder.SeedUser();

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sut = new UserServices(assertContext);
                var result = await sut.GetUser(user.Id);
                Assert.AreEqual(user.Id, result.Id);
                Assert.AreEqual(user.IsBanned, result.IsBanned);
                Assert.AreEqual(user.IsPending, result.IsPending);
                Assert.AreEqual(user.Email, result.Email);
            }
        }
        [TestMethod]
        public async Task Throw_WhenUserDoesntExist()
        {
            var options = Utils.GetOptions(nameof(Throw_WhenUserDoesntExist));

            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.GetUser(5));
        }
    }
}

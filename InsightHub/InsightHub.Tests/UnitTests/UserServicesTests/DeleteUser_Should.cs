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
    public class DeleteUser_Should
    {
        [TestMethod]
        public async Task RemoveUser_WhenParamsValid()
        {
            var options = Utils.GetOptions(nameof(RemoveUser_WhenParamsValid));
            var user = TestModelsSeeder.SeedUser();

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            await sut.DeleteUser(user.Id);
            Assert.AreEqual(0, assertContext.Users.Count());
        }
        [TestMethod]
        public async Task Throw_WhenUserDoesntExist()
        {
            var options = Utils.GetOptions(nameof(Throw_WhenUserDoesntExist));

            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.DeleteUser(5));
        }
    }
}
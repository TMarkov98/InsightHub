using InsightHub.Data;
using InsightHub.Services;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.TagServicesTests
{
    [TestClass]
    public class UpdateUser_Should
    {
        [TestMethod]
        public async Task CorrectlyUpdateData_WhenParamsValid()
        {
            var options = Utils.GetOptions(nameof(CorrectlyUpdateData_WhenParamsValid));
            var user = TestModelsSeeder.SeedUser();
            var originalModifiedDate = user.ModifiedOn;

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user);
                await arrangeContext.SaveChangesAsync();
            }

            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            var act = await sut.UpdateUser(1, "Test First Name", "Test Last Name", true, "Test Ban Reason");
            var result = assertContext.Users.FirstOrDefault();
            Assert.AreEqual("Test First Name", result.FirstName);
            Assert.AreEqual("Test Last Name", result.FirstName);
            Assert.IsTrue(result.IsBanned);
            Assert.AreEqual("Test Ban Reason", result.BanReason);
            Assert.AreNotEqual(originalModifiedDate, act.ModifiedOn);
        }
        [TestMethod]
        public async Task Throw_WhenUserDoesntExist()
        {
            var options = Utils.GetOptions(nameof(Throw_WhenUserDoesntExist));

            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.UpdateUser(5, "f", "l", false, ""));
        }
    }
}

using InsightHub.Data;
using InsightHub.Data.Entities;
using InsightHub.Services;
using Microsoft.EntityFrameworkCore;
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
    public class GetUploadedReports_Should
    {
        [TestMethod]
        public async Task ReturnAllUploadedReports()
        {
            var options = Utils.GetOptions(nameof(ReturnAllUploadedReports));
            var user1 = TestModelsSeeder.SeedUser();
            var report1 = TestModelsSeeder.SeedReport();
            var report2 = TestModelsSeeder.SeedReport2();
            report1.AuthorId = user1.Id;
            report2.AuthorId = user1.Id;

            var tag1 = TestModelsSeeder.SeedTag();
            var tag2 = TestModelsSeeder.SeedTag2();

            var industry1 = TestModelsSeeder.SeedIndustry();
            var industry2 = TestModelsSeeder.SeedIndustry2();

            user1.IsPending = false;
            report1.IsPending = false;
            report2.IsPending = false;

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user1);
                arrangeContext.Reports.Add(report1);
                arrangeContext.Reports.Add(report2);
                arrangeContext.Tags.Add(tag1);
                arrangeContext.Tags.Add(tag2);
                arrangeContext.Industries.Add(industry1);
                arrangeContext.Industries.Add(industry2);

                await arrangeContext.SaveChangesAsync();

            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sut = new UserServices(assertContext);
                var result = await sut.GetUploadedReports(user1.Id, null);
                Assert.AreEqual(2, result.Count);
            }
        }

        [TestMethod]
        public async Task Throw_WhenUserDoesntExist()
        {
            var options = Utils.GetOptions(nameof(Throw_WhenUserDoesntExist));

            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.GetUploadedReports(5, null));
        }
    }
}

using Azure.Storage.Blobs.Models;
using InsightHub.Data;
using InsightHub.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.ReportServicesTests
{
    [TestClass]
    public class AddToDownloadsCount_Should
    {
        [TestMethod]
        public async Task AddToDownloadsCount_When_ParamsValid()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(AddToDownloadsCount_When_ParamsValid));
            var user = TestModelsSeeder.SeedUser();
            var report = TestModelsSeeder.SeedReport();

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.Reports.AddAsync(report);
                arrangeContext.SaveChanges();
            }
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sutTags = new TagServices(assertContext);
            var sut = new ReportServices(assertContext, sutTags);
            await sut.AddToDownloadsCount(user.Id, report.Id);
            var count = assertContext.DownloadedReports.Count();
            Assert.AreEqual(1, count);
        }
        [TestMethod]
        public async Task DoNot_AddToDownloadsCount_When_AlreadyDownloaded()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(DoNot_AddToDownloadsCount_When_AlreadyDownloaded));
            var user = TestModelsSeeder.SeedUser();
            var report = TestModelsSeeder.SeedReport();

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.Reports.AddAsync(report);
                arrangeContext.SaveChanges();
            }
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sutTags = new TagServices(assertContext);
            var sut = new ReportServices(assertContext, sutTags);
            await sut.AddToDownloadsCount(user.Id, report.Id);
            await sut.AddToDownloadsCount(user.Id, report.Id);
            var count = assertContext.DownloadedReports.Count();
            Assert.AreEqual(1, count);
        }
    }
}
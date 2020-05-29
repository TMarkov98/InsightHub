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
    public class AddToDownloadsCount
    {
        [TestMethod]
        public async Task AddToDownloadsCount_When_ParamsValid()
        {
            var options = Utils.GetOptions(nameof(AddToDownloadsCount_When_ParamsValid));
            var user = TestModelsSeeder.SeedUser();
            var report = TestModelsSeeder.SeedReport();

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.Reports.AddAsync(report);
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new InsightHubContext(options))
            {
                var sutTags = new TagServices(assertContext);
                var sut = new ReportServices(assertContext, sutTags);
                await sut.AddToDownloadsCount(user.Id, report.Id);
                var count = assertContext.DownloadedReports.Count();
                Assert.AreEqual(1, count);
            }
        }
        [TestMethod]
        public async Task DoNot_AddToDownloadsCount_When_AlreadyDownloaded()
        {
            var options = Utils.GetOptions(nameof(DoNot_AddToDownloadsCount_When_AlreadyDownloaded));
            var user = TestModelsSeeder.SeedUser();
            var report = TestModelsSeeder.SeedReport();

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Users.AddAsync(user);
                await arrangeContext.Reports.AddAsync(report);
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new InsightHubContext(options))
            {
                var sutTags = new TagServices(assertContext);
                var sut = new ReportServices(assertContext, sutTags);
                await sut.AddToDownloadsCount(user.Id, report.Id);
                await sut.AddToDownloadsCount(user.Id, report.Id);
                var count = assertContext.DownloadedReports.Count();
                Assert.AreEqual(1, count);
            }
        }
    }
}
//public async Task AddToDownloadsCount(int userId, int reportId)
//{
//    if (!await _context.DownloadedReports.AnyAsync(ur => ur.UserId == userId && ur.ReportId == reportId))
//    {
//        await _context.DownloadedReports.AddAsync(new DownloadedReport
//        {
//            UserId = userId,
//            ReportId = reportId
//        });
//        await _context.SaveChangesAsync();
//    }
//}

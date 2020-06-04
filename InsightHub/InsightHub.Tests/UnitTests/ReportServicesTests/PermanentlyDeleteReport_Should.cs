using InsightHub.Data;
using InsightHub.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.ReportServicesTests
{
    [TestClass]
    public class PermanentlyDeleteReport
    {
        [TestMethod]
        public async Task PermanentlyDelete_When_ParamsValid()
        {
            var options = Utils.GetOptions(nameof(PermanentlyDelete_When_ParamsValid));
            var report = TestModelsSeeder.SeedReport();

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Reports.AddAsync(report);
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sutTag = new TagServices(assertContext);
                var sut = new ReportServices(assertContext, sutTag);
                assertContext.Reports.First(r => r.Id == 1).IsDeleted = true;
                await sut.PermanentlyDeleteReport(report.Id);
                Assert.AreEqual(0, assertContext.Reports.Count());
            }
        }

        [TestMethod]
        public async Task ThrowArgumentException_When_ReportIsNotFound()
        {
            var options = Utils.GetOptions(nameof(ThrowArgumentException_When_ReportIsNotFound));
            var report = TestModelsSeeder.SeedReport();

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Reports.AddAsync(report);
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new InsightHubContext(options))
            {
                var sutTag = new TagServices(assertContext);
                var sut = new ReportServices(assertContext, sutTag);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.RestoreReport(1));
            }
        }
        
    }
}

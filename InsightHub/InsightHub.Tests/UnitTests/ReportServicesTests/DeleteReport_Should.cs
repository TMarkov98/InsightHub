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
    public class DeleteReport_Should
    {
        [TestMethod]
        public async Task DeleteReport_When_ParamsValid()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(DeleteReport_When_ParamsValid));
            var report = TestModelsSeeder.SeedReport();

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Reports.AddAsync(report);
                arrangeContext.SaveChanges();
            }
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sutTag = new TagServices(assertContext);
            var sut = new ReportServices(assertContext, sutTag);
            await sut.DeleteReport(1);
            Assert.IsTrue(assertContext.Reports.First(u => u.Id == 1).IsDeleted);
        }

        [TestMethod]
        public async Task ThrowArgumentException_When_ReportNotExists()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ThrowArgumentException_When_ReportNotExists));
            var report = TestModelsSeeder.SeedReport();

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Reports.AddAsync(report);
                arrangeContext.SaveChanges();
            }
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sutTag = new TagServices(assertContext);
            var sut = new ReportServices(assertContext, sutTag);
            await sut.DeleteReport(1);
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.DeleteReport(1));
        }
    }
}

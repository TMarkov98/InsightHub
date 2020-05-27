using InsightHub.Data;
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
    public class GetDownloadedReports_Should
    {
        //[TestMethod]
        //public async Task ReturnCorrectData_WhenParamsAreValid()
        //{
        //    var options = Utils.GetOptions(nameof(ReturnCorrectData_WhenParamsAreValid));
        //    var user1 = TestModelsSeeder.SeedUser();

        //    var report1 = TestModelsSeeder.SeedReport();
        //    var report2 = TestModelsSeeder.SeedReport2();
            
        //    using (var arrangeContext = new InsightHubContext(options))
        //    {
        //        arrangeContext.Reports.Add(report1);
        //        arrangeContext.Reports.Add(report2);
        //        arrangeContext.Users.Add(user1); 
        //        user1.Reports.Add(TestModelsSeeder.SeedDownloadedReport());
        //        user1.Reports.Add(TestModelsSeeder.SeedDownloadedReport2());
        //        await arrangeContext.SaveChangesAsync();
        //    }

        //    using var assertContext = new InsightHubContext(options);
        //    {
        //        var sut = new UserServices(assertContext);
        //        var act = await sut.GetDownloadedReports(user1.Id);
        //        Assert.AreEqual(2, act.Count);
        //    }
        //}
    }
}

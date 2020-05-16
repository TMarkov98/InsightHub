﻿using InsightHub.Data;
using InsightHub.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightHub.Tests.UnitTests.IndustryServicesTests
{
    [TestClass]
    public class CreateIndustry_Should
    {
        [TestMethod]
        public async Task CreateIndustry_WhenParamsAreValid()
        {
            {
                var options = Utils.GetOptions(nameof(CreateIndustry_WhenParamsAreValid));
                var industryName = "Test Industry";

                using (var assertContext = new InsightHubContext(options))
                {
                    var sut = new IndustryServices(assertContext);
                    var act = await sut.CreateIndustry(industryName);
                    var result = assertContext.Industries.FirstOrDefault(t => t.Name == industryName);
                    Assert.AreEqual(industryName, result.Name);
                }
            }
        }
        [TestMethod]
        public async Task Throw_When_IndustryAlreadyExists()
        {
            var options = Utils.GetOptions(nameof(Throw_When_IndustryAlreadyExists));
            var industry = TestModelsSeeder.SeedIndustry();
            var industryName = industry.Name;

            using (var arrangeContext = new InsightHubContext(options))
            {
                await arrangeContext.Industries.AddAsync(industry);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new InsightHubContext(options)) 
            {
                var sut = new IndustryServices(assertContext);
                var act = sut.CreateIndustry(industryName);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.CreateIndustry(industryName));
            }
            
        }
    }
}

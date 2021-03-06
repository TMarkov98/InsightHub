﻿using InsightHub.Data;
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
    public class GetSubscriptions_Should
    {
        [TestMethod]
        public async Task ReturnAllUserSubscriptions()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ReturnAllUserSubscriptions));
            var user1 = TestModelsSeeder.SeedUser();

            var industry1 = TestModelsSeeder.SeedIndustry();
            var industry2 = TestModelsSeeder.SeedIndustry2();

            user1.IsPending = false;

            using (var arrangeContext = new InsightHubContext(options))
            {
                arrangeContext.Users.Add(user1);
                arrangeContext.Industries.Add(industry1);
                arrangeContext.Industries.Add(industry2);

                arrangeContext.IndustrySubscriptions.Add(new IndustrySubscription
                {
                    UserId = 1,
                    IndustryId = 1
                });

                arrangeContext.IndustrySubscriptions.Add(new IndustrySubscription
                {
                    UserId = 1,
                    IndustryId = 2
                });

                await arrangeContext.SaveChangesAsync();

            }
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            var result = await sut.GetSubscriptions(user1.Id);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task Throw_WhenUserDoesntExist()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(Throw_WhenUserDoesntExist));
            //Act & Assert
            using var assertContext = new InsightHubContext(options);
            var sut = new UserServices(assertContext);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await sut.GetSubscriptions(5));
        }
    }
}

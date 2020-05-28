using InsightHub.Data.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Tests.UnitTests
{
    public static class TestModelsSeeder
    {
        //Seed Tags
        public static Tag SeedTag()
        {
            return new Tag
            {
                Id = 1,
                CreatedOn = DateTime.UtcNow,
                Name = "TestTag1",
            };
        }
        public static Tag SeedTag2()
        {
            return new Tag
            {
                Id = 2,
                CreatedOn = DateTime.UtcNow,
                Name = "TestTag2",
            };
        }
        public static Tag SeedTag3()
        {
            return new Tag
            {
                Id = 3,
                CreatedOn = DateTime.UtcNow,
                Name = "TestTag3",
            };
        }
        //Seed Industries
        public static Industry SeedIndustry()
        {
            return new Industry
            {
                Id = 1,
                CreatedOn = DateTime.UtcNow,
                Name = "TestIndustry1",
            };
        }
        public static Industry SeedIndustry2()
        {
            return new Industry
            {
                Id = 2,
                CreatedOn = DateTime.UtcNow,
                Name = "TestIndustry2",
            };
        }
        public static Industry SeedIndustry3()
        {
            return new Industry
            {
                Id = 3,
                CreatedOn = DateTime.UtcNow,
                Name = "TestIndustry3",
            };
        }
        //Seed Reports
        public static Report SeedReport()
        {
            return new Report
            {
                Id = 1,
                AuthorId = 1,
                CreatedOn = DateTime.UtcNow,
                Title = "TestReport1",
                Summary = "TestSummary1",
                Description = "TestDescription1",
                IndustryId = 1,
            };
        }
        public static Report SeedReport2()
        {
            return new Report
            {
                Id = 2,
                AuthorId = 1,
                CreatedOn = DateTime.UtcNow,
                Title = "TestReport2",
                Summary = "TestSummary2",
                Description = "TestDescription2",
                IndustryId = 2,
            };
        }
        public static Report SeedReport3()
        {
            return new Report
            {
                Id = 3,
                AuthorId = 1,
                CreatedOn = DateTime.UtcNow,
                Summary = "TestSummary3",
                Title = "TestReport3",
                Description = "TestDescription3",
                IndustryId = 3,
            };
        }
        //Seed Users
        public static User SeedUser()
        {
            return new User
            {
                Id = 1,
                FirstName = "First",
                LastName = "Test",
                Email = "firstTest@user.com",
                UserName = "firstTest@user.com",
                CreatedOn = DateTime.UtcNow,
                PhoneNumber = "333221",
            };
        }
        public static User SeedUser2()
        {
            return new User
            {
                Id = 2,
                FirstName = "Second",
                LastName = "Test",
                Email = "secondTest@user.com",
                UserName = "secondTest@user.com",
                CreatedOn = DateTime.UtcNow,
                PhoneNumber = "122333",
            };
        }
        public static User SeedUser3()
        {
            return new User
            {
                Id = 3,
                FirstName = "Third",
                LastName = "Test",
                Email = "thirdTest@user.com",
                UserName = "thirdTest@user.com",
                CreatedOn = DateTime.UtcNow,
                PhoneNumber = "123321",
            };
        }
        //Seed Downloaded Reports
        public static DownloadedReport SeedDownloadedReport()
        {
            return new DownloadedReport
            {
                UserId = 1,
                ReportId = 1,
            };
        }
        public static DownloadedReport SeedDownloadedReport2()
        {
            return new DownloadedReport
            {
                UserId = 1,
                ReportId = 2,
            };
        }
        public static DownloadedReport SeedDownloadedReport3()
        {
            return new DownloadedReport
            {
                UserId = 2,
                ReportId = 2,
            };
        }
    }
}

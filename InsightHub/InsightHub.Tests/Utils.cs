using InsightHub.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsightHub.Tests
{
    public class Utils
    {
        public static DbContextOptions<InsightHubContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<InsightHubContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}

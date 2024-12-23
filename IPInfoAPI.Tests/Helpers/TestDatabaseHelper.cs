using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPInfoAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace IPInfoAPI.Tests.Helpers
{
    public static class TestDatabaseHelper
    {
        public static DbContextOptions<ApplicationDbContext> CreateInMemoryDatabase()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }
    }
}

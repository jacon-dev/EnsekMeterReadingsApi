using MeterReadingsApi.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeterReadingsApi.DataAccess
{
    public class ApiDataContext : DbContext
    {
        public ApiDataContext(DbContextOptions<ApiDataContext> options) :base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; } 
        
        public DbSet<MeterReading> MeterReadings { get; set; }
    }
}

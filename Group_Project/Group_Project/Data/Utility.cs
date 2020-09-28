﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Data
{
    public class Utility
    {
        public static Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext> TestDbContextOptions()
        {
            // Create a new service provider to create a new in-memory database.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance using an in-memory database and 
            // IServiceProvider that the context should resolve all of its 
            // services from.
            var builder = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("LMS_Zenith")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}

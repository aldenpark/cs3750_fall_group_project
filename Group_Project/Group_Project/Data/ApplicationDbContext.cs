using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Group_Project.Models;

namespace Group_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<Group_Project.Models.Course> Course { get; set; }

        public DbSet<Group_Project.Models.Registration> Registration { get; set; }

        public DbSet<Group_Project.Models.Assignment> Assignment { get; set; }

        public DbSet<Group_Project.Models.StudentPayments> StudentPayments { get; set; }
    }
}

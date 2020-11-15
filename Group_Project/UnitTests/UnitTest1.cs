using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private Group_Project.Data.ApplicationDbContext _context;

        public UnitTest1()
        {
            DbContextOptions<Group_Project.Data.ApplicationDbContext> options = new DbContextOptions<Group_Project.Data.ApplicationDbContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Server=titan.cs.weber.edu,10433;Database=LMS_Zenith;user id=LMS_Zenith;password=Password*2", null);

            _context = new Group_Project.Data.ApplicationDbContext((DbContextOptions<Group_Project.Data.ApplicationDbContext>)builder.Options);
        }

        [TestMethod]
        public async Task TestDBAccess()
        {
            string FirstName = await _context.User.Where(x => x.ID == 20).Select(x => x.FirstName).FirstOrDefaultAsync();

            Assert.AreEqual(FirstName, "Aric");
        }

        [TestMethod]
        public async Task TestRegistration()
        {
            var user = await _context.User.Where(x => x.ID == 20).FirstOrDefaultAsync();
            var courseIds = await _context.Course.Select(x => x.ID).ToListAsync();
            int numCourses = courseIds.Count();
            List<Group_Project.Models.Registration> registrations = new List<Group_Project.Models.Registration>();

            registrations = _context.Registration.Where(x => x.StudentID == user.ID).ToList();
            _context.RemoveRange(registrations);
            _context.SaveChanges();

            int i = 0;
            foreach (var id in courseIds)
            {
                registrations.Add(new Group_Project.Models.Registration());
                registrations[i].CourseID = id;
                registrations[i].paid = false;
                registrations[i].Status = Group_Project.Models.ClassTypes.Registered;
                registrations[i].StudentID = user.ID;
                
                i++;
            }

            var beforeCount = _context.Registration.Where(x => x.StudentID == user.ID).Count();

            
            foreach(var registration in registrations)
            {
                _context.Add(registration);
            }

            _context.SaveChanges();

            var afterCount = _context.Registration.Where(x => x.StudentID == user.ID).Count();

            Assert.IsTrue(beforeCount < afterCount);
            Assert.IsTrue(afterCount == numCourses);

            

            /*
            registrations = _context.Registration.Where(x => x.StudentID == user.ID).ToList();

            _context.RemoveRange(registrations);
            _context.SaveChanges();

            var deletedCount = _context.Registration.Where(x => x.StudentID == user.ID).Count();

            Assert.IsTrue(deletedCount == 0);
            */
        }

        [TestMethod]
        public async Task TestGrading()
        {
            var grade = 72;

            //save the grade
            //save your changes

            //pull grade from database
            //assert that grade = grade in database
        }


    }
}

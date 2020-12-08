using Group_Project.Controllers;
using Group_Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit.Sdk;
using System.Runtime.Serialization.Json;
using System.Web.Helpers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Group_Project.Helpers;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private Group_Project.Data.ApplicationDbContext _context;
        private Group_Project.Data.Repository.UnitOfWork _unitOfWork;

        public UnitTest1()
        {
            DbContextOptions<Group_Project.Data.ApplicationDbContext> options = new DbContextOptions<Group_Project.Data.ApplicationDbContext>();
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder(options);
            SqlServerDbContextOptionsExtensions.UseSqlServer(builder, "Server=titan.cs.weber.edu,10433;Database=LMS_Zenith;user id=LMS_Zenith;password=Password*2", null);

            _context = new Group_Project.Data.ApplicationDbContext((DbContextOptions<Group_Project.Data.ApplicationDbContext>)builder.Options);
            _unitOfWork = new Group_Project.Data.Repository.UnitOfWork(_context);
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

            registrations = new List<Group_Project.Models.Registration>();

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

            _context.RemoveRange(registrations);
            _context.SaveChanges();

        }

        [TestMethod]
        public async Task TestRegistrationV2()
        {
            SearchCourses filter = new SearchCourses();
            HttpClient httpClient = new HttpClient();
            RegistrationController registrationController = new RegistrationController(_unitOfWork);
            httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            filter.isTest = true;
            filter.CourseName = "";
            filter.CourseNumber = "";
            filter.CreditHours = "";
            filter.Department = "";

            var registrationsFromDatabase = _context.Registration.Where(x => x.StudentID == 20);
            _context.Registration.RemoveRange(registrationsFromDatabase);
            _context.SaveChanges();

            var coursesFromController = registrationController.GetCourses(filter, 20);

            int controllerCourseCount = coursesFromController.Count;
            int databaseCourseCount = _context.Course.Count();

            Assert.AreEqual(controllerCourseCount, databaseCourseCount);

            foreach(var course in coursesFromController)
            {
                registrationController.AddOrRemoveRegistration(course.ID, true, 20);
            }

            int registrationCountFromDatabase = _context.Registration.Where(x => x.StudentID == 20).Count();
            int registrationCountFromController = registrationController.GetCourses(filter, 20).Count();

            Assert.AreEqual(databaseCourseCount, registrationCountFromDatabase);
            Assert.AreEqual(controllerCourseCount, registrationCountFromDatabase);
            Assert.AreEqual(registrationCountFromController, registrationCountFromDatabase);

            registrationsFromDatabase = _context.Registration.Where(x => x.StudentID == 20);
            _context.Registration.RemoveRange(registrationsFromDatabase);
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task TestAddingAssignment()
        {
            Group_Project.Models.Assignment assignment = new Group_Project.Models.Assignment();

            assignment.CourseID = 1;
            assignment.Title = "Test";
            assignment.Description = "This is a test description";
            assignment.DueDate = DateTime.Now;
            assignment.PointsPossible = 10;
            assignment.SubmissionType = "Text Entry";
            assignment.TextSubmission = "Text Submission Test Text";
            assignment.Grade = 10;

            //Arics notes for daniel
            _context.Assignment.Add(assignment);
            _context.SaveChanges();

            var assignmentFromDB = _context.Assignment.Where(x => x.CourseID == assignment.CourseID).Where(x => x.Title == assignment.Title).FirstOrDefault();

            Assert.IsNotNull(assignmentFromDB);

            _context.Assignment.Remove(assignmentFromDB);
            _context.SaveChanges();

            var deletedAssignmentFromDB = _context.Assignment.Where(x => x.ID == assignmentFromDB.ID).FirstOrDefault();

            Assert.IsNull(deletedAssignmentFromDB);

            /*
            _context.Add(assignment);
            _context.SaveChanges();
            _context.Remove(assignment);
            _context.SaveChanges();
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

        [TestMethod]
        public async Task TestCourse()
        {
            var user = await _context.User.Where(x => x.ID == 18).FirstOrDefaultAsync();
            List<Group_Project.Models.Course> courses = new List<Group_Project.Models.Course>();
            courses = _context.Course.Where(x => x.InstructorID == user.ID).ToList();
            _context.RemoveRange(courses);
            _context.SaveChanges();

            courses.Add(new Group_Project.Models.Course());
            courses[0].InstructorID = user.ID;
            courses[0].CourseName = "First Aid: Responding to Emergencies";
            courses[0].CourseNumber = 4;
            courses[0].Department = "AT";
            courses[0].Description = "First aid, for all your first aid needs";
            courses[0].CreditHours = 4;
            courses[0].Location = "1234 Generic Drive Building 2";
            courses[0].Monday = true;
            courses[0].Tuesday = false;
            courses[0].Wednesday = false;
            courses[0].Thursday = false;
            courses[0].Friday = false;
            courses[0].Saturday = false;
            courses[0].Sunday = false;
            courses[0].StartTime = "01:30:00";
            courses[0].EndTime = "02:00:00";
            courses[0].MaxStudents = 20;

            var beforeCount = _context.Course.Where(x => x.InstructorID == user.ID).Count();
            
            foreach (var course in courses)
            {
                _context.Add(course);
            }

            _context.SaveChanges();

            var afterCount = _context.Course.Where(x => x.InstructorID == user.ID).Count();

            Assert.IsTrue(beforeCount < afterCount);

            courses = _context.Course.Where(x => x.InstructorID == user.ID).ToList();

            _context.RemoveRange(courses);
            _context.SaveChanges();

            var deletedCount = _context.Course.Where(x => x.InstructorID == user.ID).Count();

            Assert.IsTrue(deletedCount == 0);

        }

        [TestMethod]
        public async Task TestPasswordEncryption()
        {
            string password = "Password123$";

            var UserObj = new User();
            UserObj.Passwrd = password;

            var encrypt = new Security();
            UserObj.Passwrd = encrypt.EncryptString(UserObj.Passwrd);
            UserObj.Passwrd = encrypt.HashPassword(UserObj.Passwrd);
            
            string EncryptedPassword = encrypt.EncryptString(password);

            var EncryptedPass = encrypt.EncryptString("Password123$");

            var loginPassed = true;
            if (encrypt.IsMatch(EncryptedPass, UserObj.Passwrd) != true)
            {
                loginPassed = false;
            }

            Assert.IsTrue(loginPassed == true);
        }


    }

    public class RegistrationResponse
    {
        List<Course> CourseList { get; set; }
    }

    public class CouseAsStrings
    {
        public string ID { get; set; }
        //The ID of the course NOT the instructor!
        public string InstructorID { get; set; }
        //Same as the ID of the instructor, should ideally be a foreign key, 
        //but might not be neccessary to program it as such
        public string CourseName { get; set; }
        public string CourseNumber { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        public string CreditHours { get; set; }
        public string Location { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }

        //Time of day
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        
        public string MaxStudents { get; set; }
        public string Registered { get; set; } 
    }
}

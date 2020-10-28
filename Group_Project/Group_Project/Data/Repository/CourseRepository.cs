using Group_Project.Data.Repository.IRepository;
using Group_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Data.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext _db;

        public CourseRepository(ApplicationDbContext db) : base(db)  // ctor tabtab
        {
            _db = db;
        }

        public void Update(Course course)
        {
            var objFromDb = _db.Course.FirstOrDefault(s => s.ID == course.ID);

            objFromDb.InstructorID = course.InstructorID;
            objFromDb.CourseName = course.CourseName;
            objFromDb.CourseNumber = course.CourseNumber;
            objFromDb.Department = course.Department;
            objFromDb.CreditHours = course.CreditHours;
            objFromDb.Location = course.Location;
            objFromDb.Monday = course.Monday;
            objFromDb.Tuesday = course.Tuesday;
            objFromDb.Wednesday = course.Wednesday;
            objFromDb.Thursday = course.Thursday;
            objFromDb.Friday = course.Friday;
            objFromDb.Saturday = course.Saturday;
            objFromDb.Sunday = course.Sunday;
            objFromDb.StartTime = course.StartTime;
            objFromDb.EndTime = course.EndTime;
            objFromDb.MaxStudents = course.MaxStudents;

            _db.SaveChanges();
        }
    }
}

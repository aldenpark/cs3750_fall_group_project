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

            //objFromDb.Email = user.Email;
            //objFromDb.Passwrd = user.Passwrd;
            //objFromDb.FirstName = user.FirstName;
            //objFromDb.LastName = user.LastName;
            //objFromDb.Birthdate = user.Birthdate;
            //objFromDb.UserType = user.UserType;
            //objFromDb.ProfilePic = user.ProfilePic;

            _db.SaveChanges();
        }
    }
}

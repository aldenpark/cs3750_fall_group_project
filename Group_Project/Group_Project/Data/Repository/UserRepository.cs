using Group_Project.Data.Repository.IRepository;
using Group_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)  // ctor tabtab
        {
            _db = db;
        }

        public void Update(User user)
        {
            var objFromDb = _db.User.FirstOrDefault(s => s.ID == user.ID);

            objFromDb.Email = user.Email;
            objFromDb.Passwrd = user.Passwrd;
            objFromDb.FirstName = user.FirstName;
            objFromDb.LastName = user.LastName;
            objFromDb.Birthdate = user.Birthdate;
            objFromDb.UserType = user.UserType;
            objFromDb.ProfilePic = user.ProfilePic;

            _db.SaveChanges();
        }
    }
}

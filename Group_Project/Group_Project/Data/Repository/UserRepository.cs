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

            objFromDb.Passwrd = user.Passwrd;
            objFromDb.FirstName = user.FirstName;
            objFromDb.LastName = user.LastName;
            objFromDb.Birthdate = user.Birthdate;
            objFromDb.ProfilePic = user.ProfilePic;
            objFromDb.AddressLine1 = user.AddressLine1;
            objFromDb.AddressLine2 = user.AddressLine2;
            objFromDb.State = user.State;
            objFromDb.City = user.City;
            objFromDb.Zip = user.Zip;
            objFromDb.Bio = user.Bio;
            objFromDb.Link1 = user.Link1;
            objFromDb.Link2 = user.Link2;
            objFromDb.Link3 = user.Link3;

            _db.SaveChanges();
        }
    }
}

using Group_Project.Data.Repository.IRepository;
using Group_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IUserRepository User { get; private set; }
        public ICourseRepository Course { get; private set; }
        public IRegistrationRepository Registration { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
            Course = new CourseRepository(_db);
            Registration = new RegistrationRepository(_db);
        }
        void IDisposable.Dispose()
        {
            _db.Dispose();
        }

        void IUnitOfWork.Save()
        {
            _db.SaveChanges();
        }
    }
}

using Group_Project.Data.Repository.IRepository;
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

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
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

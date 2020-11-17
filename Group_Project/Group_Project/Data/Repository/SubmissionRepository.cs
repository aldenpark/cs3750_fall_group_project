using Group_Project.Data.Repository.IRepository;
using Group_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Data.Repository
{
    public class SubmissionRepository : Repository<Submission>, ISubmissionRepository
    {
        private readonly ApplicationDbContext _db;

        public SubmissionRepository(ApplicationDbContext db) : base(db)  // ctor tabtab
        {
            _db = db;
        }

        public void Update(Submission obj)
        {
            var objFromDb = _db.Submission.FirstOrDefault(s => s.ID == obj.ID);
            objFromDb.Points = obj.Points;

            _db.SaveChanges();
        }
    }
}

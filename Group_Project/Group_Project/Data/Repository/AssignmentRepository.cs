using Group_Project.Data.Repository.IRepository;
using Group_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Data.Repository
{
    public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
    {
        private readonly ApplicationDbContext _db;

        public AssignmentRepository(ApplicationDbContext db) : base(db)  // ctor tabtab
        {
            _db = db;
        }

        public void Update(Assignment assignment)
        {
            var objFromDb = _db.Assignment.FirstOrDefault(s => s.ID == assignment.ID);

            objFromDb.Title = assignment.Title;
            objFromDb.Description = assignment.Description;
            objFromDb.DueDate = assignment.DueDate;
            objFromDb.PointsPossible = assignment.PointsPossible;
            objFromDb.SubmissionType = assignment.SubmissionType;

            _db.SaveChanges();
        }


    }
}

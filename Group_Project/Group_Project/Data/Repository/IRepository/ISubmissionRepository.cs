using Group_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Data.Repository.IRepository
{
    public interface ISubmissionRepository : IRepository<Submission>
    {
        void Update(Submission obj);
    }
}

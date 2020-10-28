using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        ICourseRepository Course { get; }
        IAssignmentRepository Assignment { get; }
        void Save();
    }
}

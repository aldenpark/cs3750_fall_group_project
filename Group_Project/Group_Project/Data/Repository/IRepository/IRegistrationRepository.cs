using Group_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Data.Repository.IRepository
{
    public interface IRegistrationRepository : IRepository<Registration>
    {
        void Update(Registration obj);
    }
}

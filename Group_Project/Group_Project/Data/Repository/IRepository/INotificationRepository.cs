using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group_Project.Models;

namespace Group_Project.Data.Repository.IRepository
{
    public interface INotificationRepository : IRepository<Notification>
    {
        void update(Notification obj);
    }
}

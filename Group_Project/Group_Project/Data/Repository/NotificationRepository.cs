using Group_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group_Project.Data.Repository.IRepository;

namespace Group_Project.Data.Repository
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private readonly ApplicationDbContext _db;

        public NotificationRepository(ApplicationDbContext db) : base(db)  // ctor tabtab
        {
            _db = db;
        }


        public void update(Notification obj)
        {
            var objFromDb = _db.Notification.FirstOrDefault(s => s.ID == obj.ID);


            _db.SaveChanges();
        }
    }
}

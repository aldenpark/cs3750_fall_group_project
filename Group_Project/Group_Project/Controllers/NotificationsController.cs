using Group_Project.Data.Repository.IRepository;
using Group_Project.Models;
using Group_Project.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Group_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<NotificationsController>
        [HttpGet]
        public IActionResult Get()
        {

            int userId = 0;
            if (HttpContext.Session.GetInt32(SD.UserSessionId) != null)
            {
                if (HttpContext.Session.GetInt32(SD.UserSessionId).HasValue)
                {
                    userId = HttpContext.Session.GetInt32(SD.UserSessionId).Value;
                }
            }

            var notifications = new List<Notification>();

            var courses = new List<Course>();
            var assignments = new List<Assignment>();

            if (userId > 0)
            {
                var Usr = _unitOfWork.User.Get(userId);

                if (Usr.UserType == 'S')
                {
                    var submissions = _unitOfWork.Submission.GetAll(x => x.UserId == Usr.ID);
                    foreach (var submission in submissions)
                    {
                        notifications.AddRange(_unitOfWork.Notification.GetAll(x => x.sourceID == submission.AssignmentId));
                    }
                    var registrations = _unitOfWork.Registration.GetAll(x => x.StudentID == Usr.ID);
                    foreach (var registration in registrations)
                    {
                        courses.AddRange(_unitOfWork.Course.GetAll(x => x.ID == registration.CourseID));
                    }
                    foreach (var course in courses)
                    {
                        assignments.AddRange(_unitOfWork.Assignment.GetAll(x => x.CourseID == course.ID));
                    }
                    foreach (var assignment in assignments)
                    {
                        notifications.AddRange(_unitOfWork.Notification.GetAll(x => x.sourceID == assignment.ID));
                    }
                }


            }

            return Json(new { notifications });
        }

        // DELETE api/<NotificationsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _unitOfWork.Notification.Remove(id);
            _unitOfWork.Save();
            return Json(new { success = true });
        }
    }
}

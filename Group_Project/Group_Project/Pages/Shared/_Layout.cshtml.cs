using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Group_Project.Data;
using Group_Project.Models;
using Microsoft.AspNetCore.Http;
using Group_Project.Utility;
using Group_Project.Helpers;

namespace Group_Project.Pages.Shared
{
    public class _LayoutPageModel : PageModel
    {
         private readonly Group_Project.Data.ApplicationDbContext _context;
        private CourseHelper courseHelper;

        public _LayoutPageModel(Group_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Models.Course> Course { get;set; }
        public User User { get; set; }
        public List<Models.Assignment> Assignments {get; set;}
        public List<Models.Submission> Submissions { get; set; }
        public List<Models.Notification> Notifications { get; set; }
        
        public async Task OnGetAsync()
        {
            var userId = HttpContext.Session.GetInt32(SD.UserSessionId);
            if (userId == null)
            {
                userId = 0;
            }
            User = _context.User.Where(x => x.ID == userId).FirstOrDefault();
            var isInstructor = await _context.User.Where(x => x.ID == userId).Where(x => x.UserType == 'I').AnyAsync();
            courseHelper = new CourseHelper();

            if(isInstructor)
            {
                Course = await _context.Course.Where(x => x.InstructorID == userId).ToListAsync();
            }
            else
            {
                var courseIds = await _context.Registration.Where(x => x.StudentID == userId).Select(x => x.CourseID).ToListAsync();
                Course = new List<Models.Course>();
                Assignments = new List<Models.Assignment>();
                Submissions = new List<Models.Submission>();


                foreach(int id in courseIds)
                {
                    var createdAssignments = await _context.Assignment.Where(x => x.CourseID == id).Select(x => x.ID).ToListAsync();

                     foreach(int assignmentid in createdAssignments){
                        Assignments.Add(await _context.Assignment.Where(x => x.ID == assignmentid).FirstOrDefaultAsync());

                        var gradedAssignments = await _context.Submission.Where(x => x.AssignmentId == assignmentid).Select(x => x.ID).ToListAsync();
                        
                        foreach(int graddedId in gradedAssignments){
                            Submissions.Add(await _context.Submission.Where(x => x.ID == graddedId).FirstOrDefaultAsync());
                        }

                    }

                    Course.Add(await _context.Course.Where(x => x.ID == id).FirstOrDefaultAsync());
                }

              
                   
                   


            }


            Notifications = new List<Models.Notification>();

            foreach(Models.Assignment assign in Assignments){
                var notifID = await _context.Notification.Where(x => x.sourceID == assign.ID).Select(x => x.ID).ToListAsync();

                foreach(int id in notifID){
                Notifications.Add(await _context.Notification.Where(x => x.ID == id).FirstOrDefaultAsync());
                }

            }

            foreach(Models.Submission sub in Submissions){
                var notifID = await _context.Notification.Where(x => x.sourceID == sub.ID).Select(x => x.ID).ToListAsync();

                foreach(int id in notifID){
                Notifications.Add(await _context.Notification.Where(x => x.ID == id).FirstOrDefaultAsync());
                }

            }


            foreach (Models.Course course in Course) {
                var assignmentIds = await _context.Assignment.Where(x => x.CourseID == course.ID).Where(x => x.DueDate >= DateTime.Today).Select(x => x.ID).ToListAsync();
                
                foreach (int id in assignmentIds)
                {
                    Assignments.Add(await _context.Assignment.Where(x => x.ID == id).FirstOrDefaultAsync());
                }
            }

        }
    }



}

    
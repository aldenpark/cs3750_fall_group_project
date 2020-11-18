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
namespace Group_Project.Pages
{
    public class LoggedInHomePageModel : PageModel
    {
        private readonly Group_Project.Data.ApplicationDbContext _context;
        private CourseHelper courseHelper;

        public LoggedInHomePageModel(Group_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Models.Course> Course { get;set; }
        public User User { get; set; }
        public List<Models.Assignment> Assignment { get; set; }



        public async Task OnGetAsync()
        {
            var userId = HttpContext.Session.GetInt32(SD.UserSessionId);
            if (userId == null)
            {
                userId = 0;
            }
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

                foreach(int id in courseIds)
                {
                    Course.Add(await _context.Course.Where(x => x.ID == id).FirstOrDefaultAsync());
                }

               


            }


            Assignment = new List<Models.Assignment>();

            foreach (Models.Course course in Course) {
                var assignmentIds = await _context.Assignment.Where(x => x.CourseID == course.ID).Where(x => x.DueDate >= DateTime.Today).Select(x => x.ID).ToListAsync();
                foreach (int id in assignmentIds)
                {
                    Assignment.Add(await _context.Assignment.Where(x => x.ID == id).FirstOrDefaultAsync());
                }
            }

        }


    }
}

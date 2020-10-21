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

namespace Group_Project.Pages.Course
{
    public class InstructorCourseListModel : PageModel
    {
        private readonly Group_Project.Data.ApplicationDbContext _context;
        private CourseHelper courseHelper;

        public InstructorCourseListModel(Group_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.Course> Course { get;set; }

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

                foreach (var course in Course)
                {
                    course.StartTime = courseHelper.ConvertMilitaryToStandardTime(course.StartTime);
                    course.EndTime = courseHelper.ConvertMilitaryToStandardTime(course.EndTime);
                }
            }
            else
            {
                userId = 0;
                Course = await _context.Course.Where(x => x.InstructorID == userId).ToListAsync();
            }
            
        }
    }
}

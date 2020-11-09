using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Group_Project.Data;
using Group_Project.Models;
using Group_Project.Helpers;
using Microsoft.AspNetCore.Http;
using Group_Project.Utility;

namespace Group_Project.Pages.Course
{
    public class CourseDetailsModel : PageModel
    {
        private readonly Group_Project.Data.ApplicationDbContext _context;
        private CourseHelper courseHelper;

        public CourseDetailsModel(Group_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public User User { get; set; }
        public Models.Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            int userId = (int)HttpContext.Session.GetInt32(SD.UserSessionId);

            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Course.FirstOrDefaultAsync(m => m.ID == id);
            courseHelper = new CourseHelper();

            if (Course == null)
            {
                return NotFound();
            }
            else
            {
                string startEndTime = courseHelper.ConcatenateStartAndEndTime(Course);
                string fullSchedule = courseHelper.ConcatenateDaysAndTimes(Course, startEndTime);

                Course.StartTime = fullSchedule;
            }

            if(id > 0)
            {
                User = _context.User.Where(x => x.ID == userId).FirstOrDefault();
            }


            return Page();
        }
    }
}

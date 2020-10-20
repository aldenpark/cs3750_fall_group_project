using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Group_Project.Data;
using Group_Project.Models;

namespace Group_Project.Pages.Course
{
    public class CourseDetailsModel : PageModel
    {
        private readonly Group_Project.Data.ApplicationDbContext _context;

        public CourseDetailsModel(Group_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Models.Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Course.FirstOrDefaultAsync(m => m.ID == id);            


            if (Course == null)
            {
                return NotFound();
            }
            else
            {
                string[] times = Course.StartTime.Split(':');

                int hour = int.Parse(times[0]);
                string AMPM = "";

                if (hour > 12)
                {
                    AMPM = "PM";
                    hour = hour - 12;
                }
                else
                {
                    AMPM = "AM";
                }

                Course.StartTime = hour.ToString() + ":" + times[1] + " " + AMPM;
            }

            return Page();
        }
    }
}

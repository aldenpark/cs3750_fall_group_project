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

namespace Group_Project.Pages.Assignments
{
    public class AssignmentHomeModel : PageModel
    {
        private readonly Group_Project.Data.ApplicationDbContext _context;
        private CourseHelper courseHelper;

        public AssignmentHomeModel(Group_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public User User { get; set; }
        public Models.Assignment Assignment { get; set; }

        public Models.Submission Submission { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            //id = HttpContext.Session.GetInt32(SD.UserSessionId);

            if (id == null)
            {
                return NotFound();
            }

            Assignment = await _context.Assignment.FirstOrDefaultAsync(m => m.ID == id);
            courseHelper = new CourseHelper();

            if (Assignment == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Group_Project.Data;
using Group_Project.Models;
using Microsoft.AspNetCore.Http;
using Group_Project.Utility;

namespace Group_Project.Pages.Course
{
    public class EditCourseModel : PageModel
    {
        private readonly Group_Project.Data.ApplicationDbContext _context;

        public EditCourseModel(Group_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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
            
            //Get userId of logged in user
            var userId = HttpContext.Session.GetInt32(SD.UserSessionId);
            if (userId == null)
            {
                userId = 0;
            }
            //Check if user is an instructor
            var isInstructor = await _context.User.Where(x => x.ID == userId).Where(x => x.UserType == 'I').AnyAsync();

            //Makes sure user is logged in, is an instructor and is the instructor who created the course
            if (userId == 0 || !isInstructor || (userId != Course.InstructorID))
            {
                return Unauthorized();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(Course.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.ID == id);
        }
    }
}

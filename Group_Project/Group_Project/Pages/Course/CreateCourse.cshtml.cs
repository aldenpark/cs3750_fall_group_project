using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Group_Project.Data;
using Group_Project.Models;
using Group_Project.Helpers;
using Microsoft.AspNetCore.Http;
using Group_Project.Utility;
using Microsoft.EntityFrameworkCore;

namespace Group_Project.Pages.Course
{
    public class CreateCourseModel : PageModel
    {
        private readonly Group_Project.Data.ApplicationDbContext _context;
        private CourseHelper courseHelper;
        private CourseValidationResponse courseValidationResponse;

        public CreateCourseModel(Group_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {

            return Page();
        }

        [BindProperty]
        public Models.Course Course { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            courseHelper = new CourseHelper();
            courseValidationResponse = new CourseValidationResponse();

            courseValidationResponse = courseHelper.ValidateCourse(Course);

            //Get userId of logged in user
            var userId = HttpContext.Session.GetInt32(SD.UserSessionId);
            if (userId == null)
            {
                userId = 0;
            }

            //check if logged in user is an instructor
            var isInstructor = await _context.User.Where(x => x.ID == userId).Where(x => x.UserType == 'I').AnyAsync();

            //If user is not logged in or is not an instructor or the instructorId for the course is not the ID of the user, return unauthorized
            if(userId == 0 || !isInstructor || Course.InstructorID != userId)
            {
                return Unauthorized();
            }

            //Make sure that start time is before the end time
            if (courseValidationResponse.isValidated)
            {
                _context.Course.Add(Course);
                await _context.SaveChangesAsync();
            }
            //TODO: Make error page 



            return RedirectToPage("./Index");
        }
    }
}

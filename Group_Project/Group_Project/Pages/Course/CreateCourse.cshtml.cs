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

namespace Group_Project.Pages.Course
{
    public class CreateCourseModel : PageModel
    {
        private readonly Group_Project.Data.ApplicationDbContext _context;
        private CourseValidator courseValidator;
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

            courseValidator = new CourseValidator();
            courseValidationResponse = new CourseValidationResponse();

            courseValidationResponse = courseValidator.ValidateCourse(Course);

            //Make sure that start time is before the end time
            if(courseValidationResponse.isValidated)
            {
                _context.Course.Add(Course);
                await _context.SaveChangesAsync();
            }



            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Group_Project.Data;
using Group_Project.Models;
using Microsoft.AspNetCore.Http;
using Group_Project.Utility;
using Microsoft.EntityFrameworkCore;

namespace Group_Project.Pages.Assignments
{
    public class AssignmentCreateModel : PageModel
    {
        private readonly Group_Project.Data.ApplicationDbContext _context;

        public AssignmentCreateModel(Group_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            int userId = (int)HttpContext.Session.GetInt32(SD.UserSessionId);
            var isInstructor = await _context.User.Where(x => x.ID == userId).Where(x => x.UserType == 'I').AnyAsync();

            if(!isInstructor)
            {
                return NotFound();
            }

            var course = _context.Course.Where(x => x.ID == id).FirstOrDefault();

            if(course == null)
            {
                return BadRequest();
            }
            else
            {
                CourseId = course.ID;
            }

            return Page();
        }

        [BindProperty]
        public Assignment Assignment { get; set; }


        [ViewData]
        public int CourseId { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Models.Notification NotificationObj = new Models.Notification();

            NotificationObj.sourceID = Assignment.ID;
            NotificationObj.Type = 'A';
            NotificationObj.Message = "Assignment: " + Assignment.Title + " Has been Created"; 

            _context.Notification.Add(NotificationObj);

            _context.Assignment.Add(Assignment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./LoggedInHomePage");
        }
    }
}

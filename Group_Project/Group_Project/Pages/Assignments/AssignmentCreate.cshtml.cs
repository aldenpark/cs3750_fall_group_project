using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Group_Project.Data;
using Group_Project.Models;

namespace Group_Project.Pages.Assignments
{
    public class AssignmentCreateModel : PageModel
    {
        private readonly Group_Project.Data.ApplicationDbContext _context;

        public AssignmentCreateModel(Group_Project.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Assignment Assignment { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Assignment.Add(Assignment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
